namespace Entities;

public record Aid (
    int Id,
    Nation SendingNation,
    Alliance SendingAlliance,
    Team SendingTeam,
    Nation ReceivingNation,
    Alliance ReceivingAlliance,
    Team ReceivingTeam,
    AidStatus Status,
    int Money,
    int Technology,
    int Soldiers,
    DateTime SentOn,
    string Reason
);