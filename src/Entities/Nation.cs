namespace Entities;

public record Nation(
    int Id,
    string Name,
    string RulerName,
    Alliance? Alliance,
    DateTime? AllianceDate,
    string AllianceStatus,
    GovernmentType GovernmentType,
    Religion Religion,
    Team Team,
    DateTime Created,
    decimal Technology,
    decimal Infrastructure,
    decimal BaseLand,
    NationalWarStatus WarStatus,
    int Votes,
    decimal Strength,
    int Defcon,
    int BaseSoldiers,
    int Tanks,
    int CruiseMissiles,
    int Nukes,
    RecentActivity RecentActivity,
    DateTime? UpdatedOn
);