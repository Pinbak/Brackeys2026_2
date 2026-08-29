namespace Brackeys2026_2.Game.Classes;

public class Suspicion
{
    public float Trust { get; private set; }
    private readonly Dictionary<Faction, float> _actualValues = new();

    public Suspicion()
    {
        foreach (var faction in Faction.All)
        {
            _actualValues.Add(faction, 0f);
        }
    }

    public void UpdateSuspicions(Faction faction, float increaseAmount, float decreaseAmount)
    {
        _actualValues[faction] += increaseAmount;
        foreach (var (key, value) in _actualValues)
        {
            if (key == faction) continue;
            _actualValues[key] -= decreaseAmount;
        }
    }

    public Faction GetLowest()
    {
        var lowest = _actualValues[Faction.All[0]];
        var lowestFaction = Faction.All[0];
        foreach (var (faction, value) in _actualValues)
        {
            if (!(value < lowest)) continue;
            lowest = value;
            lowestFaction = faction;
        }

        return lowestFaction;
    }

    public Faction GetHighest()
    {
        var highest = _actualValues[Faction.All[0]];
        var highestFaction = Faction.All[0];
        foreach (var (faction, value) in _actualValues)
        {
            if (!(value > highest)) continue;
            highest = value;
            highestFaction = faction;
        }

        return highestFaction;
    }

    public void SetActualValue(Faction toSet, float value)
    {
        _actualValues[toSet] = value;
    }
    
    public void RaiseTrust(float amount)
    {
        Trust += amount;
    }

    public void SetTrust(float amount)
    {
        Trust = amount;
    }
    
    public void LowerTrust(float amount)
    {
        Trust -= amount;
    }

    public void UpdateTrust(Faction lastPolicy, Faction playerFaction, float trustGain, float trustLoss)
    {
        if (lastPolicy == playerFaction)
        {
            Trust += trustGain;
        }

        else if (lastPolicy != playerFaction)
        {
            Trust -= trustLoss;
        }
    }
    
}