using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components.Experiments;

public class SineTweener : Component
{
    private float _time;

    protected override void OnUpdate()
    {
        _time += Time.DeltaTime;
        Console.WriteLine($"{Time.DeltaTime * 1000.0:F3} ms");
        // GameObject.Transform.X = float.Sin(_time * 2) * 100;
        // GameObject.Transform.Angle += 100f * Time.DeltaTime;
    }
}