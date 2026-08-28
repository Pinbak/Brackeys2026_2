using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class TiledSpriteRenderer : SpriteRenderer
{
    private float _scale;
    private float _width;
    private float _height;
    
    public TiledSpriteRenderer(Texture texture, float width, float height, float scale = 1f) : base(texture)
    {
        _width = width;
        _height = height;
        _scale = scale;
    }

    protected override void OnInitialise()
    {
        
    }

    protected override void OnDraw()
    {
        SpriteTexture.DrawTiled(GameObject.Transform, _width, _height, _scale);
    }
}