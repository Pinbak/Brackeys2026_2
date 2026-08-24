using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Setup()
    {
        var texture = new Texture("Game/Assets/test.png");
        var sprite = new Sprite(texture, 100, 100);
        var sprite2 = new Sprite(texture, 200, 100);
        
    }
}
