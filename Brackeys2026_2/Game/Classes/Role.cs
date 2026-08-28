using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public abstract class Role
{
    public RoleType RoleType { get; set; }
    protected Minister LastTurn { get; set; }
    protected Faction LastPolicy { get; set; }

    protected const float GenericSuspicionIncrease = 10f;
    protected const float PresidentialSuspicionIncrease = 2f;
    protected const float GenericSuspicionDecrease = 2f;
    protected const float PresidentialSuspicionDecrease = 1f;
    private const float SuspicionThreshold = 20f;
    private const float TrustGain = 1f;

    public string Name { get; }

    public virtual void PerformAction(Minister thisPlayer)
    {
        if (GameManager.Turn == 0) return;
        (LastTurn, LastPolicy) = GameManager.PreviouslyImplementedPolicy;
        if (thisPlayer.IsPlayer) return;
        thisPlayer.Suspicions[LastTurn]
            .UpdateSuspicions(LastPolicy, GenericSuspicionIncrease, GenericSuspicionDecrease);

        thisPlayer.Suspicions[LastTurn].UpdateTrust(LastPolicy, thisPlayer.Faction, TrustGain);

        if (thisPlayer.Role.RoleType is RoleType.President) return;
        thisPlayer.Suspicions[Minister.President]
            .UpdateSuspicions(LastPolicy, PresidentialSuspicionIncrease, PresidentialSuspicionDecrease);
    }

    protected Role(RoleType roleType, string name)
    {
        RoleType = roleType;
        Name = name;
    }

    public static Role GetRoleFromRoleType(RoleType roleType)
    {
        return roleType switch
        {
            RoleType.President => new President(roleType, "President"),
            RoleType.PresidentsAide => new PresidentsAide(roleType, "President's Aide"),
            RoleType.MinisterOfNationalSecurity => new MinisterOfNationalSecurity(roleType, "Minister of National Security"),
            RoleType.SupremeCommander => new SupremeCommander(roleType, "Supreme Commander"),
            RoleType.SecretaryOfImmigration => new SecretaryOfImmigration(roleType, "Secretary of Immigration"),
            RoleType.MinisterOfCommunication => new MinisterOfCommunication(roleType, "Minister of Communication"),
            RoleType.MinisterOfInterior => new MinisterOfInterior(roleType, "Minister of Interior"),
            RoleType.SecretaryForPropaganda => new SecretaryForPropaganda(roleType, "Secretary for Propaganda"),
            _ => throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null)
        };
    }
}