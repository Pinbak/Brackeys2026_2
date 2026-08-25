
namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextureData : IDisposable
{
    internal SDL.Texture* Handle { get; private set; }
    internal SDL.Rect Dimensions { get; }
    
    internal TextureData(SDL.Texture* handle, float width, float height)
    {
        Handle = handle;
        Dimensions = new SDL.Rect(0, 0, width, height);
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