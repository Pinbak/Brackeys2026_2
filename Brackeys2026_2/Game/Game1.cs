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
        using var font = Font.New("Game/Assets/baijamjuree.ttf");
        var text = new Text(font, new SDL.Color(0, 0, 0, 255), "Hello world", 150);
        var textNode = new GameObject(0, 0, obj);
        textNode.AddBehaviour(text);
        var child = new GameObject(100, 0, obj);
        child.AddBehaviour(new Sprite(texture));
        obj.AddBehaviour(new Test());
        var child2 = new GameObject(0, 100, child);
        child2.AddBehaviour(new Sprite(texture));
        child2.AddBehaviour(new Test2());
        // child.IsEnabled = false;

    }
}