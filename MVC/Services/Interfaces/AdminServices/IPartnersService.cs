using Services.ViewModels.Admin;

namespace Services.Interfaces.AdminServices
{
    public interface IPartnersService
    {
        Partners getPartnersData();

        List<PartnersTableData> getPartnersTableDatas(int regionId, String searchElement);

        BusinessProfile addBusiness(bool isUpdate,int venderId);

        Task<bool> createBusiness(BusinessProfile businessProfile);

        Task<bool> EditBusiness(BusinessProfile businessProfile,int venderId);

        Task<bool> deleteBusiness(int venderId);
    }
}
