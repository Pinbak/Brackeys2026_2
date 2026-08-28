namespace Brackeys2026_2.Game.Classes;

public class Faction
{
    public string Name { get; set; }
    public string SecretName { get; set; }
    public FactionType FactionType { get; set; }
    
    public Faction(FactionType factionType)
    {
        FactionType = factionType;
        switch (factionType)
        {
            case FactionType.Spots:
                Name = "Spots";
                SecretName = "Secretly supports spots";
                break;
            case FactionType.Stripes:
                Name = "Stripes";
                SecretName = "Secretly supports stripes";
                break;
            case FactionType.Squares:
                Name = "Squares";
                SecretName = "Secretly supports squares";
                break;
            case FactionType.Triangles:
                Name = "Triangles";
                SecretName = "Secretly supports triangles";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(factionType), factionType, null);
        }
    }
    
    public static readonly Faction[] All =
    [
        new(FactionType.Spots),
        new(FactionType.Stripes),
        new(FactionType.Squares),
        new(FactionType.Triangles)
    ];
}