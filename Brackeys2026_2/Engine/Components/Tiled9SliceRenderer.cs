using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class Tiled9SliceSpriteRenderer : SpriteRenderer
{
    private float _scale;
    private float _width;
    private float _height;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;
    
    public Tiled9SliceSpriteRenderer(Texture texture, float width, float height, float left, float right, float top, float bottom, float scale = 1f) : base(texture)
    {
        _width = width;
        _height = height;
        _left = left;
        _right = right;
        _top = top;
        _bottom = bottom;
        _scale = scale;
    }
    
    public Tiled9SliceSpriteRenderer(Texture texture, float width, float height, float sides, float scale = 1f) : base(texture)
    {
        _width = width;
        _height = height;
        _left = sides;
        _right = sides;
        _top = sides;
        _bottom = sides;
        _scale = scale;
    }

    protected override void OnInitialise()
    {
        
    }

    protected override void OnDraw()
    {
        SpriteTexture.Draw9Slice(GameObject.Transform, _width, _height, _left, _right, _top, _bottom, _scale);
    }
}