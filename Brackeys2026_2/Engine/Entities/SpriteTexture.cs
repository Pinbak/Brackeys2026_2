namespace Brackeys2026_2.Engine.Entities;

public class SpriteTexture
{
    public Transform Transform { get; }
    public RawTexture RawTexture { get; }
    public SDL.Rect? Frame { get; } // the part of the texture to render, used for spritesheets
    
    public float TextureWidth => RawTexture.Dimensions.w;
    public float TextureHeight => RawTexture.Dimensions.h;
    
    public SpriteTexture(RawTexture rawTexture, Transform transform, SDL.Rect? frame)
    {
        RawTexture = rawTexture;
        Transform = transform;
        Frame = frame;
    }
}