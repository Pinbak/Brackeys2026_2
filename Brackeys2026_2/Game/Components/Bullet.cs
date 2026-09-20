using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Game.Components;

public class Bullet : Component
{
    protected override void OnUpdate()
    {
        GameObject.Transform.X += Time.DeltaTime * 40;
    }
}