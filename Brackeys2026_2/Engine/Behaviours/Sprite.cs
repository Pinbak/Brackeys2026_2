using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class Sprite : UpdatableBehaviour
{
    private readonly TextureData _spriteTexture;
    
    public Sprite(TextureData textureData)
    {
        _spriteTexture = textureData;
    }

    public override void OnDraw()
    {
        Graphics.Draw(_spriteTexture, GameObject.Transform);
    }

}