using Entities;

namespace Repository.Services.Cybernations.Contracts;

public class SearchNationsQuery
{
    public SearchResultSet<Nation> SearchNations { get; set; }
}