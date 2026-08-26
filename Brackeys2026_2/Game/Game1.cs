using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Behaviours;
using Brackeys2026_2.Engine.Behaviours.ExperimentBehaviours;
using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Setup()
    {
        var texture = Graphics.NewTexture("Game/Assets/test.png");
        var obj = new GameObject(500, 250, null);
        obj.AddBehaviour(new SineTweener());
        var sprite = new Sprite(texture);
        obj.AddBehaviour(sprite);
        using var font = Font.New("Game/Assets/baijamjuree.ttf", 18);
        var child = new GameObject(100, 100, obj);
        child.AddBehaviour(new Sprite(texture));
        var child2 = new GameObject(100, 100, child);
        child2.AddBehaviour(new Sprite(texture));
        child2.AddBehaviour(new SineTweener());
        var textNode = new GameObject(0, 0, child2);
        var text = new Text(font, new SDL.Color(0, 0, 0, 255), "Hello world this is longer", 150);
        var texture2 = Graphics.NewTexture("Game/Assets/test2.png");
        textNode.AddBehaviour(new Sprite(texture2));
        var text2 = new Text(font, new SDL.Color(0, 0, 0, 255), "Hello", 150);
        var textNode2 = new GameObject(0, 50, child2);
        textNode2.AddBehaviour(text);
        // child.IsEnabled = false;

    }
}