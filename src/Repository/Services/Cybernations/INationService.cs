using Entities;

namespace Repository.Services.Cybernations;

public interface INationService
{
    Task<Nation> GetNationAsync(int nationId);
    Task<IEnumerable<Nation>> SearchNationsAsync(string searchText);
}