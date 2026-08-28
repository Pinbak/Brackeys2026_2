using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Engine.Components.Experiments;

public class InputTesting() : Component
{
    protected override void OnUpdate()
    {
        if (Mouse.HasPressed(Mouse.Button.Left))
        {
            // Tweener.MoveXTo(GameObject.Transform, GameObject.Transform.X + 50, 2f, EaseFunctions.OutElastic);
            // Tweener.ScaleTo(GameObject.Transform, 2f, 5f, EaseFunctions.OutElastic);
            
            Console.WriteLine($"Left button pressed x: {Mouse.X}, y: {Mouse.Y}");

        }

        if (Mouse.HasReleased(Mouse.Button.Left))
        {
            Console.WriteLine("Left button released");
        }

        // if (_sprite.IsPointInside(new Point(Mouse.X, Mouse.Y)))
        // {
        //     Console.WriteLine(Time.Fps);
        // }
        
        // Console.WriteLine(Time.Fps);
    }
}