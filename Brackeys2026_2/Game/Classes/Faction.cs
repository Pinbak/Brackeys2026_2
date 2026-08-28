namespace Brackeys2026_2.Game.Classes;

public class Faction
{
    public string Name { get; set; }
    public FactionType FactionType { get; set; }
    
    public Faction(FactionType factionType)
    {
        FactionType = factionType;
    }
}