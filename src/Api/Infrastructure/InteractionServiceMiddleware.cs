using Discord.Interactions;
using Discord.Rest;

namespace Api.Infrastructure;

public sealed class InteractionServiceMiddleware
{
    private readonly DiscordRestClient _discord;
    private readonly InteractionService _interactions;
    private readonly string _pbk;
    private readonly IServiceProvider _serviceProvider;
    private readonly RequestDelegate _next;

    public InteractionServiceMiddleware(
        DiscordRestClient discord,
        InteractionService interactions,
        string pbk,
        IServiceProvider serviceProvider,
        RequestDelegate next
    )
    {
        _discord = discord;
        _interactions = interactions;
        _pbk = pbk;
        _serviceProvider = serviceProvider;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        async Task RespondAsync(int statusCode, string responseBody)
        {
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(responseBody).ConfigureAwait(false);
            await httpContext.Response.CompleteAsync().ConfigureAwait(false);
        }

        var signature = httpContext.Request.Headers["X-Signature-Ed25519"];
        var timestamp = httpContext.Request.Headers["X-Signature-Timestamp"];
        using var sr = new StreamReader(httpContext.Request.Body);
        var body = await sr.ReadToEndAsync();

        if (!_discord.IsValidHttpInteraction(_pbk, signature, timestamp, body))
        {
            await RespondAsync(StatusCodes.Status400BadRequest, "Invalid interaction signature!");
            return;
        }

        var interaction = await _discord.ParseHttpInteractionAsync(_pbk, signature, timestamp, body);

        if (interaction is RestPingInteraction pingInteraction)
        {
            await RespondAsync(StatusCodes.Status200OK, pingInteraction.AcknowledgePing());
            return;
        }

        var interactionCtx = new RestInteractionContext(_discord, interaction, (str) => RespondAsync(StatusCodes.Status200OK, str));

        var result = await _interactions.ExecuteCommandAsync(interactionCtx, _serviceProvider);
    }
}