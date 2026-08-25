using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.Entities;

namespace Brackeys2026_2.Engine.System;

public static unsafe class Font
{
    public static FontData New(string fontPath, float size = 24)
    {
        GCHandle gcHandle = default;
        TTF.Font* font;
        try
        {
            var stream = Resources.CreateStream(fontPath, out gcHandle);
            font = TTF.OpenFontIO(stream, true, size);
            if (font == null)
            {
                throw new Exception($"Failed to load font '{fontPath}': {SDL.GetError()}");
            }
                
            // Apply SDF for scaling
            TTF.SetFontSDF(font, true);
        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }

        return new FontData(font);
    }

}