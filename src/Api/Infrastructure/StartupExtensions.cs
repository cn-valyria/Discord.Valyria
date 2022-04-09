using Discord.Interactions;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Repository.Services.Cybernations;

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

    public static IServiceCollection AddGraphQL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGraphQLClient>(s => new GraphQLHttpClient(configuration["stats_api_url"], new SystemTextJsonSerializer()));

        return services;
    }

    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        services.AddScoped<INationService, NationService>();

        return services;
    }
}