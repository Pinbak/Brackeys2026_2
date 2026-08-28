using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Classes;

public class Minister
{
    public string Name { get; set; }
    public Faction Faction { get; set; }
    public RoleType RoleType { get; set; }
    public Texture Face { get; set; }

    private static readonly Random Random = new();

    public Minister(RoleType roleType, Texture face)
    {
        Name = GenerateName();
        Faction = new Faction(GetRandomFaction());
        RoleType = roleType;
        Face = face;
    }

    private string GenerateName()
    {
        return "T. Test";
    }

    private FactionType GetRandomFaction()
    {
        var i = Random.Next(0, 4);
        return (FactionType)i;
    }
}