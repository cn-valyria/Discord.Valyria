using Entities;
using GraphQL.Client.Abstractions;

namespace Repository.Services.Cybernations;

public class NationService : INationService
{
    private readonly IGraphQLClient _valyriaApiClient;

    public NationService(IGraphQLClient client) => _valyriaApiClient = client;

    public async Task<Nation> GetNationAsync(int nationId)
    {
        const string query = @"
query getNationQuery($nationId: ID) {
  getNation(nationId: $nationId) {
    id,
    name,
    rulerName,
    alliance {
      name
    },
    strength,
    infrastructure,
    technology
  }
}";

        var response = await _valyriaApiClient.SendQueryAsync<Nation>(query, new { nationId });
        return response.Data;
    }

    public async Task<IEnumerable<Nation>> SearchNationsAsync(string searchText)
    {
        const string query = @"
query searchNationQuery($searchText: String) {
  searchNations(filter: {
    nationName: $searchText,
    rulerName: $searchText,
    match: ANY
  }) {
    totalCount,
    results {
      id,
      name,
      rulerName,
      alliance {
        name
      },
      strength,
      infrastructure,
      technology
    }
  }
}";

        var response = await _valyriaApiClient.SendQueryAsync<IEnumerable<Nation>>(query, new { searchText }); // TODO: Use wrapper class
        return response.Data;
    }
}