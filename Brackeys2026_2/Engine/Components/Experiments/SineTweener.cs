using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components.Experiments;

public class SineTweener : Component
{
    private float _time;

    protected override void OnUpdate()
    {
        _time += Time.DeltaTime;
        // GameObject.Transform.Y = float.Sin(_time * 2) * 100;
        GameObject.Transform.Angle += 100f * Time.DeltaTime;
    }
}