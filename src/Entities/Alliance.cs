namespace Entities;

public record Alliance (
    int Id,
    string Name,
    DateTime Updated,
    int TotalNations,
    int ActiveNations,
    int PercentActive,
    int TotalStrength,
    int AverageStrength,
    decimal Score,
    int TotalLand,
    int TotalInfrastructure,
    int TotalTechnology,
    int NationsAtWar,
    int NationsAtPeace,
    int TotalSoldiers,
    int TotalTanks,
    int TotalCruiseMissiles,
    int TotalNuclearWeapons,
    int TotalAircraft,
    int TotalNavy,
    int TotalNationsInAnarchy
);