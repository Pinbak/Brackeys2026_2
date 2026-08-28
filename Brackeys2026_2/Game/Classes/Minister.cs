namespace Brackeys2026_2.Game.Classes;

public class Minister
{
    public string Name { get; set; }
    public Faction Faction { get; set; }
    public RoleType RoleType { get; set; }

    private static readonly Random Random = new();

    public Minister(RoleType roleType)
    {
        Name = GenerateName();
        Faction = new Faction(GetRandomFaction());
        RoleType = roleType;
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