using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class SpriteRenderer : UpdatableBehaviour
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