
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Engine.System;

public sealed unsafe class Renderer : SystemComponent
{
    public static SDL.Renderer* Handle { get; private set; }

    internal override void OnInitialise()
    {
        Handle = SDL.CreateRenderer(Window.Handle, null);
    }

    internal override void OnDrawBegin()
    {
        SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
        SDL.RenderClear(Handle);
    }

    internal override void OnDrawEnd()
    {
        SDL.RenderPresent(Handle);
    }

    internal override void OnRemove()
    {
        if (Handle == null)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyRenderer(Handle);
        Handle = null;
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
        SDL.RenderTexture(Handle, spriteTexture.RawTexture.Handle, frame, newDimensions);
    }

    public static void RenderText(Text text)
    {
        var newDimensions = new SDL.Rect(text.Transform.X, text.Transform.Y,
            text.Texture->width * text.Transform.ScaleX * text.Transform.Scale,
            text.Texture->height * text.Transform.ScaleY * text.Transform.Scale);
        if (!SDL.RenderTexture(Handle, text.Texture, null, newDimensions))
        {
            throw new Exception(SDL.GetError());
        }
    }
}