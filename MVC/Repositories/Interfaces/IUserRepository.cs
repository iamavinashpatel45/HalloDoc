using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        int countUsers(Func<User, bool> predicat);

        List<User> getAllUser(Func<User, bool> predicat,int skip);

        int getUserID(int aspNetUserID);

        Task<int> addUser(User user);

        User getUser(int aspNetUserID);

        Task<bool> updateProfile(User user);

        Admin getAdmionByAspNetUserId(int aspNetUserId);

        Physician getPhysicianByAspNetUserId(int aspNetUserId);

        Task<bool> addAdmin(Admin admin);

        List<Physician> getAllPhysicians();

        List<Physician> getAllPhysiciansByRegionId(int regionId);

        List<PhysicianRegion> getAllPhysicianRegionsByRegionId(int regionId);

        List<PhysicianRegion> getAllPhysicianRegionsByPhysicianId(int physicianId);

        List<PhysicianRegion> getAllPhysicianRegionsByAspNetUserIdWithRegionName(int aspNetUserId);

        List<Physician> getAllUnAssignedPhysician();

        PhysicianNotification getPhysicianNotification(int physicianId);

        Task<bool> updatePhysicianNotification(PhysicianNotification physicianNotification);

        Physician getPhysicianByPhysicianId(int physicianId);

        Physician getPhysicianWithAspNetUser(int physicianId);

        List<AdminRegion> getAdminRegionByAdminId(int adminId);

        Task<bool> updateAdmin(Admin admin);

        Task<bool> addAdminRgions(List<AdminRegion> adminRegions);

        Task<bool> deleteAdminRgions(List<AdminRegion> adminRegions);

        Task<bool> addPhysicianRegions(List<PhysicianRegion> physicianRegions);

        Task<bool> deletePhysicianRegions(List<PhysicianRegion> physicianRegions);

        Task<bool> addPhysician(Physician physician);

        Task<bool> updatePhysician(Physician physician);

        Task<bool> addPhysicianNotification(PhysicianNotification physicianNotification);

        List<PhysicianLocation> getAllProviderLocation();

        List<Physician> getAllPhysicianWithCurrentShift(int regionId);

    }
}
