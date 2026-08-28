using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Classes;

public class Minister : IHasTurn
{
    public string Name { get; set; }
    public Faction Faction { get; set; }
    public Role Role { get; set; }
    public Texture Face { get; set; }
    public bool IsPlayer { get; set; }
    public bool IsTurn { get; set; }

    public Dictionary<Minister, Suspicion> Suspicions = new();
    
    public static Minister President { get; set; }
    
    private static readonly string[] FirstNames =
    [
        "A","B","C","D","E","F","G","M","O","R","S","T","W"
    ];
    private static readonly string[] LastNames =
    [
        "Adams", "Anderson", "Edwards", "Thomason"
    ];

    private static readonly Random Random = new();

    public Minister(Role role, Texture face)
    {
        if (role.RoleType is RoleType.President) President = this;
        Name = GenerateName();
        Faction = GetRandomFaction();
        Role = role;
        Face = face;
    }

    private string GenerateName()
    {
        var fName = Random.Next(0, FirstNames.Length);
        var lName = Random.Next(0, LastNames.Length);
        return $"{FirstNames[fName]}. {LastNames[lName]}";
    }

    private Faction GetRandomFaction()
    {
        var i = Random.Next(0, Faction.All.Length);
        return Faction.All[i];
    }

    public string GetGoal()
    {
        return $"Have every minister support {Faction.Name.ToLower()}.";
    }

    public string GetActionText()
    {
        return Role.RoleType switch
        {
            RoleType.President => "Can reshuffle cabinet, has final vote.",
            RoleType.MinisterOfNationalSecurity => "'Dispose' of target.",
            RoleType.SupremeCommander => "Stage a coup against the president (become president).",
            RoleType.SecretaryOfImmigration => "Choose a new minister on a peer's death.",
            RoleType.MinisterOfCommunication => "Change target's ideology.",
            RoleType.PresidentsAide => "President's hand is always exposed.",
            RoleType.MinisterOfInterior => "Expose actual ideology or target.",
            RoleType.SecretaryForPropaganda => "Convince target of ideology.",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void PopulateSuspicions(List<Minister> currentMinisters)
    {
        foreach (var currentMinister in currentMinisters)
        {
            if (currentMinister == this) continue; // don't add yourself
            Suspicions.Add(currentMinister, new Suspicion());
        }
    }

    public virtual void BeginTurn()
    {
        Role.PerformAction(this);
    }
}