using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IEncounterRepository
    {
        Encounter getEncounter(int requestId);

        Task<bool> addEncounter(Encounter encounter);

        Task<bool> updateEncounter(Encounter encounter);
    }
}
