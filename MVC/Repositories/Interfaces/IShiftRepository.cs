using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IShiftRepository
    {
        List<Physician> getPhysicianWithShiftDetailByRegionIdAndDAte(int regionId, DateTime startDate, DateTime endDate);

        List<ShiftDetail> getShiftDetailByPhysicianIdAndDAte(int aspNetUserId, DateTime startDate, DateTime endDate);

        List<ShiftDetail> getShiftDetailByRegionIdAndDAte(int regionId, DateTime startDate,DateTime endDate);

        List<ShiftDetail> getAllShiftDetailsFromShiftId(int shiftId);

        Task<bool> addShift(Shift shift);

        Task<bool> addShiftDetails(List<ShiftDetail> shiftDetails);

        Task<bool> addShiftDetailsRegion(List<ShiftDetailRegion> shiftDetailRegions);

        Task<bool> updateShiftDetails(List<ShiftDetail> shiftDetails);

        Task<bool> updateShiftDetailRegions(List<ShiftDetailRegion> shiftDetailRegions);

        ShiftDetail getShiftDetails(int shiftDetailsId);

        ShiftDetail getShiftDetailsWithPhysician(int shiftDetailsId);

        ShiftDetailRegion getShiftDetailRegion(int shiftDetailsId);

        List<ShiftDetail> getAllShiftDetails(int regionId, bool isThisMonth, DateTime date, int skip);

        int countAllShiftDetails(int regionId, bool isThisMonth, DateTime date);
    }
}