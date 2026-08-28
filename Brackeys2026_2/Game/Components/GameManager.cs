using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class GameManager : Component
{
    public List<Minister> CurrentMinisters { get; set; } = [];
    private const int NumberOfMinisters = 8;
    public static (Minister, Faction) PreviouslyImplementedPolicy;
    public static int Turn { get; set; }
    
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

    private List<GameObject> _ministerGameObjects = [];
    private List<Card> _cards = [];

    public GameManager(Texture card, Texture cardOutline, Texture[] faces, Font nameFont, Font smallerFont, Button confirmButton, Button nextTurnButton, PlayerInteraction player)
    {
        _nameFont = nameFont;
        _card = card;
        _faces = faces;
        _smallerFont = smallerFont;
        _nextTurnButton = nextTurnButton;
        nextTurnButton.HasPressed += NextTurn;
        _player = player;
        _cardOutline = cardOutline;
    }

    protected override void OnInitialise()
    {
        var playerIndex = Random.Next(1, NumberOfMinisters);
        for (var i = 0; i < NumberOfMinisters; i++)
        {
            if (i == playerIndex)
            {
                var newMinister = new Player(Role.GetRoleFromRoleType((RoleType)i), GetRandomFaceTexture(), _player);
                CurrentMinisters.Add(newMinister);
                newMinister.IsPlayer = true;
                _player.Player = newMinister;
            }
            else
            {
                var newMinister = new Minister(Role.GetRoleFromRoleType((RoleType)i), GetRandomFaceTexture());
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
        GeneratePresidentGameObject();
    }

    private void GeneratePresidentGameObject()
    {
        
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
            currentX += (int)width + gap;
            c++;
            var collider = new Collider(ministerGameObject.Transform, _card.Width, _card.Height);

            var cardOutline = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(_cardOutline),
            ]) { IsEnabled = false };
            
            var cardComponent = new Card(minister, collider, cardOutline, _nextTurnButton);
            ministerGameObject.AddComponent(collider);
            ministerGameObject.AddComponent(cardComponent);

            var cardGameObject = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(_card),
            ]);
            
            var face = new GameObject(-96, 0, cardGameObject, [
                new SpriteRenderer(minister.Face)
            ]);

            var displayName = minister.Name;
            if (minister.IsPlayer)
            {
                displayName += " (YOU)";
            }

            var nameText = new GameObject(50, -30, cardGameObject, [
                new TextRenderer(_smallerFont, displayName, 200)
            ]);
            var roleText = new GameObject(60, 10, cardGameObject, [
                new TextRenderer(_smallerFont, minister.Role.Name, 200)
            ]);
            
            _ministerGameObjects.Add(ministerGameObject);
        }
    }

    private Texture GetRandomFaceTexture()
    {
        return _faces[Random.Next(0, _faces.Length)];
    }
    
    private void NextTurn()
    {
        if (_isInTurn) return;
        var positionInList = CurrentMinisters.FindIndex(a => a == _currentTurn);
        var nextMinisterTurn = positionInList += 1;
        if (nextMinisterTurn > NumberOfMinisters - 1)
        {
            nextMinisterTurn = 0;
        }

        _isInTurn = true;
        _currentTurn.IsTurn = false;
        _currentTurn = CurrentMinisters[nextMinisterTurn];
        _currentTurn.IsTurn = true;

        _currentTurn.BeginTurn();
    }

    public static void EndTurn(Faction faction)
    {
        PreviouslyImplementedPolicy = (_currentTurn, faction);
        Turn++;
        _isInTurn = false;
    }

}