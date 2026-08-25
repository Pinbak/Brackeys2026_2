
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Engine.System;

public static unsafe class Renderer
{
    public static SDL.Renderer* Handle { get; private set; }

    internal static void CreateGraphics()
    {
        Handle = SDL.CreateRenderer(Window.Handle, null);
    }

    internal static void CleanUpResources()
    {
        if (Handle == null)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyRenderer(Handle);
        Handle = null;
    }

    private static void SetDrawColour(byte red = 255, byte green = 255, byte blue = 255, byte alpha = 255)
    {
        SDL.SetRenderDrawColor(Handle, red, green, blue, alpha);
    }

    internal static void DrawBegin(byte red = 255, byte green = 255, byte blue = 255, byte alpha = 255)
    {
        SetDrawColour(red, green, blue, alpha);
        SDL.RenderClear(Handle);
    }

    internal static void DrawEnd()
    {
        SDL.RenderPresent(Handle);
    }

    public static void RenderTexture(RawTexture rawTexture)
    {
        var textureHandle = rawTexture.Handle;
        SDL.RenderTexture(Handle, textureHandle, null, rawTexture.Dimensions);
    }

    public static void RenderTexture(SpriteTexture spriteTexture)
    {
        var transform = spriteTexture.Transform;
        var frame = spriteTexture.Frame;
        
        var newDimensions = new SDL.Rect(transform.X, transform.Y, spriteTexture.TextureWidth * transform.ScaleX * transform.Scale,
            spriteTexture.TextureHeight * transform.ScaleY * transform.Scale);
        SDL.RenderTextureRotated(Handle, spriteTexture.RawTexture.Handle, frame, newDimensions, transform.Angle, null, SDL.FlipMode.None);
    }

    public static void RenderText(Text text)
    {
        var newDimensions = new SDL.Rect(text.Transform.X, text.Transform.Y,
            text.Texture->width * text.Transform.ScaleX * text.Transform.Scale,
            text.Texture->height * text.Transform.ScaleY * text.Transform.Scale);
        if (!SDL.RenderTextureRotated(Handle, text.Texture, null, newDimensions, text.Transform.Angle, null, SDL.FlipMode.None))
        {
            throw new Exception(SDL.GetError());
        }
    }
}