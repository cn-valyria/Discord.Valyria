using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Repository.Services.Cybernations;
using System.Threading.Tasks;
using System.Linq;

namespace Repository.Tests.Integration;

[TestClass]
public class NationServiceTests
{
    private INationService _target;

    [TestInitialize]
    public void Initialize()
    {
        var graphQlClient = new GraphQLHttpClient("https://valyria-api-stats.azurewebsites.net/api/graphql", new SystemTextJsonSerializer(options => 
        {
            options.PropertyNameCaseInsensitive = true;
        }));

        _target = new NationService(graphQlClient);
    }

    [TestMethod]
    public async Task GetNationAsync_Works_For_Valid_Nation()
    {
        // Arrange
        const int lilweirdwardNationId = 552285;

        // Act
        var nation = await _target.GetNationAsync(lilweirdwardNationId);

        // Assert
        Assert.AreEqual(lilweirdwardNationId, nation.Id);
    }

    [TestMethod]
    public async Task SearchNationsAsync_Works_For_One_Result()
    {
        // Arrange
        const string lilweirdward = nameof(lilweirdward);

        // Act
        var searchResults = await _target.SearchNationsAsync(lilweirdward);

        // Assert
        Assert.AreEqual(1, searchResults.Count());
        Assert.AreEqual(lilweirdward, searchResults.First().RulerName);
    }

    [TestMethod]
    public async Task SearchNationsAsync_Works_For_Multiple_Results()
    {
        // Arrange
        const string searchText = "lil";

        // Act
        var searchResults = await _target.SearchNationsAsync(searchText);

        // Assert
        Assert.IsTrue(searchResults.Count() > 1);
    }
}