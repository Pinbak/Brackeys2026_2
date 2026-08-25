using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class Test : UpdatableBehaviour
{
    public override void OnUpdate()
    {
        GameObject.Transform.X += 100 * Time.DeltaTime;
    }

}