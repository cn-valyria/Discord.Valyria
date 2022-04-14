using Discord.Interactions;
using Discord.Rest;
using Entities;
using Repository.Services.Cybernations;

namespace Api.Modules;

[Group(name: "nation", description: "Commands for fetching data related to a nation in CN")]
public class NationModule : RestInteractionModuleBase<RestInteractionContext>
{
    private readonly INationService _nationService;
    private readonly ILogger<NationModule> _logger;

    public NationModule(INationService nationService, ILogger<NationModule> logger) 
        => (_nationService, _logger) = (nationService, logger);

    [SlashCommand(name: "search", description: "Attempts to find a single nation given a search parameter or nation ID")]
    public async Task Search(
        [Summary(name: "search-text", description: "Some text that should tie back to your nation. Currently supports: nation name, ruler name")] string searchText,
        [Summary(name: "nation-id", description: "The ID of the nation you want to find. Helpful if you don't want to deal with fuzzy searching")] int nationId = 0,
        [Summary(name: "ephemeral", description: "Determines whether the results are only visible to you (true) or visible to everyone (false)")] bool ephemeral = false
    )
    {
        _logger.LogInformation("NationModule command Search executed with parameters: {search-text}, {nation-id}, {ephemeral}", searchText, nationId, ephemeral);

        var nation = await QueryNation();
        _logger.LogInformation("Found nation in search: {nation}", nation);

        if (nation is null)
            return;

        await RespondAsync($"Found nation {nation.Id}, name {nation.Name}, ruler {nation.RulerName}, at {nation.Strength} NS");

        async Task<Nation?> QueryNation()
        {
            if (nationId > 0)
                return await _nationService.GetNationAsync(nationId);
            else 
            {
                var searchResults = await _nationService.SearchNationsAsync(searchText);
                _logger.LogInformation("Search returned {resultCount} search results", searchResults.Count());

                if (searchResults.Count() == 1)
                    return searchResults.First();

                await FollowupAsync("Search returned too many nations. Please try again");
                return null;
            }
        }
    }

    [SlashCommand(name: "history", description: "Searches all historical entries for a given nation")]
    public async Task History(
        [Summary(name: "nation-id", description: "The NationID of the nation that you want to view history for")] int nationId,
        [Summary(name: "updated-lower-bound", description: "Basically, how far back you want to search for updates to the nation")] DateTime? updatedLowerBound = null,
        [Summary(name: "updated-upper-bound", description: "When you want the search to stop looking for updates")] DateTime? updatedUpperBound = null,
        [Summary(name: "limit", description: "How many historical records you want returned in the results")] int limit = 10,
        [Summary(name: "offset", description: "How many results you want to skip before starting the search")] int offset = 0
    )
    {
        await RespondAsync("This command is not implemented yet. Please ping @lilweirdward to finish developing this.");
    }

    [SlashCommand(name: "range", description: "Finds all nations that are in a list of alliances and in range of the provided nation")]
    public async Task NationsInRange(
        [Summary(name: "nation-id", description: "The NationID of the nation that you want to view history for")] int nationId,
        [Summary(name: "alliance-names", description: "A list of alliance names that you want to search for targets in")] string allianceNames
    )
    {
        await RespondAsync("This command is not implemented yet. Please ping @lilweirdward to finish developing this.");
    }
}