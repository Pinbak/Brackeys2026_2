using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer : Component
{
    private readonly Texture _spriteTexture;
    
    public SpriteRenderer(Texture texture)
    {
        _spriteTexture = texture;
    }

    protected override void OnInitialise()
    {
    }

    protected override void OnDraw()
    {
        _spriteTexture.Draw(GameObject.Transform);
    }

}