using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class MinisterOfNationalSecurity : Role
{
    private const int DisposalCost = 50;
    private const float EnoughToKill = -20f;
    
    public MinisterOfNationalSecurity(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        KillSomeone(thisPlayer, DisposalCost, EnoughToKill);
    }
}