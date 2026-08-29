using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class PlayerInteraction : Component
{
    private Player Player { get; set; }
    
    private TextRenderer _playerDescription;
    private TextRenderer _jobDescription;
    private TextRenderer _secretFaction;
    private TextRenderer _goalText;
    private TextRenderer _actionText;
    private Texture _agrarianTexture;
    private Texture _technocratTexture;
    private Texture _federalistTexture;
    private Texture _loyalistTexture;
    private Button _confirmActionButton;
    private readonly Button _nextTurnButton;
    private readonly List<GameObject> _playerCardsGameObjects = [];
    private readonly List<PlayerCard> _playerCards = [];
    private readonly List<Faction> _selected = [];

    public PlayerInteraction(Button confirmButton, Button nextTurnButton, TextRenderer playerDescriptionText,
        TextRenderer jobDescriptionText, TextRenderer secretText, TextRenderer goalText, TextRenderer actionText,
        Texture loyalist, Texture agrarian, Texture technocrat, Texture federalist)
    {
        _confirmActionButton = confirmButton;
        confirmButton.HasPressed += ConfirmAction;
        _nextTurnButton = nextTurnButton;
        _nextTurnButton.HasPressed += FinishTurn;
        _playerDescription = playerDescriptionText;
        _jobDescription = jobDescriptionText;
        _secretFaction = secretText;
        _goalText = goalText;
        _actionText = actionText;
        _agrarianTexture = agrarian;
        _technocratTexture = technocrat;
        _federalistTexture = federalist;
        _loyalistTexture = loyalist;
    }
    
    public void UpdateTextUi(Player thisPlayer)
    {
        Player = thisPlayer;
        _playerDescription.Set($"{Player.Name} (YOU)");
        _jobDescription.Set(Player.Role.Name);
        _secretFaction.Set(Player.Faction.SecretName);
        _goalText.Set(Player.GetGoal());
        _actionText.Set(Player.GetActionText());
    }

    private void ConfirmAction()
    {
        
    }
    
    private void FinishTurn()
    {
        if (_selected.Count == 2)
        {
            ToggleNextTurnEnabled();
            EndTurn();
            RemoveCards();
        }
    }

    private void RemoveCards()
    {
        foreach (var playerCard in _playerCards)
        {
            playerCard.CardDeselected -= DeselectSelected;
            playerCard.CardSelected -= CardSelected;
        }

        _playerCardsGameObjects.Clear();
        _playerCards.Clear();
        _selected.Clear();
    }
    

    private void EndTurn()
    {
        for (var i = 0; i < _playerCards.Count; i++)
        {
            var playerCard = _playerCards[i];
            playerCard.RemoveTween(i * .1f);
        }

        Player.EndPlayerTurn(_selected[0], _selected[1]);
    }

    public void StartOfNewPlayerTurn(List<Faction> gameManagerThisTurnsCards)
    {
        ToggleNextTurnDisabled();
        ShowCards(gameManagerThisTurnsCards);
    }

    private void ShowCards(List<Faction> thisTurn)
    {
        var width = _federalistTexture.Width;
        var height = _federalistTexture.Height;
        var gap = 10;
        var padding = 10f;
        var startingX = 960 - thisTurn.Count * .5f * (width + gap);
        var currentX = startingX;
        var currentY = 1300;
        var i = 0;
        foreach (var faction in thisTurn)
        {
            var newCardObject = new GameObject(currentX + padding, currentY + padding, GameObject);
            currentX += (int)width + gap;

            switch (faction.FactionType)
            {
                case FactionType.Agrarian:
                    newCardObject.AddComponent(new SpriteRenderer(_agrarianTexture));
                    break;
                case FactionType.Loyalist:
                    newCardObject.AddComponent(new SpriteRenderer(_loyalistTexture));
                    break;
                case FactionType.Technocrat:
                    newCardObject.AddComponent(new SpriteRenderer(_technocratTexture));
                    break;
                case FactionType.Federalist:
                    newCardObject.AddComponent(new SpriteRenderer(_federalistTexture));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var collider = new Collider(newCardObject.Transform, width, height);
            newCardObject.AddComponent(collider);
            var playerCard = new PlayerCard(collider, faction, i * .1f);
            newCardObject.AddComponent(playerCard);
            playerCard.CardSelected += CardSelected;
            playerCard.CardDeselected += DeselectSelected;
            i++;

            _playerCards.Add(playerCard);
            _playerCardsGameObjects.Add(newCardObject);
        }
    }

    private void CardSelected(Faction factionSelected)
    {
        _selected.Add(factionSelected);
        UpdateActions();
    }

    private void UpdateActions()
    {
        if (_selected.Count == 2)
        {
            _nextTurnButton.EnablePresses();
            _nextTurnButton.SetText("Send to President");
        }
        else
        {
            _nextTurnButton.DisablePresses("Choose Two Cards...");
        }
    }
    
    private void DeselectSelected(Faction factionSelected)
    {
        _selected.Remove(factionSelected);
        UpdateActions();
    }

    private void ToggleNextTurnDisabled()
    {
        _nextTurnButton.DisablePresses("Choose Two Cards...");
        _confirmActionButton.EnablePresses();
    }

    private void ToggleNextTurnEnabled()
    {
        _nextTurnButton.EnablePresses();
        _confirmActionButton.DisablePresses();
    }
}