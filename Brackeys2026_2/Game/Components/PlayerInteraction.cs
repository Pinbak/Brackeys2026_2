using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class PlayerInteraction : Component
{
    public Minister Player { get; set; }
    
    private TextRenderer _playerDescription;
    private TextRenderer _jobDescription;
    private TextRenderer _secretFaction;
    private TextRenderer _goalText;
    private TextRenderer _actionText;
    private Button _confirmActionButton;
    private readonly Button _nextTurnButton;

    public PlayerInteraction(Button confirmButton, Button nextTurnButton, TextRenderer playerDescriptionText, TextRenderer jobDescriptionText, TextRenderer secretText, TextRenderer goalText, TextRenderer actionText)
    {
        _confirmActionButton = confirmButton;
        confirmButton.HasPressed += ConfirmAction;
        _nextTurnButton = nextTurnButton;
        _playerDescription = playerDescriptionText;
        _jobDescription = jobDescriptionText;
        _secretFaction = secretText;
        _goalText = goalText;
        _actionText = actionText;
    }

    protected override void OnInitialise()
    {
        _playerDescription.Set($"{Player.Name} (YOU)");
        _jobDescription.Set(Player.Role.Name);
        _secretFaction.Set(Player.Faction.SecretName);
        _goalText.Set(Player.GetGoal());
        _actionText.Set(Player.GetActionText());
    }

    private void ConfirmAction()
    {
        
    }

    public void StartOfNewPlayerTurn()
    {
        ToggleNextTurnDisabled();
    }

    private void ToggleNextTurnDisabled()
    {
        _nextTurnButton.DisablePresses("Your turn...");
        _confirmActionButton.EnablePresses();
    }

    private void ToggleNextTurnEnabled()
    {
        _nextTurnButton.EnablePresses();
        _confirmActionButton.DisablePresses();
    }
}