using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class MinisterOfCommunication : Role
{
    private const int ConvertCost = 40;
    
    public MinisterOfCommunication(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        if (KillSomeone(thisPlayer)) return;
        if (ConvertIdeology(thisPlayer, ConvertCost)) return;
        if (BribeSomeone(thisPlayer)) return;
    }
}