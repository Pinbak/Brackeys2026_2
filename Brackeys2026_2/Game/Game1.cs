using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Behaviours;
using Brackeys2026_2.Engine.Behaviours.Experiments;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Layout()
    {
        var texture = new Texture("Game/Assets/test.png");
        var obj = new GameObject(500, 250, null, [
            new SineTweener(),
            new SpriteRenderer(texture)
        ]);
            var font = new Font("Game/Assets/Staatliches-Regular.ttf", 20);
            var child = new GameObject(100, 100, obj, [
                new SpriteRenderer(texture)
            ]);
                var child2 = new GameObject(100, 100, child, [
                    new SpriteRenderer(texture),
                    new SineTweener()
                ]);
                    var text = new TextRenderer(font, new Colour(0, 0, 0, 255), "Hello world this is longer", 150);
                    var texture2 = new Texture("Game/Assets/test2.png");
                    _ = new GameObject(0, 0, child2,[
                        new SpriteRenderer(texture2)
                    ]);
                    var text2 = new TextRenderer(font, new Colour(0, 0, 0, 255), "Hello world", 150);
                    _ = new GameObject(150, 50, child2, [
                        new InputTesting(),
                        text2
                    ]);
        
    }
}