
using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class Texture : Collidable2D, IDrawable, IDisposable
{
    private SDL.Texture* Handle { get; set; }

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

            texture = SDL.CreateTextureFromSurface(Graphics.Handle, surface);
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

    public void Dispose()
    {
        if (Handle != null)
        {
            SDL.DestroyTexture(Handle);
        }
        Handle = null;
    }

    public void Draw(Transform position, Rectangle? frame = null)
    {
        Graphics.Draw(Handle, position, Dimensions.Width, Dimensions.Height, frame);
    }
}