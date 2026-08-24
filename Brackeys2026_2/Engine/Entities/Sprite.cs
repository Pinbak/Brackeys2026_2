using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public class Sprite : GameObject
{
    private readonly SpriteTexture _spriteTexture; 
    
    public Sprite(RawTexture rawTexture, SDL.Rect? frame = null, float x = 0f, float y = 0f, GameObjectBase? parent = null) : base(x, y, parent)
    {
        _spriteTexture = new SpriteTexture(rawTexture, Transform, frame);
    }

    public override void OnInitialise()
    {
        base.OnInitialise();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnDraw()
    {
        Renderer.RenderTexture(_spriteTexture);
        base.OnDraw();
    }

}