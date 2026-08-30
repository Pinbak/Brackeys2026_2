using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Classes.Ministers;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public class Player : Minister
{
    private readonly PlayerInteraction _player;

    public Player(Role role, Texture face, PlayerInteraction player, GameManager gameManager) : base(role, face,
        gameManager)
    {
        _player = player;
        PoliticalCapital = 10000;
    }

    public override void BeginTurn()
    {
        // Console.WriteLine("players turn");
        _player.StartOfNewPlayerTurn(GameManager.ThisTurnsCards);
    }

    public override void RespondToBribe(Minister sender)
    {
        _player.SendMessage($"{sender.Name} has gifted you 30 political capital. Some may call this a bribe.", true);   
        _player.UpdatePoliticalCapitalUi();
    }

    public override void RespondToConversion(Minister sender)
    {
        _player.SendMessage($"{sender.Name} convinced you to alter your ways. You now support the {sender.Faction.Name} faction.", true);   
        _player.UpdateTextUi();
    }

    public void JustIncreasedPoliticalCapital(int amount)
    {
        _player.IncreasePoliticalCapital(amount);
    }
    
    public void JustDecreasedPoliticalCapital(int amount)
    {
        _player.DecreasePoliticalCapital(amount);
    }

}