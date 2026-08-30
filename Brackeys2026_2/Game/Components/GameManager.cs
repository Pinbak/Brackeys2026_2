using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Game.Classes;
using Brackeys2026_2.Game.Classes.Ministers;

namespace Brackeys2026_2.Game.Components;

public class GameManager : Component
{
    public List<Minister> CurrentMinisters { get; set; } = [];
    private const int NumberOfMinisters = 8;
    public (Minister, Faction) PreviouslyImplementedPolicy { get; private set; }
    public static float TurnDelay { get; set; }
    public int Turn { get; set; }
    public List<Faction> ThisTurnsCards = [];
    public List<Minister> ThisTurnsMinisters = [];
    public bool SomeoneIsDying;
    public Minister ToRemove;

    private static readonly Random Random = new();
    private Font _nameFont;
    private readonly Texture _card;
    private readonly Texture _cardOutline;
    private readonly Texture[] _faces;
    private readonly Font _smallerFont;
    private PlayerInteraction _player;
    private Button _nextTurnButton;
    private static Minister _currentTurn;
    private static bool _isInTurn;
    private const float TurnLength = 0.0f;
    private LastPlayed _lastPlayed;
    private NoteManager _stickyNote;
    // private News _news;

    private Dictionary<RoleType, (float x, float y)> MinisterPositions { get; set; } = new();

    private List<GameObject> _ministerGameObjects = [];
    private List<MinisterFaceCard> _cards = [];

    public GameManager(Texture card, Texture cardOutline, Texture[] faces, Font nameFont, Font smallerFont,
        Button nextTurnButton, PlayerInteraction player, LastPlayed lastPlayed, NoteManager stickyNote)
    {
        _nameFont = nameFont;
        _card = card;
        _faces = faces;
        _smallerFont = smallerFont;
        _nextTurnButton = nextTurnButton;
        nextTurnButton.HasPressed += NextTurn;
        _player = player;
        _cardOutline = cardOutline;
        _lastPlayed = lastPlayed;
        // _news = news;
        _stickyNote = stickyNote;
    }

    protected override void OnUpdate()
    {
        if (!_isInTurn) return;
        TurnDelay -= Time.DeltaTime;
        if (TurnDelay <= 0)
        {
            _isInTurn = false;
            TurnDelay = 0f;
            if (_player.IsTurn)
            {
                _player.ToggleNextTurnDisabled();
            }
            else
            {
                _player.ToggleNextTurnEnabled();   
            }
            
        }
    }

    protected override void OnInitialise()
    {
        var playerIndex = Random.Next(1, NumberOfMinisters - 1);
        for (var i = 0; i < NumberOfMinisters; i++)
        {
            if (i == playerIndex)
            {
                var newMinister = new Player(GetRoleFromRoleType((RoleType)i), GetRandomFaceTexture(), _player, this);
                CurrentMinisters.Add(newMinister);
                newMinister.IsPlayer = true;
                _player.UpdateTextUi(newMinister);
            }
            else
            {
                var newMinister = new Minister(GetRoleFromRoleType((RoleType)i), GetRandomFaceTexture(), this);
                CurrentMinisters.Add(newMinister);
                
                if (i == NumberOfMinisters - 1)
                    _currentTurn = newMinister;
            }
        }

        foreach (var currentMinister in CurrentMinisters)
        {
            currentMinister.PopulateSuspicions(CurrentMinisters);
        }
        
        GenerateMinisterGameObjects();
    }

    private void GenerateMinisterGameObjects()
    {
        // have to hard-code as no ui-system
        var width = _card.Width;
        var gap = 10;
        var padding = 10f;
        var rowMax = 2;
        var startingX = (int)width * .5f + 30;
        var currentX = startingX;
        var currentY = _card.Height * .5f + 5;
        var c = 0;
        foreach (var minister in CurrentMinisters)
        {
            if (c == rowMax)
            {
                c = 0;
                currentX = startingX;
                currentY += (int)_card.Height + gap;
            }

            var ministerGameObject = new GameObject(currentX + padding, currentY + padding, GameObject);
            MinisterPositions.Add(minister.Role.RoleType, (currentX + padding, currentY + padding));
            currentX += (int)width + gap;
            c++;
            var collider = new Collider(ministerGameObject.Transform, _card.Width, _card.Height);

            var cardOutline = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(_cardOutline),
            ]) { IsEnabled = false };
            

            var cardGameObject = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(_card),
            ]);

            var face = new SpriteRenderer(minister.Face);
            _ = new GameObject(-96, 0, cardGameObject, [
                face
            ]);

            var cardComponent = new MinisterFaceCard(minister, collider, cardOutline, _nextTurnButton, face, _player);
            ministerGameObject.AddComponent(collider);
            ministerGameObject.AddComponent(cardComponent);

            var displayName = minister.Name;
            if (minister.IsPlayer)
            {
                displayName += " (YOU)";
            }

            var roleText = new TextRenderer(_nameFont, minister.Role.Name, 200);
            _ = new GameObject(60, -10, cardGameObject, [
               roleText 
            ]);
            var nameText = new TextRenderer(_smallerFont, displayName, 200);
            _ = new GameObject(50, 40, cardGameObject, [
                nameText
            ]);

            cardComponent.NameText = nameText;
            cardComponent.RoleText = roleText;

            _cards.Add(cardComponent);
            _ministerGameObjects.Add(ministerGameObject);
            
        }
    }

    private Role GetRoleFromRoleType(RoleType roleType)
    {
        return roleType switch
        {
            RoleType.President => new President(roleType, "President", this),
            RoleType.PresidentsAide => new PresidentsAide(roleType, "President's Aide", this),
            RoleType.MinisterOfNationalSecurity => new MinisterOfNationalSecurity(roleType, "Minister of National Security", this),
            RoleType.SupremeCommander => new SupremeCommander(roleType, "Supreme Commander", this),
            RoleType.SecretaryOfImmigration => new SecretaryOfImmigration(roleType, "Secretary of Immigration", this),
            RoleType.MinisterOfCommunication => new MinisterOfCommunication(roleType, "Minister of Communication", this),
            RoleType.MinisterOfInterior => new MinisterOfInterior(roleType, "Minister of Interior", this),
            RoleType.SecretaryForPropaganda => new SecretaryForPropaganda(roleType, "Secretary for Propaganda", this),
            _ => throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null)
        };
    }

    private Texture GetRandomFaceTexture()
    {
        return _faces[Random.Next(0, _faces.Length)];
    }
    
    private void NextTurn()
    {
        if (_player.IsTurn)
        {
            _player.FinishPlayerTurn();
        }
        else
        {
            if (_isInTurn) return;
            var positionInList = CurrentMinisters.FindIndex(a => a == _currentTurn);
            var nextMinisterTurn = positionInList + 1;
            if (nextMinisterTurn > NumberOfMinisters - 1)
            {
                nextMinisterTurn = 0;
            }

            _currentTurn.IsTurn = false;
            _currentTurn = CurrentMinisters[nextMinisterTurn];
            _currentTurn.IsTurn = true;
        
            // new turn
            _isInTurn = true;
            _player.DisableButtons();
            GenerateFirstOfTurnCards();
            _currentTurn.BeginTurn();
        }
        
    }

    public void EndTurn(Faction faction)
    {
        if (_currentTurn == Minister.President)
            _stickyNote.Add($"{faction.Name} policy was enacted. This was implemented by our glorious leader.", false);
        else if (_currentTurn.IsPlayer)
            _stickyNote.Add($"{faction.Name} policy was enacted. This was sponsored by you and implemented by our glorious leader.", false);
        else
            _stickyNote.Add($"{faction.Name} policy was enacted. This was sponsored by {_currentTurn.Name} and implemented by our glorious leader.", false);
        foreach (var minister in CurrentMinisters) minister.CalculateSuspicions();
        PreviouslyImplementedPolicy = (_currentTurn, faction);
        // _ = new GameObject(650 - 520, 190 - 10, GameObject, [new SpriteRenderer(faction.Texture), new CardToken()]);
        _lastPlayed.UpdateTexture(faction);
        Turn++;
        TurnDelay = TurnLength;
    }

    public void GenerateFirstOfTurnCards()
    {
        const int numberOfCardsPerTurn = 3;
        ThisTurnsCards.Clear();
        for (var j = 0; j < numberOfCardsPerTurn; j++)
        {
            var i = Random.Next(0, 4);
            var card = Faction.All[i];
            ThisTurnsCards.Add(card);    
        }
    }
    
    public void UpdatePositions()
    {
        foreach (var ministerFaceCard in _cards)
        {
            var (x, y) = MinisterPositions[ministerFaceCard.Role.RoleType];
            ministerFaceCard.MoveToNewPosition(x, y);
            ministerFaceCard.UpdateRoleText();
            if (ministerFaceCard.Minister.IsPlayer)
            {
                _player.UpdateTextUi();
            }
        }
    }

    public void CabinetReshuffled(Minister a, Minister b)
    {
        if (a.IsPlayer)
        {
            _stickyNote.Add($"Our glorious leader has reshuffled the cabinet. You have changed role. You are now the {a.Role.Name}.", true);
        }
        else if (b.IsPlayer)
        {
            _stickyNote.Add($"Our glorious leader has reshuffled the cabinet. You have changed role. You are now the {b.Role.Name}.", true);
        }
        else
        {
            _stickyNote.Add(
                $"Our glorious leader has reshuffled the cabinet. The former {b.Role.Name}, {a.Name} is now the {a.Role.Name}. The former {a.Role.Name}, {b.Name} is now the {b.Role.Name}", true);
        }
        
    }

    private Minister GetMinisterByRoleType(RoleType role)
    {
        foreach (var currentMinister in CurrentMinisters)
        {
            if (currentMinister.Role.RoleType == role)
                return currentMinister;
        }

        throw new Exception($"Could not find {role}.");
    }

    private void GenerateMinisterChoices(Role role)
    {
        ThisTurnsMinisters.Clear();
        ThisTurnsMinisters.Add(new Minister(role, GetRandomFaceTexture(), this));
        ThisTurnsMinisters.Add(new Minister(role, GetRandomFaceTexture(), this));
        
    }

    public void KillMinister(Minister lowestTrust)
    {
        Minister newMinister;
        SomeoneIsDying = true;
        // TurnDelay += 5f;
        // on player death, end game
        if (lowestTrust.IsPlayer)
        {
            _player.EndGame();
            return;
        }
        ToRemove = lowestTrust;
        GenerateMinisterChoices(lowestTrust.Role);
        if (lowestTrust.Role.RoleType is RoleType.President)
        {
            newMinister = new Minister(lowestTrust.Role, GetRandomFaceTexture(), this);
            ChosenNewMinister(newMinister);
        }
        else
        {
            var president = GetMinisterByRoleType(RoleType.President);
            newMinister = president.ChooseNewMinister();
            ChosenNewMinister(newMinister);
        }
        
    }

    private void ChosenNewMinister(Minister newMinister)
    {
        
        _stickyNote.Add(
            $"{ToRemove.Name} has mysteriously disappeared. In other news, we welcome {newMinister.Name} to take his place. {newMinister.Name} was chosen by our glorious leader.", true);
        foreach (var ministerFaceCard in _cards)
        {
            if (ministerFaceCard.Minister == ToRemove)
            {
                ministerFaceCard.ChangeMinister(newMinister);
            }
        }

        var positionInList = CurrentMinisters.FindIndex(m => m == ToRemove);
        CurrentMinisters.Remove(ToRemove);
        CurrentMinisters.Insert(positionInList, newMinister);
        
        foreach (var currentMinister in CurrentMinisters)
        {
            currentMinister.Suspicions.Remove(ToRemove);
            currentMinister.Suspicions.Add(newMinister, new Suspicion());
        }

        if (PreviouslyImplementedPolicy.Item1 == ToRemove)
        {
            PreviouslyImplementedPolicy = (newMinister, PreviouslyImplementedPolicy.Item2);
        }

        _player.Killed(ToRemove);
        
        newMinister.PopulateSuspicions(CurrentMinisters);
        
        SomeoneIsDying = false;
    }

    public void BribeMinister(Minister thisPlayer, Minister lowestTrust, int amount)
    {
        Console.WriteLine($"{thisPlayer.Name} has bribed {lowestTrust.Name}.");
        lowestTrust.PoliticalCapital += amount;
        lowestTrust.RespondToBribe(thisPlayer);
    }

    public void ConvertIdeology(Minister thisPlayer, Minister lowestTrust)
    {
        Console.WriteLine($"{thisPlayer.Name} has converted {lowestTrust.Name} to {thisPlayer.Faction.Name}.");
        lowestTrust.Faction = thisPlayer.Faction;
        lowestTrust.Suspicions[thisPlayer].SetTrust(10f);
        thisPlayer.Suspicions[lowestTrust].SetTrust(10f);
        lowestTrust.Suspicions[thisPlayer].SetActualValue(thisPlayer.Faction, 10f);
        thisPlayer.Suspicions[lowestTrust].SetActualValue(thisPlayer.Faction, 10f);
        lowestTrust.RespondToConversion(thisPlayer);
    }
}