using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class AnimatedSprite : UpdatableBehaviour
{
    private readonly TextureData _spriteTexture;
    private readonly Rectangle _frame;
    
    public AnimatedSprite(TextureData textureData, Rectangle frame)
    {
        _frame = frame;
        _spriteTexture = textureData;
    }

    public override void OnUpdate()
    {
        // potential for animation
    }

    public override void OnDraw()
    {
        Graphics.Draw(_spriteTexture, GameObject.Transform, _frame);
    }

}