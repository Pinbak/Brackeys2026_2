using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Game.Components;

public class Card(Collider collider) : Component
{
    private static readonly Random Random = new(); 
    
    protected override void OnUpdate()
    {
        if (collider.HasMouseEntered())
        {
            Tweener.RotateTo(GameObject.Transform, Random.Next(-5, 5), 0.4f, EaseFunctions.OutBounce);
        }

        else if (collider.HasMouseExited())
        {
            Tweener.RotateTo(GameObject.Transform, 0f, 0.4f, EaseFunctions.OutBounce);
        }
    }

    protected override void OnDraw()
    {
        Renderer.DrawMousePosition();
    }
}