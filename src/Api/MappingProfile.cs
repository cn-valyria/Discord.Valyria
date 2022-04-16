using AutoMapper;
using Discord;
using Entities;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Nation, Embed>().ConvertUsing((src, dest) => 
        {
            return new EmbedBuilder()
                .WithTitle($"Nation: {src.RulerName} of {src.Name}")
                .WithUrl($"https://www.cybernations.net/nation_drill_display.asp?Nation_ID={src.Id}")
                .WithDescription($"Created on: {src.Created:F}. Recent Activity: {src.RecentActivity.ToString()}")
                .AddField("Alliance", $"{(src.Alliance?.Name ?? "None")}, as of {src.AllianceDate:F}")
                .AddField("Nation Strength", src.Strength)
                .AddField("Infrastructure", src.Infrastructure)
                .AddField("Technology", src.Technology)
                .AddField("Base Land", src.BaseLand)
                .AddField("Team Color", src.Team)
                .AddField("Other Settings", $"Government Type: {src.GovernmentType}, Religion: {src.Religion}")
                .AddField("War Settings", $"War Status: {src.WarStatus}, DEFCON: {src.Defcon}")
                .AddField("Military", $"Soldiers: {src.BaseSoldiers}, Tanks: {src.Tanks}, CMs: {src.CruiseMissiles}, Nukes: {src.Nukes}")
                .WithColor(Color.Gold)
                .WithCurrentTimestamp()
                .Build();
        });

        CreateMap<IEnumerable<Nation>, Embed>().ConvertUsing((src, dest) => 
        {
            var embedBuilder = new EmbedBuilder()
                .WithTitle("Multiple nations found!")
                .WithDescription($"The search returned the following {src.Count()} nations. Please search again with the specific ruler/nation name for more data on the desired nation.")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp();

            foreach (var nation in src)
                embedBuilder.AddField($"{nation.RulerName} of {nation.Name} ({nation.Id})", $"Alliance: {(nation.Alliance?.Name ?? "None")}, NS: {nation.Strength}");
            
            return embedBuilder.Build();
        });
    }
}