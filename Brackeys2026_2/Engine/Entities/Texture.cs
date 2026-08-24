using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class Texture
{
    private SDL.Texture* _texture;
    private readonly SDL.Rect _dimensions;
    
    public Texture(string texturePath, float x, float y)
    {
        GCHandle gcHandle = default;
        SDL.IOStream* stream = null;
        SDL.Surface* surface = null;

        try
        {
            stream = Resources.CreateStream(texturePath, out gcHandle);

            surface = SDL.LoadIO(stream, false);

            if (surface == null)
            {
                throw new Exception(
                    $"Failed to load texture '{texturePath}': {SDL.GetError()}");
            }

            _texture = SDL.CreateTextureFromSurface(
                Renderer.Handle,
                surface);

            if (_texture == null)
            {
                throw new Exception(
                    $"Failed to create texture '{texturePath}': {SDL.GetError()}");
            }

            _dimensions = new SDL.Rect(
                x,
                y,
                surface->width,
                surface->height);
        }
        finally
        {
            if (surface != null)
                SDL.DestroySurface(surface);

            if (stream != null)
                SDL.CloseIO(stream);

            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }
    }

    ~Texture()
    {
        if (_texture != null)
        {
            SDL.DestroyTexture(_texture);
        }

        _texture = null;
    }

    public void Draw()
    {
        SDL.RenderTexture(Renderer.Handle, _texture, null, _dimensions);
    }
}