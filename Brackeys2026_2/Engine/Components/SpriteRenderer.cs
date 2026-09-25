using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer : Component
{
    protected Texture SpriteTexture;
    private readonly byte _red = 255;
    private readonly byte _green = 255;
    private readonly byte _blue = 255;
    private bool _customColour;
    
    public SpriteRenderer(Texture texture)
    {
        SpriteTexture = texture;
    }

    public SpriteRenderer(Texture texture, byte red, byte green, byte blue)
    {
        SpriteTexture = texture;
        _red = red;
        _green = green;
        _blue = blue;
        _customColour = true;
        if (_red == 255 && _green == 255 && _blue == 255) _customColour = false;
    }

    protected override void OnInitialise()
    {
        // var collider = new Collider(GameObject.Transform, SpriteTexture.Width, SpriteTexture.Height);
        // GameObject.AddComponent(collider);
        // _collider = collider;
    }

    // protected override void OnUpdate()
    // {
    //     if (_collider is null) return;
    //     if (_collider.HasMouseEntered())
    //     {
    //         SpriteTexture.ModifyColour(255, 0, 0);
    //     }
    //     else if (_collider.HasMouseExited())
    //     {
    //         SpriteTexture.ResetColour();
    //     }
    // }

    protected override void OnDraw()
    {
        SpriteTexture.ModifyColour(_red, _green, _blue);
        SpriteTexture.Draw(GameObject.Transform);
    }

    public void ChangeTexture(Texture texture)
    {
        SpriteTexture = texture;
    }

}