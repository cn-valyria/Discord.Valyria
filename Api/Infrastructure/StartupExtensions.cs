using Discord.Interactions;

namespace Api.Infrastructure;

public static class StartupExtensions
{
    public static IServiceCollection AddInteractionService(this IServiceCollection services, Action<InteractionServiceConfig> configure)
    {
        var config = new InteractionServiceConfig();
        configure(config);
        config.DefaultRunMode = RunMode.Sync;

        services.AddSingleton(config);
        services.AddSingleton<InteractionService>();

        return services;
    }
}