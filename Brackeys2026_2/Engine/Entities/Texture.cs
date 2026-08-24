using Brackeys2026_2.Engine.System;
using SDL3;

namespace Brackeys2026_2.Engine.Entities;

public sealed class Texture
{
    private readonly IntPtr _surface;
    private readonly IntPtr _texture;
    private readonly SDL.FRect _dimensions;
    
    public Texture(string texturePath, float x, float y)
    {
        _surface = SDL.LoadPNG(texturePath);
        if (_surface == IntPtr.Zero)
        {
            SDL.Log($"Failed to load image {SDL.GetError()}.");
            throw new Exception($"Failed to load texture from {texturePath}.");
        }

        _texture = SDL.CreateTextureFromSurface(Renderer.Handle, _surface);
        if (_texture == IntPtr.Zero)
        {
            SDL.Log($"Failed to load image {SDL.GetError()}.");
            throw new Exception($"Failed to load texture from {texturePath}.");
        }

        _dimensions = TextureHelpers.GetTextureRect(_texture, x, y);
    }

    ~Texture()
    {
        if (_surface != IntPtr.Zero)
        {
            SDL.DestroySurface(_surface);
        }

        if (_texture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_texture);
        }
    }

    public void Draw()
    {
        SDL.RenderTexture(Renderer.Handle, _texture, IntPtr.Zero, _dimensions);
    }
    
    
}