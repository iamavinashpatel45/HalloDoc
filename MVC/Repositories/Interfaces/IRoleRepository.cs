using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> getAllRoles();

        List<Role> getRolesByUserType(int type);

        Role getRoleByRoleId(int roleId);

        List<Menu> getAllMenus();

        List<Menu> getAllMenusByRole(int roleId);

        List<RoleMenu> getAllRoleMenusByRole(int roleId);

        Task<int> addRole(Role role);

        Task<bool> addRoleMenus(List<RoleMenu> roleMenus);

        Task<bool> updateRole(Role role);

        Task<bool> deleteRoleMenus(List<RoleMenu> roleMenus);
    }
}
