using Entities;

namespace Repository.Services.Cybernations;

public interface INationService
{
    Task<Nation> GetNationAsync(int nationId);
    Task<IEnumerable<Nation>> SearchNationsByRulerOrNationAsync(string searchText);
    Task<IEnumerable<Nation>> SearchNationsInRangeAsync(string allianceName, decimal strengthLowerBound, decimal strengthUpperBound);
}