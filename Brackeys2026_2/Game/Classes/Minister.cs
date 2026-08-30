using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Classes.Ministers;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public class Minister : IHasTurn
{
    public string Name { get; set; }
    public Faction Faction { get; set; }
    public Role Role { get; set; }
    public Texture Face { get; set; }
    public bool IsPlayer { get; set; }
    public bool IsTurn { get; set; }
    
    public int PoliticalCapital { get; set; }

    public readonly Dictionary<Minister, Suspicion> Suspicions = new();
    public readonly Dictionary<FactionType, int> Implemented = new();
    public Perception Perception { get; set; } = new();
    public GameManager GameManager { get; set; }

    public static Minister President { get; private set; } = null!;

    private static readonly string[] FirstNames =
    [
        "A", "B", "C", "D", "E", "F", "G", "M", "O", "R", "S", "T", "W", "Z", "Y", "Z", "Y"
    ];

    private static readonly string[] LastNames =
    [
        "Smirnov", "Ivanov", "Sokolov", "Popov", "Lebedev", "Kozlov", "Novikov", "Morozov", "Solovyov", "Volkov",
        "Zaytsev", "Melnyk", "Shevchenko", "Kovalenko", "Bondarenko", "Boyko", "Oliynyk", "Ivanenko", "Nazarenko"
    ];

    private static readonly Random Random = new();
    private const float Riskiness = 4f;
    private const float GenericSuspicionIncrease = 5f;
    private const float PresidentialSuspicionIncrease = 2f;
    private const float GenericSuspicionDecrease = 2f;
    private const float PresidentialSuspicionDecrease = 1f;
    private const float SuspicionThreshold = 20f;
    private const float TrustGain = 5f;
    private const float TrustLoss = 1f;
    protected Minister? LastTurn { get; set; }
    protected Faction LastPolicy { get; set; }

    public Minister(Role role, Texture face, GameManager gameManager)
    {
        if (role.RoleType is RoleType.President) President = this;
        Name = GenerateName();
        Faction = GetRandomFaction();
        Role = role;
        Face = face;
        GameManager = gameManager;
        foreach (var faction in Faction.All)
        {
            Implemented.Add(faction.FactionType, 0);
        }
    }

    private void UpdatePerceptions()
    {
        if (GameManager.Turn == 0) return;
        // perceptions of yourself (internal reflection)
        (LastTurn, LastPolicy) = GameManager.PreviouslyImplementedPolicy;
        Perception.UpdateBeliefs(LastPolicy);
    }
    
    public void CalculateSuspicions()
    {
        if (LastTurn is null) return;
        (LastTurn, LastPolicy) = GameManager.PreviouslyImplementedPolicy;
        if (LastTurn == this) return;
        
        // suspicions of other players
        Suspicions[LastTurn].UpdateSuspicions(LastPolicy, GenericSuspicionIncrease, GenericSuspicionDecrease);

        Suspicions[LastTurn].UpdateTrust(LastPolicy, Faction, TrustGain, TrustLoss);

        if (Role.RoleType is RoleType.President) return;
        Suspicions[President].UpdateSuspicions(LastPolicy, PresidentialSuspicionIncrease, PresidentialSuspicionDecrease);
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
        return Faction.FactionType switch
        {
            FactionType.Loyalist => "You win if all members of the cabinet are Loyalist. Glory to our leader!",
            FactionType.Technocrat => "You win if all members of the cabinet are Technocrats. Embrace technology and change!",
            FactionType.Agrarian => "You win if all members of the cabinet are Agrarian. The old ways are what made our country great!",
            FactionType.Federalist => "You win if all members of the cabinet are Federalists. Long live democracy!",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public string GetActionText()
    {
        return Role.RoleType switch
        {
            RoleType.President => "Can reshuffle cabinet, has final vote.",
            RoleType.MinisterOfNationalSecurity => "Removing a minister costs 50% less political capital.",
            RoleType.SupremeCommander => "Can 'dispose' of the president.",
            RoleType.SecretaryOfImmigration => "None",
            RoleType.MinisterOfCommunication => "Converting someone's ideology costs 30% less political capital.",
            RoleType.PresidentsAide => "None",
            RoleType.MinisterOfInterior => "None",
            RoleType.SecretaryForPropaganda => "Bribing someone costs 50% less political capital.",
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
        UpdatePerceptions();
        Role.PerformAction(this);
        EndAiTurn();
    }

    private void EndAiTurn()
    {
        if (Role.RoleType is RoleType.President)
        {
            var card = SelectOneOfThreeCards();
            GameManager.EndTurn(card);
        }
        else
        {
            var (a, b) = SelectTwoOfThreeCards();
            var card = President.SelectOneOfTwoCards(this, a, b);
            GameManager.EndTurn(card);
        }
    }

    public void EndPlayerTurn(Faction cardA, Faction cardB)
    {
        var card = President.SelectOneOfTwoCards(this, cardA, cardB);
        GameManager.EndTurn(card);
    }

    private Faction SelectOneOfThreeCards()
    {
        var cards = GameManager.ThisTurnsCards;
        foreach (var faction in cards)
        {
            if (faction == Faction)
            {
                if (!IsPerceivedAThreat())
                {
                    return faction;
                }
            }
            
            // otherwise try and side with the majority
            else if (PollBestPosition() == faction)
            {
                return faction;
            }
            
        }

        return cards[0];
    }

    private (Faction a, Faction b) SelectTwoOfThreeCards()
    {
        var cards = GameManager.ThisTurnsCards;
        var chosenCards = new List<Faction>();
        foreach (var faction in cards)
        {
            if (faction == Faction)
            {
                if (!IsPerceivedAThreat() || IsPresidentTrustworthy())
                {
                    chosenCards.Add(faction);
                }
            }
            // try to appease the president
            else if (faction == Suspicions[President].GetHighest())
            {
                chosenCards.Add(faction);
            }
        }

        if (chosenCards.Count >= 2) return (chosenCards[0], chosenCards[1]);
        chosenCards.Clear();
        chosenCards.AddRange(cards[0], cards[1]);
        return (chosenCards[0], chosenCards[1]);
    }

    private Faction SelectOneOfTwoCards(Minister minister, Faction a, Faction b)
    {
        // from the predisent's point of view
        if (Suspicions[minister].Trust > 1f)
        {
            var amount = int.Clamp((int)Suspicions[minister].Trust, 1, 5);
            amount += 1;
            minister.IncreasePoliticalCapital(amount);
        }

        if (a == b)
        {
            if (Faction != a)
                Suspicions[minister].LowerTrust(1f);
            else
                Suspicions[minister].RaiseTrust(3f);
        }
        
        if (a == Faction || b == Faction)
            Suspicions[minister].RaiseTrust(1f);
        
        if (a != Faction && b != Faction)
            Suspicions[minister].LowerTrust(2f);
        
        if (a == minister.Faction || b == minister.Faction)
        {
            minister.IncreasePoliticalCapital(10);
        }
        
        var cards = new List<Faction>() { a, b };
        foreach (var faction in cards)
        {
            if (faction == Faction)
            {
                if (!IsPerceivedAThreat())
                {
                    return faction;
                }
            }
            
            // otherwise try and side with the majority
            else if (PollBestPosition() == faction)
            {
                return faction;
            }
            
        }
        
        return a;
    }

    private void IncreasePoliticalCapital(int amount)
    {
        PoliticalCapital += amount;
        // Console.WriteLine($"Poltical capital of {Name} increased by {amount}");
        if (IsPlayer)
        {
            var player = this as Player;
            player?.JustIncreasedPoliticalCapital(amount);
        }
    }

    public void DecreasePoliticalCapital(int amount)
    {
        PoliticalCapital -= amount;
        if (IsPlayer)
        {
            var player = this as Player;
            player?.JustDecreasedPoliticalCapital(amount);
        }
    }

    private bool IsPerceivedAThreat()
    {
        var randomChoice = Random.Next(0, 10);
        if (randomChoice <= Riskiness) return false; // dismiss our instinct
        var poll = PollAllSuspicions();
        return Perception.IsPerceivedAThreat(poll);
    }

    private Faction PollBestPosition()
    {
        // probably should be weighted on most threatening positions, to keep them on your side seems logical
        var poll = PollAllSuspicions();
        var youThinkTheMajorityAre = poll.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        return youThinkTheMajorityAre;
    }

    private bool IsPresidentTrustworthy()
    {
        const float threshold = 10f;
        return Suspicions[President].Trust > threshold;
    }

    private Dictionary<Faction, int> PollAllSuspicions()
    {
        var dict = new Dictionary<Faction, int>();
        foreach (var (minister, value) in Suspicions)
        {
            var belief = value.GetHighest();
            var exists = dict.TryGetValue(belief, out _);
            if (exists)
            {
                dict[belief] += 1;
            }
            else
            {
                dict.Add(belief, 1);
            }
        }

        return dict;
    }

    public Minister GetMinisterWhomLeastTrusted()
    {
        var (lastTurn, lastPolicy) = GameManager.PreviouslyImplementedPolicy;
        var lowestTrust = Suspicions[lastTurn].Trust;
        var lowestTrustMinister = lastTurn;
        foreach (var (minister, suspicion) in Suspicions)
        {
            if (!(suspicion.Trust < lowestTrust)) continue;
            lowestTrust = suspicion.Trust;
            lowestTrustMinister = minister;
        }

        return lowestTrustMinister;
    }

    public RoleType[] RankOrderThreats()
    {
        return
        [
            RoleType.MinisterOfNationalSecurity, // can kill ministers
            RoleType.SupremeCommander, // overthrows president
            RoleType.SecretaryOfImmigration,
            RoleType.SecretaryForPropaganda
        ];
    }

    public RoleType[] RankOrderLeastThreatening()
    {
        return
        [
            RoleType.MinisterOfInterior,
            RoleType.PresidentsAide,
            RoleType.SecretaryForPropaganda
        ];
    }

    public Minister FindMinisterGivenRole(RoleType role)
    {
        foreach (var minister in GameManager.CurrentMinisters)
        {
            if (minister.Role.RoleType == role)
                return minister;
        }

        throw new Exception("Minister appears to have gone missing!");
    }
    
    public Minister ChooseNewMinister()
    {
        var ministerOptions = GameManager.ThisTurnsMinisters;
        if (ministerOptions[0].Faction == Faction) return ministerOptions[0];
        if (ministerOptions[1].Faction == Faction) return ministerOptions[1];
        return ministerOptions[0];
    }

    public virtual void RespondToBribe(Minister sender)
    { 
        if (Suspicions[sender].GetHighest() == Faction)
            Suspicions[sender].RaiseTrust(10f);
        else
            Suspicions[sender].RaiseTrust(5f);
    }

    public virtual void RespondToConversion(Minister thisPlayer)
    {
        
    }
}