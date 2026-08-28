using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public class Player : Minister
{
    private PlayerInteraction _player;
    
    public Player(Role role, Texture face, PlayerInteraction player) : base(role, face)
    {
        _player = player;
    }
    
    public override void BeginTurn()
    {
        Console.WriteLine("players turn");
        _player.StartOfNewPlayerTurn();
    }
}