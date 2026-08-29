using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Classes;

public class Faction
{
    public string Name { get; set; }
    public string SecretName { get; set; }
    public FactionType FactionType { get; set; }
    public Texture Texture { get; set; }
    
    public Faction(FactionType factionType)
    {
        FactionType = factionType;
        switch (factionType)
        {
            case FactionType.Loyalist:
                Name = "Loyalist";
                SecretName = "Proudly a Loyalist";
                break;
            case FactionType.Technocrat:
                Name = "Technocrat";
                SecretName = "Secretly a Technocrat";
                break;
            case FactionType.Agrarian:
                Name = "Agrarian";
                SecretName = "Secretly an Agrarian";
                break;
            case FactionType.Federalist:
                Name = "Federalist";
                SecretName = "Secretly a Federalist";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(factionType), factionType, null);
        }
    }
    
    public static readonly Faction[] All =
    [
        new(FactionType.Loyalist),
        new(FactionType.Technocrat),
        new(FactionType.Agrarian),
        new(FactionType.Federalist)
    ];
}