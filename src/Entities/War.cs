namespace Entities;

public record War (
    int Id,
    Nation AttackingNation,
    Alliance AttackingAlliance,
    Team AttackingTeam,
    decimal AttackingDestruction,
    Nation DefendingNation,
    Alliance DefendingAlliance,
    Team DefendingTeam,
    decimal DefendingDestruction,
    WarStatus WarStatus,
    DateTime DeclaredOn,
    DateTime ExpiresOn,
    string Reason
);