using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextData : IDisposable
{
    internal SDL.Texture* Texture { get; private set; }
    internal SDL.Surface* Surface { get; private set; }
    private SDL.Color32 Colour { get; set; }
    internal SDL.Rect Dimensions { get; }  
    
    internal TextData(FontData fontData, SDL.Color32 colour, string content, int width)
    {
        Colour = colour;
        Surface = TTF.RenderTextSolidWrapped(fontData.Handle, content, Colour, width);
        Texture = SDL.CreateTextureFromSurface(Graphics.Handle, Surface);
        Dimensions = new SDL.Rect(0, 0, Surface->width, Surface->height);
    }
    
    public void Dispose()
    {
        SDL.DestroyTexture(Texture);
        SDL.DestroySurface(Surface);
    }
}