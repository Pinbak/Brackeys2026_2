using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class MinisterOfNationalSecurity : Role
{
    private const int DisposalCost = 0;
    private const float EnoughToKill = 0;
    
    public MinisterOfNationalSecurity(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        // can kill people
        if (GameManager.Turn == 0) return;
        if (thisPlayer.PoliticalCapital < DisposalCost) return;
            
        var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();
        if (lowestTrust == Minister.President) return;
        var trust = thisPlayer.Suspicions[lowestTrust].Trust;
        if (trust < EnoughToKill)
        {
            GameManager.KillMinister(lowestTrust);
            thisPlayer.DecreasePoliticalCapital(DisposalCost);
        }
    }
}