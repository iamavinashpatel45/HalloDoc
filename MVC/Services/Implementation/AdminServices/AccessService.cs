using Repositories.DataModels;
using Repositories.Interface;
using Repositories.Interfaces;
using Services.Interfaces.AdminServices;
using Services.ViewModels.Admin;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementation.AdminServices
{
    public class AccessService : IAccessService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRequestClientRepository _requestClientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAspRepository _aspRepository;

        public AccessService(IRoleRepository roleRepository, IRequestClientRepository requestClientRepository,IAspRepository aspRepository,
                                         IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _requestClientRepository = requestClientRepository;
            _aspRepository = aspRepository;
            _userRepository = userRepository;
        }

        public Access getAccessData()
        {
            return new Access()
            {
                RolesData = _roleRepository.getAllRoles().Select(role => new AccessTable()
                            {
                                Name = role.Name,
                                AccountType = role.AccountTypeNavigation.Name,
                                RoleId = role.RoleId,
                            }).ToList(),
            };
        }

        public CreateRole getCreateRole()
        {
            return new CreateRole()
            {
                RolesCheckBox = new RolesCheckBox()
                {
                    SelectedMenusForLoadData = new List<int>(),
                    Menus = _roleRepository.getAllMenus().ToDictionary(menu => menu.MenuId, menu => menu.Name),
                },
            };
        }

        public RolesCheckBox getMenusByRole(int roleId)
        {
            return new RolesCheckBox()
            {
                SelectedMenusForLoadData = new List<int>(),
                Menus = _roleRepository.getAllMenusByRole(roleId).ToDictionary(menu => menu.MenuId, menu => menu.Name),
            };
        }

        public async Task<bool> createRole(CreateRole model)
        {
            Role role = new Role()
            {
                Name = model.RoleName,
                AccountType = model.SlectedAccountType,
                CreatedDate = DateTime.Now,
            };
            int roleId = await _roleRepository.addRole(role); 
            if(roleId > 0 && model.SelectedMenus.Count > 0)
            {
                await _roleRepository.addRoleMenus(
                        model.SelectedMenus.Select(menuId =>
                        new RoleMenu()
                        {
                            RoleId = roleId,
                            MenuId = menuId,
                        }).ToList()
                );
                return true;
            }
            else if(roleId > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> delete(int roleId,int aspNetUserId)
        {
            if(await _roleRepository.deleteRoleMenus(_roleRepository.getAllRoleMenusByRole(roleId)))
            {
                Role role = _roleRepository.getRoleByRoleId(roleId);
                role.IsDeleted = new BitArray(1, true);
                role.ModifiedDate = DateTime.Now;
                role.ModifiedBy = aspNetUserId.ToString();
                return await _roleRepository.updateRole(role);
            }
            return false;
        }

        public AdminCreaateAndProfile GetAdminCreaateAndProfile()
        {
            return new AdminCreaateAndProfile()
            {
                Regions = _requestClientRepository.getAllRegions().ToDictionary(region => region.RegionId, region => region.Name),
                Roles = _roleRepository.getRolesByUserType(2).Select(role => role.Name).ToList(),
            };
        }

        public async Task<bool> createAdmin(AdminCreaateAndProfile model)
        {
            int aspNetRoleId = _aspRepository.checkUserRole(role: "Admin");
            if (aspNetRoleId == 0)
            {
                AspNetRole aspNetRole = new()
                {
                    Name = "Physician",
                };
                aspNetRoleId = await _aspRepository.addUserRole(aspNetRole);
            }
            AspNetUser aspNetUser = new()
            {
                UserName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                PasswordHash = genrateHash(model.Password),
                CreatedDate = DateTime.Now,
            };
            int aspNetUserId = await _aspRepository.addUser(aspNetUser);
            AspNetUserRole aspNetUserRole = new()
            {
                UserId = aspNetUserId,
                RoleId = aspNetRoleId,
            };
            await _aspRepository.addAspNetUserRole(aspNetUserRole);
            Admin admin = new Admin()
            {
                AspNetUserId = aspNetUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Mobile = model.Mobile,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                RegionId = int.Parse(model.SelectedRegion),
                Zip = model.ZipCode,
                AltPhone = model.Phone,
                Status = model.Status,
                Role = model.SelectedRole,
            };
            if(await _userRepository.addAdmin(admin))
            {
                List<AdminRegion> adminRegions = new List<AdminRegion>();
                foreach (String regionId in model.SelectedRegions)
                {
                    adminRegions.Add(new AdminRegion()
                    {
                        AdminId = admin.AdminId,
                        RegionId = int.Parse(regionId),
                    });
                }
                await _userRepository.addAdminRgions(adminRegions);
            }
            return false;
        }

        public CreateRole getEditRole(int roleId)
        {
            Role role = _roleRepository.getRoleByRoleId(roleId);
            return new CreateRole()
            {
                IsUpdate = true,
                RoleName = role.Name,
                SlectedAccountType = role.AccountType,
                RolesCheckBox = new RolesCheckBox()
                {
                    SelectedMenusForLoadData = _roleRepository.getAllRoleMenusByRole(roleId).Select(roleMenu => roleMenu.MenuId).ToList(),
                    Menus = _roleRepository.getAllMenusByRole(role.AccountType).ToDictionary(menu => menu.MenuId, menu => menu.Name),
                },
            };
        }

        public async Task<bool> editRole(CreateRole model,int roleId,int aspNetUserId)
        {
            List<RoleMenu> roleMenus = _roleRepository.getAllRoleMenusByRole(roleId);
            List<RoleMenu> roleMenusForDelete = new List<RoleMenu>();
            List<RoleMenu> roleMenusCreateNew = new List<RoleMenu>();
            foreach (RoleMenu roleMenu in roleMenus)
            {
                if (!model.SelectedMenus.Contains(roleMenu.MenuId))
                {
                    roleMenusForDelete.Add(roleMenu);
                }
            }
            foreach (int menuId in model.SelectedMenus)
            {
                if (!roleMenus.Any(a => a.MenuId == menuId))
                {
                    RoleMenu roleMenu = new RoleMenu()
                    {
                        RoleId = roleId,
                        MenuId = menuId,
                    };
                    roleMenusCreateNew.Add(roleMenu);
                }
            }
            if(roleMenusForDelete.Count > 0)
            {
                await _roleRepository.deleteRoleMenus(roleMenusForDelete);
            }
            if (roleMenusCreateNew.Count > 0)
            {
                await _roleRepository.addRoleMenus(roleMenusCreateNew);
            }
            Role role = _roleRepository.getRoleByRoleId(roleId);
            role.Name = model.RoleName;
            role.AccountType = model.SlectedAccountType;
            role.ModifiedDate = DateTime.Now;
            role.ModifiedBy = aspNetUserId.ToString();
            return await _roleRepository.updateRole(role);
        }

        private String genrateHash(String password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashPassword).Replace("-", "").ToLower();
            }
        }
    }
}
