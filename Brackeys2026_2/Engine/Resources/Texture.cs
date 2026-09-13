using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Texture : Resource, IDrawable, IDisposable
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    private Rectangle Dimensions { get; set; }
    private SDL.Texture* Handle { get; set; }
    internal SDL.IoStream* Stream { get; set; }
    internal GCHandle GCHandle;
    private bool _isDisposed;

    public Texture(string texturePath)
    {
        Stream = CreateStream(texturePath, out GCHandle);
        var surface = IMG.LoadIo(Stream, false);
        if (surface == null)
        {
            throw new Exception($"Failed to load texture '{texturePath}': {SDL.GetError()}");
        }

        Handle = SDL.CreateTextureFromSurface(Renderer.Handle, surface);
        if (Handle == null)
        {
            throw new Exception($"Failed to create texture '{texturePath}': {SDL.GetError()}");
        }

        Width = surface->width;
        Height = surface->height;
        Dimensions = new Rectangle(0, 0, Width, Height);
        SDL.DestroySurface(surface);
    }

    ~Texture()
    {
        Release();
    }

    public void Dispose()
    {
        Release();
        GC.SuppressFinalize(this);
    }
    
    private void Release()
    {
        if (_isDisposed) return;
        if (Handle != null)
        {
            SDL.DestroyTexture(Handle);
        }

        if (GCHandle.IsAllocated)
        {
            GCHandle.Free();
        }

        if (Stream != null)
        {
            SDL.CloseIo(Stream);
        }
        Handle = null;
        _isDisposed = true;
    }

    public void Draw(Transform position, Rectangle? frame = null)
    {
        Renderer.Draw(Handle, position, Dimensions.Width, Dimensions.Height, frame);
    }
    
    public void Draw(Transform position, Colour colourTint, Rectangle? frame = null)
    {
        Renderer.Draw(Handle, position, Dimensions.Width, Dimensions.Height, frame);
    }

    public void DrawTiled(Transform position, float width, float height, float tileScale = 1f)
    {
        var dimensions = new Rectangle(position.ActualX, position.ActualY, width, height);
        Renderer.Draw(Handle, dimensions, tileScale);
    }

    public void Draw9Slice(Transform position, float width, float height, float left, float right, float top, float bottom, float tileScale = 1f)
    {
        var dimensions = new Rectangle(position.ActualX, position.ActualY, width, height);
        Renderer.Draw(Handle, dimensions, left, right, top, bottom, tileScale);
    }

    public void ModifyColour(byte red, byte green, byte blue)
    {
        SDL.SetTextureColorMod(Handle, red, green, blue);
    }

    public void ResetColour()
    {
        SDL.SetTextureColorMod(Handle, 255, 255, 255);
    }
}