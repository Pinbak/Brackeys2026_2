using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public class Sprite : Entity
{
    private Texture _texture; 
    
    public Sprite(Texture texture, float x = 0f, float y = 0f, EntityBase? parent = null) : base(x, y, parent)
    {
        _texture = texture;
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
        Renderer.RenderTexture(_texture, Transform);
        base.OnDraw();
    }

}