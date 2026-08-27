using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class Font : Resource, IDisposable
{
    internal TTF.Font* Handle { get; private set; }
    
    public Font(string fontPath, float size = 24)
    {
        GCHandle gcHandle = default;
        TTF.Font* font;
        try
        {
            var stream = CreateStream(fontPath, out gcHandle);
            font = TTF.OpenFontIo(stream, true, size);
            if (font == null)
            {
                throw new Exception($"Failed to load font '{fontPath}': {SDL.GetError()}");
            }
                
            // Apply SDF for scaling
            TTF.SetFontSdf(font, true);
        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }

        Handle = font;
    }
    
    public void Dispose()
    {
        if (Handle != null)
            TTF.CloseFont(Handle);
        Handle = null;
    }
}