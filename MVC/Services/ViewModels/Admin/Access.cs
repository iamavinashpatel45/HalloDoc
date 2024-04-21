using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class Access
    {
        public List<AccessTable> RolesData;
    }

    public class AccessTable
    {
        public string Name { get; set; }

        public int RoleId { get; set; }

        public string AccountType { get; set; }
    }

    public class CreateRole
    {
        public bool IsUpdate { get; set; } = false;

        [StringLength(50)]
        public string RoleName { get; set; }

        public int SlectedAccountType { get; set; }

        public RolesCheckBox RolesCheckBox { get; set; }

        public List<int> SelectedMenus { get; set; }
    }

    public class RolesCheckBox
    {
        public List<int> SelectedMenusForLoadData { get; set; }

        public Dictionary<int, string>? Menus { get; set; }
    }
}
