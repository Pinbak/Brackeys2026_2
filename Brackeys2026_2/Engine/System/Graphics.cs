
using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.Behaviours;
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Engine.System;

public static unsafe class Graphics
{
    internal static SDL.Renderer* Handle { get; private set; }

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

    public static TextData NewText(FontData fontData, SDL.Color colour, string content, int width)
    {
        var colour32 = new SDL.Color32(colour.r, colour.g, colour.b, colour.a);
        return new TextData(fontData, colour32, content, width);
    }

    public static TextureData NewTexture(string texturePath)
    {
        GCHandle gcHandle = default;
        SDL.Texture* texture;
        float width, height;
        try
        {
            var stream = Resources.CreateStream(texturePath, out gcHandle);
            var surface = IMG.LoadIO(stream, true);
            if (surface == null)
            {
                throw new Exception($"Failed to load texture '{texturePath}': {SDL.GetError()}");
            }

            texture = SDL.CreateTextureFromSurface(Handle, surface);
            if (texture == null)
            {
                throw new Exception($"Failed to create texture '{texturePath}': {SDL.GetError()}");
            }

            width = surface->width;
            height = surface->height;
            SDL.DestroySurface(surface);

        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }

        return new TextureData(texture, width, height);
    }

    public static void Draw(TextureData textureData)
    {
        var textureHandle = textureData.Handle;
        SDL.RenderTexture(Handle, textureHandle, null, textureData.Dimensions);
    }
    
    public static void Draw(TextureData textureData, Transform transform)
    {
        Draw(textureData, transform.ActualX, transform.ActualY, transform.ActualAngle, transform.ActualScale, null);
    }
    
    public static void Draw(TextureData textureData, Transform transform, SDL.Rect frame)
    {
        Draw(textureData, transform.ActualX, transform.ActualY, transform.ActualAngle, transform.ActualScale, frame);
    }

    private static void Draw(TextureData textureData, float x, float y, float angle, float scale, SDL.Rect? frame)
    {
        SDL.RenderTextureRotated(Handle, textureData.Handle, null,
            new SDL.Rect(x, y, textureData.Dimensions.w * scale, textureData.Dimensions.h * scale), angle, null,
            SDL.FlipMode.None);
    }

    public static void Draw(TextData text, Transform transform)
    {
        SDL.RenderTextureRotated(Handle, text.Texture, null,
            new SDL.Rect(transform.ActualX, transform.ActualY, text.Dimensions.w * transform.ActualScale,
                text.Dimensions.h * transform.ActualScale), transform.ActualAngle, null,
            SDL.FlipMode.None);
    }
}