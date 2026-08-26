
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextureData : IDisposable
{
    public float Width { get; }
    public float Height { get; }
    internal SDL.Texture* Handle { get; private set; }
    internal Rectangle Dimensions { get; }

    internal TextureData(SDL.Texture* handle, float width, float height)
    {
        Handle = handle;
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
}