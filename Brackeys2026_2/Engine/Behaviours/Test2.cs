using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class Test2 : UpdatableBehaviour
{
    private float _time;
    
    public override void OnUpdate()
    {
        _time += Time.DeltaTime;
        GameObject.Transform.Y = MathF.Sin(_time * 2) * 100;
    }

}