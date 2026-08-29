using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class MinisterOfInterior : Role
{
    private const int BribeCost = 15;
    
    public MinisterOfInterior(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        if (KillSomeone(thisPlayer)) return;
        if (ConvertIdeology(thisPlayer)) return;
        if (BribeSomeone(thisPlayer, BribeCost)) return;
    }
}