using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Classes;

public class Minister
{
    public string Name { get; set; }
    public Faction Faction { get; set; }
    public RoleType RoleType { get; set; }
    public Texture Face { get; set; }
    public bool IsPlayer { get; set; }
    
    private static string[] _firstNames = new[]
    {
        "A","B","C","D","E","F","G","M","O","R","S","T","W"
    };
    private static string[] _lastNames = new[]
    {
        "Adams", "Anderson", "Edwards", "Thomason"
    };

    private static readonly Random Random = new();

    public Minister(RoleType roleType, Texture face)
    {
        Name = GenerateName();
        Faction = new Faction(GetRandomFaction());
        RoleType = roleType;
        Face = face;
    }

    public string GetHumanReadableRole()
    {
        switch (RoleType)
        {
            case RoleType.President:
                return "President";
            case RoleType.MinisterOfCommunication:
                return "Minister of Communication";
            case RoleType.MinisterOfNationalSecurity:
                return "Minister of National Security";
            case RoleType.SupremeCommander:
                return "Supreme Commander";
            case RoleType.SecretaryOfImmigration:
                return "Secretary of Immigration";
            case RoleType.MinisterOfInterior:
                return "Minister of Interior";
            case RoleType.SecretaryOfLaw:
                return "Secretary of Law";
            case RoleType.MinisterOfFinance:
                return "Minister of Finance";
            case RoleType.SecretaryForStateAdvocacy:
                return "Secretary for StateAdvocacy";
            case RoleType.PresidentsAide:
                return "President's Aide";
            case RoleType.SovietOrganizer:
                return "Soviet Organizer";
            default:
                return "Unknow Role";
        }
    }

    private string GenerateName()
    {
        var fName = Random.Next(0, _firstNames.Length);
        var lName = Random.Next(0, _lastNames.Length);
        return $"{_firstNames[fName]}. {_lastNames[lName]}";
    }

    private FactionType GetRandomFaction()
    {
        var i = Random.Next(0, 4);
        return (FactionType)i;
    }
}