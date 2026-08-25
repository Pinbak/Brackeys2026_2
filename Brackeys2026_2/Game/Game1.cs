using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Behaviours;
using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Setup()
    {
        var texture = Graphics.NewTexture("Game/Assets/test.png");
        var obj = new GameObject(100, 0, null);
        var sprite = new Sprite(texture);
        obj.AddBehaviour(sprite);
        var test = new Test(sprite);
        obj.AddBehaviour(test);
        var child = new GameObject(50, 0, obj);
        child.AddBehaviour(new Sprite(texture));
        
        var child2 = new GameObject(50, 50, obj);
        child2.AddBehaviour(new Sprite(texture));

        // using var font = Font.New("Game/Assets/baijamjuree.ttf");
        // obj.AddBehaviour(new Text(font, new SDL.Color(0, 0, 0, 255), "Hello world", 150));

    }
}