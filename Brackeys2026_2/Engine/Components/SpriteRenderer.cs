using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer : Component
{
    protected readonly Texture SpriteTexture;
    private Collider? _collider;
    
    public SpriteRenderer(Texture texture)
    {
        SpriteTexture = texture;
    }

    protected override void OnInitialise()
    {
        var collider = new Collider(GameObject.Transform, SpriteTexture.Width, SpriteTexture.Height);
        GameObject.AddComponent(collider);
        _collider = collider;
    }

    // protected override void OnUpdate()
    // {
    //     if (_collider is null) return;
    //     if (_collider.HasMouseEntered())
    //     {
    //         SpriteTexture.ModifyColour(255, 0, 0);
    //     }
    //     else if (_collider.HasMouseExited())
    //     {
    //         SpriteTexture.ResetColour();
    //     }
    // }

    protected override void OnDraw()
    {
        SpriteTexture.Draw(GameObject.Transform);
    }

}