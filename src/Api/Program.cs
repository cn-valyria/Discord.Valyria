using Discord.Rest;
using Api.Infrastructure;
using Discord.Interactions;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRouting();
builder.Services.AddGraphQL(builder.Configuration);
builder.Services.AddDI();

var discord = new DiscordRestClient();
await discord.LoginAsync(Discord.TokenType.Bot, builder.Configuration["token"]);

builder.Services.AddSingleton(discord);
builder.Services.AddInteractionService(config => config.UseCompiledLambda = true);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSerilogRequestLogging();

var commands = app.Services.GetRequiredService<InteractionService>();

await commands.AddModulesAsync(Assembly.GetExecutingAssembly(), app.Services);
await commands.RegisterCommandsToGuildAsync(app.Configuration.GetValue<ulong>("ccc_guild"));
await commands.RegisterCommandsToGuildAsync(app.Configuration.GetValue<ulong>("rfd_guild"));

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapWhen(ctx => ctx.Request.Path == "/interactions" && ctx.Request.Method == "POST", app => app.UseMiddleware<InteractionServiceMiddleware>(builder.Configuration["pbk"]));

app.Run();
