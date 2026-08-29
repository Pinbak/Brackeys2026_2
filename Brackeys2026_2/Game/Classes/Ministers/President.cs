using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public class President(RoleType roleType, string name, GameManager gameManager) : Role(roleType, name, gameManager)
{
    private const float ReshuffleValue = -10f;
    
    public override void PerformAction(Minister thisPlayer)
    {
        // reshuffle cabinet
        if (GameManager.Turn == 0) return;
        var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();

        if (thisPlayer.Suspicions[lowestTrust].Trust < ReshuffleValue)
        {
            var mostThreatening = thisPlayer.RankOrderThreats();
            // we don't know their role, so go off of assumption
            var belief = thisPlayer.Suspicions[lowestTrust].GetHighest();
            if (belief != thisPlayer.Faction)
            {
                if (mostThreatening[0] == lowestTrust.Role.RoleType ||
                    mostThreatening[1] == lowestTrust.Role.RoleType ||
                    mostThreatening[2] == lowestTrust.Role.RoleType)
                {
                    // we swap with a lower threat role, as long as they are somewhat trustworthy
                    var leastThreateningRoles = thisPlayer.RankOrderLeastThreatening();
                    var leastThreateningMinisterByRole = thisPlayer.FindMinisterGivenRole(leastThreateningRoles[0]);
                    if (thisPlayer.Suspicions[leastThreateningMinisterByRole].Trust > 0f)
                    {
                        SwapMinisterRoles(lowestTrust, leastThreateningMinisterByRole);
                        return;
                    }
                        
                    leastThreateningMinisterByRole = thisPlayer.FindMinisterGivenRole(leastThreateningRoles[1]);
                    if (thisPlayer.Suspicions[leastThreateningMinisterByRole].Trust > 0f)
                    {
                        SwapMinisterRoles(lowestTrust, leastThreateningMinisterByRole);
                    }

                }
            }
        }
    }
    
    private void SwapMinisterRoles(Minister a, Minister b)
    {
        var currentARole = a.Role;
        var currentBRole = b.Role;
        a.Role = currentBRole;
        b.Role = currentARole;
        
        // swap order
        var positionInListA = GameManager.CurrentMinisters.FindIndex(m => m == a);
        var positionInListB = GameManager.CurrentMinisters.FindIndex(m => m == b);
        (GameManager.CurrentMinisters[positionInListA], GameManager.CurrentMinisters[positionInListB]) = (
            GameManager.CurrentMinisters[positionInListB], GameManager.CurrentMinisters[positionInListA]);
        
        GameManager.UpdatePositions();
        
    }
    
}