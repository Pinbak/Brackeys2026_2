using SDL3;

namespace Brackeys2026_2.Engine.Entities;

public static class TextureHelpers
{
    public static SDL.FRect GetTextureRect(IntPtr texture, float x, float y)
    {
        SDL.GetTextureSize(texture, out var width, out var height);
        return new SDL.FRect
        {
            H = height,
            W = width,
            X = x,
            Y = y
        };
    }
}