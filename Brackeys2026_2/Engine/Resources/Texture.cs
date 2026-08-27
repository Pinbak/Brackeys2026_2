using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Texture : Collidable2D, IDrawable, IDisposable
{
    private SDL.Texture* Handle { get; set; }
    private bool _isDisposed;

    public Texture(string texturePath)
    {
        GCHandle gcHandle = default;
        SDL.Texture* texture;
        float width, height;
        try
        {
            var stream = CreateStream(texturePath, out gcHandle);
            var surface = IMG.LoadIo(stream, true);
            if (surface == null)
            {
                throw new Exception($"Failed to load texture '{texturePath}': {SDL.GetError()}");
            }

            texture = SDL.CreateTextureFromSurface(Renderer.Handle, surface);
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
        
        Handle = texture;
        Dimensions = new Rectangle(0, 0, width, height);
        Width = width;
        Height = height;
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
        Handle = null;
        _isDisposed = true;
    }

    public void Draw(Transform position, Rectangle? frame = null)
    {
        Renderer.Draw(Handle, position, Dimensions.Width, Dimensions.Height, frame);
    }
}