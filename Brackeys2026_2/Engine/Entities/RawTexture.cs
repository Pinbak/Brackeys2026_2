using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class RawTexture
{
    internal SDL.Texture* Handle { get; private set; }
    internal SDL.Rect Dimensions { get; }
    internal SDL.IOStream* Stream { get; }
    
    public RawTexture(string texturePath)
    {
        GCHandle gcHandle = default;
        SDL.Surface* surface = null;

        try
        {
            Stream = Resources.CreateStream(texturePath, out gcHandle);

            surface = IMG.LoadIO(Stream, false);

            if (surface == null)
            {
                throw new Exception(
                    $"Failed to load texture '{texturePath}': {SDL.GetError()}");
            }

            Handle = SDL.CreateTextureFromSurface(
                Renderer.Handle,
                surface);

            if (Handle == null)
            {
                throw new Exception(
                    $"Failed to create texture '{texturePath}': {SDL.GetError()}");
            }

            Dimensions = new SDL.Rect(0, 0, surface->width, surface->height);
        }
        finally
        {
            if (surface != null)
                SDL.DestroySurface(surface);

            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }
    }

    ~RawTexture()
    {
        if (Handle != null)
        {
            SDL.DestroyTexture(Handle);
        }

        Handle = null;
        if (Stream != null)
            SDL.CloseIO(Stream);
    }
    
}