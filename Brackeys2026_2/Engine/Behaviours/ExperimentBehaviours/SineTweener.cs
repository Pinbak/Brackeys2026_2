using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.Behaviours.ExperimentBehaviours;

public class SineTweener : UpdatableBehaviour
{
    private float _time;
    
    public override void OnUpdate()
    {
        _time += Time.DeltaTime;
        GameObject.Transform.Y = float.Sin(_time * 2) * 100;

        if (Input.IsMousePressed(Input.MouseButton.Left, out var x, out var y))
        {
            Console.WriteLine($"Left button pressed x: {x}, y: {y}");
        }

        if (Input.IsMouseReleased(Input.MouseButton.Left))
        {
            Console.WriteLine("Left button released!");
        }
        
    }
}