using Discord.Interactions;
using Discord.Rest;

namespace Api.Modules;

public class PingModule : RestInteractionModuleBase<RestInteractionContext>
{
    [SlashCommand("ping", "Get pong")]
    public async Task Ping() => await RespondAsync("Pong!");
}