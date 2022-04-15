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
                .AddField("Alliance", src.Alliance?.Name ?? "None")
                .AddField("Nation Strength", src.Strength)
                .AddField("Infrastructure", src.Infrastructure)
                .AddField("Technology", src.Technology)
                .WithColor(Color.Gold)
                .WithCurrentTimestamp()
                .Build();
        });
    }
}