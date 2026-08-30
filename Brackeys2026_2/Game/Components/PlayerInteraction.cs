using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class PlayerInteraction : Component
{
    private Player Player { get; set; }
    public bool IsTurn { get; set; }

    public int CostToKill { get; set; } = 100;
    public int CostToConvert { get; set; } = 60;
    public int CostToBribe { get; set; } = 30;

    private TextRenderer _playerDescription;
    private TextRenderer _jobDescription;
    private TextRenderer _secretFaction;
    private TextRenderer _goalText;
    private TextRenderer _actionText;
    private TextRenderer _pcText;
    private Texture _agrarianTexture;
    private Texture _technocratTexture;
    private Texture _federalistTexture;
    private Texture _loyalistTexture;
    private Texture _token;
    private readonly Button _nextTurnButton;
    private readonly List<GameObject> _playerCardsGameObjects = [];
    private readonly List<PlayerCard> _playerCards = [];
    private readonly List<Faction> _selected = [];
    private RightFolderManager _rightFolderManager;
    private MinisterFaceCard? _currentlySelected;

    public PlayerInteraction(Button nextTurnButton, TextRenderer playerDescriptionText,
        TextRenderer jobDescriptionText, TextRenderer secretText, TextRenderer goalText, TextRenderer actionText,
        TextRenderer pcText,
        Texture loyalist, Texture agrarian, Texture technocrat, Texture federalist, Texture token,
        RightFolderManager rightFolderManager)
    {
        _nextTurnButton = nextTurnButton;
        _playerDescription = playerDescriptionText;
        _jobDescription = jobDescriptionText;
        _secretFaction = secretText;
        _goalText = goalText;
        _actionText = actionText;
        _agrarianTexture = agrarian;
        _technocratTexture = technocrat;
        _federalistTexture = federalist;
        _loyalistTexture = loyalist;
        _pcText = pcText;
        _token = token;
        rightFolderManager.UpdateCosts(CostToKill, CostToConvert, CostToBribe);
        rightFolderManager.KillButton.HasPressed += KillButtonHasPressed;
        rightFolderManager.ConvertButton.HasPressed += ConvertButtonHasPressed;
        rightFolderManager.BribeButton.HasPressed += BribeButtonHasPressed;

        _rightFolderManager = rightFolderManager;
        
        foreach (var faction in Faction.All)
        {
            faction.Texture = faction.FactionType switch
            {
                FactionType.Loyalist => loyalist,
                FactionType.Technocrat => technocrat,
                FactionType.Agrarian => agrarian,
                FactionType.Federalist => federalist,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
    public void Killed(Minister toRemove)
    {
        if (_currentlySelected is null) return;
        if (_currentlySelected.Minister == toRemove)
        {
            _rightFolderManager.RemoveSelected();
            _currentlySelected = null;
        }
    }

    private void BribeButtonHasPressed()
    {
        if (Player.PoliticalCapital < CostToBribe) return;
        if (_currentlySelected is null)
        {
            _rightFolderManager.GiveFeedback("Click on a minister, and then click the action.");
        }
        else
        {
            if (_currentlySelected.Minister.IsPlayer)
            {
                _rightFolderManager.GiveFeedback("You cannot bribe yourself.");
            }
            else
            {
                _currentlySelected.Minister.RespondToBribe(Player);
                Player.PoliticalCapital -= CostToBribe;
                UpdatePoliticalCapitalUi();
                _rightFolderManager.GiveFeedback($"You have bribed {_currentlySelected.Minister.Name}. This will increase his trust in you.");
            }
        }
    }

    private void ConvertButtonHasPressed()
    {
        if (Player.PoliticalCapital < CostToConvert) return;
        if (_currentlySelected is null)
        {
            _rightFolderManager.GiveFeedback("Click on a minister, and then click the action.");
        }
        else
        {
            if (_currentlySelected.Minister.IsPlayer)
            {
                _rightFolderManager.GiveFeedback("You cannot convert yourself.");
            }
            else
            {
                _currentlySelected.Minister.RespondToConversion(Player);
                Player.PoliticalCapital -= CostToConvert;
                UpdatePoliticalCapitalUi();
                _rightFolderManager.GiveFeedback($"You have converted {_currentlySelected.Minister.Name}. He is now a {Player.Faction.Name} for now...");
            }
        }
    }

    private void KillButtonHasPressed()
    {
        if (Player.PoliticalCapital < CostToKill) return;
        if (_currentlySelected is null)
        {
            _rightFolderManager.GiveFeedback("Click on a minister, and then click the action.");
        }
        else
        {
            if (_currentlySelected.Minister.IsPlayer)
            {
                _rightFolderManager.GiveFeedback("You cannot remove yourself. This job is for life...");
            }
            else
            {
                Player.GameManager.KillMinister(_currentlySelected.Minister);
                Player.PoliticalCapital -= CostToBribe;
                UpdatePoliticalCapitalUi();
                _rightFolderManager.GiveFeedback($"You have 'disposed' of {_currentlySelected.Minister.Name}. His replacement will hopefully be more inline with our cause.");
            }
        }
    }

    public void UpdateTextUi()
    {
        UpdateTextUi(Player);
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
    
    public void FinishPlayerTurn()
    {
        if (!Player.IsTurn) return;
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
        IsTurn = false;
    }

    public void StartOfNewPlayerTurn(List<Faction> gameManagerThisTurnsCards)
    {
        _rightFolderManager.UpdateButtonEnables(Player.PoliticalCapital, CostToBribe, CostToConvert, CostToKill);
        IsTurn = true;
        ToggleNextTurnDisabled();
        ShowCards(gameManagerThisTurnsCards);
    }

    private void UpdatePoliticalCapitalUi()
    {
        _pcText.Set($"Political Capital: {Player.PoliticalCapital}");
    }
    
    public void IncreasePoliticalCapital(int amount)
    {
        UpdatePoliticalCapitalUi();

        for (var i = 0; i < amount; i++)
        {
            _ = new GameObject(650, 190, GameObject, [new SpriteRenderer(_token), new PoliticalCapitalToken(i * .1f)]);    
        }
        
        
    }
    
    public void DecreasePoliticalCapital(int amount)
    {
        UpdatePoliticalCapitalUi();
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

    public void ToggleNextTurnDisabled()
    {
        _nextTurnButton.DisablePresses("Choose Two Cards...");
    }

    public void ToggleNextTurnEnabled()
    {
        _nextTurnButton.EnablePresses();
        _rightFolderManager.DisablePresses();
    }
    
    public void DisableButtons()
    {
        _nextTurnButton.DisablePresses("AI Taking Turn...");
        _rightFolderManager.DisablePresses();
    }
    
    public void EnableButtons()
    {
        _nextTurnButton.EnablePresses();
        _rightFolderManager.DisablePresses();
    }

    public void ChooseMinister()
    {
        // Player.GameManager.ToRemove = newMinister;
    }

    public void EndGame()
    {
        throw new NotImplementedException();
    }


    public void ClickedOnCard(MinisterFaceCard ministerFaceCard)
    {
        _currentlySelected = ministerFaceCard;
        _rightFolderManager.UpdateUi(ministerFaceCard);
    }
}