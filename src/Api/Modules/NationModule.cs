using Discord.Interactions;
using Discord.Rest;

namespace Api.Modules;

[Group(name: "nation", description: "Commands for fetching data related to a nation in CN")]
public class NationModule : RestInteractionModuleBase<RestInteractionContext>
{
    [SlashCommand(name: "search", description: "Attempts to find a single nation given a search parameter. If multiple matches are found then you can follow-up with the correct match")]
    public async Task Search(
        [Summary(name: "search-text", description: "Some text that should tie back to your nation. Currently supports: nation name, ruler name")] string searchText,
        [Summary(name: "nation-id", description: "The ID of the nation you want to find. Helpful if you don't want to deal with fuzzy searching")] int nationId = 0,
        [Summary(name: "ephemeral", description: "Determines whether the results are only visible to you (true) or visible to everyone in the channel (false). Defaults to false")] bool ephemeral = false
    )
    {
        
    }

    [SlashCommand(name: "history", description: "Searches all historical entries for a given nation")]
    public async Task History(
        [Summary(name: "nation-id", description: "The NationID of the nation that you want to view history for")] int nationId,
        [Summary(name: "updated-lower-bound", description: "Basically, how far back you want to search for updates to the nation")] DateTime? updatedLowerBound = null,
        [Summary(name: "updated-upper-bound", description: "When you want the search to stop looking for updates. Generally only useful for searching for updates in a specific date range")] DateTime? updatedUpperBound = null,
        [Summary(name: "limit", description: "How many historical records you want returned in the results. Note that the more you request, the worse the embed might look")] int limit = 10,
        [Summary(name: "offset", description: "How many results you want to skip before starting the search. Useful if you want to page through results instead of requesting all of them at once")] int offset = 0
    )
    {
        await RespondAsync("This command is not implemented yet. Please ping @lilweirdward to finish developing this.");
    }

    [SlashCommand(name: "range", description: "Finds all nations that are in a list of alliances and in range of the provided nation")]
    public async Task NationsInRange(
        [Summary(name: "nation-id", description: "The NationID of the nation that you want to view history for")] int nationId,
        [Summary(name: "alliance-names", description: "A list of alliance names that you want to search for targets in. The names must exactly match the full name in CN. Multiple names are supported as long as they are separated by commas")] string allianceNames
    )
    {
        await RespondAsync("This command is not implemented yet. Please ping @lilweirdward to finish developing this.");
    }
}