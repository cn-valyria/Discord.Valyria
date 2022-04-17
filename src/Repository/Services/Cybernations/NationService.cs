using Entities;
using GraphQL.Client.Abstractions;
using Repository.Services.Cybernations.Contracts;

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
    allianceDate,
    strength,
    infrastructure,
    technology,
    baseLand,
    created,
    recentActivity,
    governmentType,
    religion,
    defcon,
    warStatus,
    team,
    baseSoldiers,
    tanks,
    cruiseMissiles,
    nukes
  }
}";

        var response = await _valyriaApiClient.SendQueryAsync<GetNationQuery>(query, new { nationId });
        return response.Data.GetNation;
    }

    public async Task<IEnumerable<Nation>> SearchNationsByRulerOrNationAsync(string searchText)
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
      allianceDate,
      strength,
      infrastructure,
      technology,
      baseLand,
      created,
      recentActivity,
      governmentType,
      religion,
      defcon,
      warStatus,
      team,
      baseSoldiers,
      tanks,
      cruiseMissiles,
      nukes
    }
  }
}";

        var response = await _valyriaApiClient.SendQueryAsync<SearchNationsQuery>(query, new { searchText });
        return response.Data.SearchNations.Results;
    }

    public async Task<IEnumerable<Nation>> SearchNationsInRangeAsync(string allianceName, decimal strengthLowerBound, decimal strengthUpperBound)
    {
        const string query = @"
query searchNationsByAllianceQuery($allianceName: String!, $strengthUpperBound: Decimal, $strengthLowerBound: Decimal) {
  searchNations(filter: { 
    allianceName: $allianceName, 
    nationStrengthUpperBound: $strengthUpperBound, 
    nationStrengthLowerBound: $strengthLowerBound,
    match: ALL
  }, limit: 500) {
    totalCount,
    results {
      id,
      name,
      rulerName,
      alliance { name },
        strength,
      infrastructure,
      technology,
      baseLand,
      warStatus,
      defcon,
      baseSoldiers,
      tanks,
      cruiseMissiles,
      nukes
    }
  }
}";

        var response = await _valyriaApiClient.SendQueryAsync<SearchNationsQuery>(query, new { allianceName, strengthLowerBound, strengthUpperBound });
        return response.Data.SearchNations.Results;
    }

// TODO: Finish this method
//     public Task GetNationHistoryAsync(int nationId)
//     {
//         const string query = @"
// query getNationQuery($nationId: ID) {
//   getNation(nationId: $nationId) {
//     name,
//     rulerName,
//     alliance { name },
//     allianceDate,
//     strength,
//     infrastructure,
//     technology,
//     baseLand,
//     created,
//     recentActivity,
//     governmentType,
//     religion,
//     defcon,
//     warStatus,
//     team,
//     baseSoldiers,
//     tanks,
//     cruiseMissiles,
//     nukes,
//     updatedOn,
//     auditHistory(limit: 10) {
//       alliance { name },
//       allianceDate,
//       strength,
//       infrastructure,
//       technology,
//       baseLand,
//       created,
//       recentActivity,
//       governmentType,
//       religion,
//       defcon,
//       warStatus,
//       team,
//       baseSoldiers,
//       tanks,
//       cruiseMissiles,
//       nukes,
//       updatedOn
//     }
//   }
// }";
//         throw new NotImplementedException();
//     }
}