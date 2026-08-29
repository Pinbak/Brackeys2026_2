
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public class President(RoleType roleType, string name, GameManager gameManager) : Role(roleType, name, gameManager)
{
    private const float ReshuffleValue = -10f;
    
    public override void PerformAction(Minister thisPlayer)
    {
        // reshuffle cabinet
        if (GameManager.Turn != 0)
        {
            var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();

            if (thisPlayer.Suspicions[lowestTrust].Trust < ReshuffleValue)
            {
                var mostThreatening = thisPlayer.RankOrderThreats();
                // we don't know their role, so go off of assumption
                var belief = thisPlayer.Suspicions[lowestTrust].GetHighest();
                if (belief != thisPlayer.Faction)
                {
                    if (mostThreatening[0] == lowestTrust.Role.RoleType ||
                        mostThreatening[1] == lowestTrust.Role.RoleType)
                    {
                        // we swap with a lower threat role, as long as they are somewhat trustworthy
                        var leastThreateningRoles = thisPlayer.RankOrderLeastThreatening();
                        var leastThreateningMinisterByRole = thisPlayer.FindMinisterGivenRole(leastThreateningRoles[0]);
                        if (thisPlayer.Suspicions[leastThreateningMinisterByRole].Trust > 0f)
                        {
                            SwapMinisterRoles(lowestTrust, leastThreateningMinisterByRole);
                        }

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
        Console.WriteLine("Swapped ministers");
    }
    
}

public class PresidentsAide : Role
{
    public PresidentsAide(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}

public class MinisterOfNationalSecurity : Role
{
    public MinisterOfNationalSecurity(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}

public class SupremeCommander : Role
{
    public SupremeCommander(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}

public class SecretaryOfImmigration : Role
{
    public SecretaryOfImmigration(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
    }
}

public class MinisterOfCommunication : Role
{
    public MinisterOfCommunication(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}

public class MinisterOfInterior : Role
{
    public MinisterOfInterior(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}

public class SecretaryForPropaganda : Role
{
    public SecretaryForPropaganda(RoleType roleType, string name, GameManager gameManager) : base(roleType, name, gameManager)
    {
    }

    public override void PerformAction(Minister thisPlayer)
    {
        
    }
}