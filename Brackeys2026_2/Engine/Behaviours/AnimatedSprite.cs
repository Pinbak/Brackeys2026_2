using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class AnimatedSprite : UpdatableBehaviour
{
    private readonly Texture _spriteTexture;
    private readonly Rectangle _frame;
    
    public AnimatedSprite(Texture texture, Rectangle frame)
    {
        _frame = frame;
        _spriteTexture = texture;
    }

    protected override void OnUpdate()
    {
        // potential for animation
    }

    protected override void OnDraw()
    {
        _spriteTexture.Draw(GameObject.Transform, _frame);
    }

}