using System.Text;
using Discord;
using Entities;

namespace Api.Helpers;

public static class EmbedBuilderExtensions
{
    public static Embed ToNationSearchSingleResultEmbed(this Nation nation) => new EmbedBuilder()
        .WithTitle($"Nation: {nation.RulerName} of {nation.Name}")
        .WithUrl($"https://www.cybernations.net/nation_drill_display.asp?Nation_ID={nation.Id}")
        .WithDescription($"Created on: {nation.Created:F}. Recent Activity: {nation.RecentActivity.ToString()}")
        .AddField("Alliance", $"{(nation.Alliance?.Name ?? "None")}, as of {nation.AllianceDate:F}")
        .AddField("Nation Strength", nation.Strength)
        .AddField("Infrastructure", nation.Infrastructure)
        .AddField("Technology", nation.Technology)
        .AddField("Base Land", nation.BaseLand)
        .AddField("Team Color", nation.Team)
        .AddField("Other Settings", $"Government Type: {nation.GovernmentType}, Religion: {nation.Religion}")
        .AddField("War Settings", $"War Status: {nation.WarStatus}, DEFCON: {nation.Defcon}")
        .AddField("Military", $"Soldiers: {nation.BaseSoldiers}, Tanks: {nation.Tanks}, CMs: {nation.CruiseMissiles}, Nukes: {nation.Nukes}")
        .WithColor(Color.Gold)
        .WithCurrentTimestamp()
        .Build();

    public static Embed ToNationSearchMultipleResultsEmbed(this IEnumerable<Nation> nations)
    {
        var embedBuilder = new EmbedBuilder()
            .WithTitle("Multiple nations found!")
            .WithDescription($"The search returned the following {nations.Count()} nations. Please search again with the specific ruler/nation name for more data on the desired nation.")
            .WithColor(Color.Blue)
            .WithCurrentTimestamp();

        foreach (var nation in nations)
            embedBuilder.AddField($"{nation.RulerName} of {nation.Name} ({nation.Id})", $"Alliance: {(nation.Alliance?.Name ?? "None")}, NS: {nation.Strength}");
        
        return embedBuilder.Build();
    }

    public static Embed ToNationsInRangeEmbed(this IEnumerable<Nation> nations, Nation nationRequested, string allianceInRangeOf)
    {
        var embedBuilder = new EmbedBuilder()
            .WithTitle("Results")
            .WithDescription($"The following {nations.Count()} nations were found in the {allianceInRangeOf} alliance in range of the nation you requested ({nationRequested.Name} of {nationRequested.RulerName}).")
            .WithColor(Color.Gold)
            .WithCurrentTimestamp();

        foreach (var nation in nations)
        {
            var nationFieldDescription = new StringBuilder()
                .AppendLine($"{new Emoji("\u2139")} Alliance: {(nation.Alliance?.Name ?? "None")}, NS: {nation.Strength}")
                .AppendLine($"{new Emoji("\uD83C\uDFF0")} Infra: {nation.Infrastructure}, Tech: {nation.Technology}, Land: {nation.BaseLand}")
                .AppendLine($"{new Emoji("\u2694")} Status: {nation.WarStatus.ToString()}, DEFCON: {nation.Defcon}, Soldiers: {nation.BaseSoldiers}, Tanks: {nation.Tanks}, CMs: {nation.CruiseMissiles}, Nukes: {nation.Nukes}")
                .ToString();

            embedBuilder.AddField($"{nation.RulerName} of {nation.Name} ({nation.Id})", nationFieldDescription);
        }

        return embedBuilder.Build();
    }
}