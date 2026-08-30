using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes.Ministers;

public abstract class Role
{
    public RoleType RoleType { get; set; }
    protected GameManager GameManager { get; set; }
    private static readonly Random Random = new();
    
    public string Name { get; }

    public abstract void PerformAction(Minister thisPlayer);
    
    protected Role(RoleType roleType, string name, GameManager gameManager)
    {
        RoleType = roleType;
        GameManager = gameManager;
        Name = name;
    }
    
    protected bool KillSomeone(Minister thisPlayer, int disposalCost = 100, float enoughToKill = -10f, bool canKillPresident = false)
    {
        // can kill people
        if (GameManager.Turn == 0) return false;
        if (thisPlayer.PoliticalCapital < disposalCost) return false;
            
        var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();
        if (lowestTrust == Minister.President && !canKillPresident) return false;
        var trust = thisPlayer.Suspicions[lowestTrust].Trust;
        if (trust < enoughToKill)
        {
            GameManager.KillMinister(lowestTrust);
            thisPlayer.DecreasePoliticalCapital(disposalCost);
            return true;
        }

        return false;
    }

    protected bool BribeSomeone(Minister thisPlayer, int bribeCost = 30, float enoughToBribe = -5f)
    {
        if (GameManager.Turn == 0) return false;
        if (thisPlayer.PoliticalCapital < bribeCost) return false;
        
        var choice = Random.Next(0, 3);
        if (choice == 0)
        {
            var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();
            var trust = thisPlayer.Suspicions[lowestTrust].Trust;
            if (trust < enoughToBribe)
            {
                GameManager.BribeMinister(thisPlayer, lowestTrust, bribeCost);
                thisPlayer.DecreasePoliticalCapital(bribeCost);
                return true;
            }
        }
        else if (choice == 1)
        {
            foreach (var (minister, suspicion) in thisPlayer.Suspicions)
            {
                var highest = suspicion.GetHighest();
                if (highest == thisPlayer.Faction)
                {
                    GameManager.BribeMinister(thisPlayer, minister, bribeCost);
                    thisPlayer.DecreasePoliticalCapital(bribeCost);
                    return true;
                }
            }
        }

        return false;
    }

    protected bool ConvertIdeology(Minister thisPlayer, int convertCost = 60)
    {
        if (GameManager.Turn == 0) return false;
        if (thisPlayer.PoliticalCapital < convertCost) return false;

        var choice = Random.Next(0, 2);

        if (choice == 0)
        {
            var lowestTrust = thisPlayer.GetMinisterWhomLeastTrusted();
            if (thisPlayer.Suspicions[lowestTrust].GetHighest() != thisPlayer.Faction)
            {
                GameManager.ConvertIdeology(thisPlayer, lowestTrust);
                thisPlayer.DecreasePoliticalCapital(convertCost);
                return true;
            }
        }

        return false;

    }
}