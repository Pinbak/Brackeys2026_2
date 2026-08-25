using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Behaviours;
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Setup()
    {
        
        var texture = new RawTexture("Game/Assets/test.png");
        var sprite = new Sprite(texture, new SDL.Rect(0, 0, 50, 60), 100, 100);
        sprite.Transform.Scale *= .5f;
        var test = new Test("Game/Assets/baijamjuree.ttf");
        sprite.AddBehaviour(test);
        var sprite2 = new Sprite(texture, null, 200, 100);
    }
}