using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class SupremeCommander : Role
{
    public SupremeCommander(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        if (KillSomeone(thisPlayer, 100, -20, true)) return;
        if (ConvertIdeology(thisPlayer)) return;
        if (BribeSomeone(thisPlayer)) return;
    }
}