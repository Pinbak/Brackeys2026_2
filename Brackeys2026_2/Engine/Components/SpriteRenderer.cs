using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer : Component
{
    private readonly Texture _spriteTexture;
    private Collider _collider = null!;
    
    public SpriteRenderer(Texture texture)
    {
        _spriteTexture = texture;
    }

    protected override void OnInitialise()
    {
        var collider = new Collider(GameObject.Transform, _spriteTexture.Width, _spriteTexture.Height);
        GameObject.AddComponent(collider);
        _collider = collider;
    }

    protected override void OnUpdate()
    {
        if (_collider.HasMouseEntered())
        {
            _spriteTexture.ModifyColour(255, 0, 0);
        }
        else if (_collider.HasMouseExited())
        {
            _spriteTexture.ResetColour();
        }
    }

    protected override void OnDraw()
    {
        _spriteTexture.Draw(GameObject.Transform);
    }

}