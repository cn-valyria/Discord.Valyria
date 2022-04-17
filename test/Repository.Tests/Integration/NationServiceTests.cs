using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphQL.Client.Http;
using Repository.Services.Cybernations;
using System.Threading.Tasks;
using System.Linq;
using Repository.Services.Cybernations.Infrastructure;
using GraphQL.Client.Serializer.Newtonsoft;
using Repository.Services.Cybernations.Contracts;

namespace Repository.Tests.Integration;

[TestClass]
public class NationServiceTests
{
    private INationService _target;

    [TestInitialize]
    public void Initialize()
    {
        var graphQlClient = new GraphQLHttpClient("https://valyria-api-stats.azurewebsites.net/api/graphql", new NewtonsoftJsonSerializer(settings => 
        {
            foreach (var converter in settings.Converters.OfType<ConstantCaseEnumConverter>().ToList())
                settings.Converters.Remove(converter);

            settings.Converters.Add(new GraphQLEnumConverter());
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
        var searchResults = await _target.SearchNationsByRulerOrNationAsync(lilweirdward);

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
        var searchResults = await _target.SearchNationsByRulerOrNationAsync(searchText);

        // Assert
        Assert.IsTrue(searchResults.Count() > 1);
    }

    [TestMethod]
    public async Task SearchNationsInRangeAsync_Works_For_Real_Alliance()
    {
        // Arrange
        const string allianceName = "New Pacific Order";
        const decimal baseStrength = 100000;

        // Act
        var nationsInRange = await _target.SearchNationsInRangeAsync(allianceName, baseStrength * 0.75m, baseStrength * 1.33m);

        // Assert
        Assert.IsTrue(nationsInRange.Count() > 1); // I assume this will never not be true as long as CN exists lol
    }

    [TestMethod]
    public async Task SearchNationsInRangeAsync_Returns_Nothing_For_Garbage_AllianceName()
    {
        // Arrange
        const string allianceName = "junk"; // Assume that no one will ever create an alliance with this name lol
        const decimal baseStrength = 100000;

        // Act
        var nationsInRange = await _target.SearchNationsInRangeAsync(allianceName, baseStrength * 0.75m, baseStrength * 1.33m);

        // Assert
        Assert.IsTrue(nationsInRange.Count() == 0);
    }

    [TestMethod]
    public async Task SearchNationsInRangeAsync_Returns_Nothing_For_Impossible_Range()
    {
        // Arrange
        const string allianceName = "New Pacific Order";
        const decimal baseStrength = 1; // TODO: Actually this test should still return nations by rank, but /shrug

        // Act
        var nationsInRange = await _target.SearchNationsInRangeAsync(allianceName, baseStrength * 0.75m, baseStrength * 1.33m);

        // Assert
        Assert.IsTrue(nationsInRange.Count() == 0);
    }
}