using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.System;

public sealed unsafe class Font
{
    internal TTF.Font* Handle { get; private set; }
    internal SDL.IOStream* Stream { get; set; }
    
    public Font(string fontPath, float size = 24)
    {
        GCHandle gcHandle = default;
        try
        {
            Stream = Resources.CreateStream(fontPath, out gcHandle);
            Handle = TTF.OpenFontIO(Stream, false, size);
            if (Handle == null)
            {
                Console.WriteLine(SDL.GetError());
                throw new Exception($"Failed to load font '{fontPath}': {SDL.GetError()}");
            }
                
            // Apply SDF for scaling
            TTF.SetFontSDF(Handle, true);
        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }
    }

    ~Font()
    {
        if (Handle == null) return;
        TTF.CloseFont(Handle);
        Handle = null;
        if (Stream != null)
            SDL.CloseIO(Stream);
    }
}