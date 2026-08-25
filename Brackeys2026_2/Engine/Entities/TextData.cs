using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextData : IDisposable
{
    internal SDL.Texture* Texture { get; private set; }
    private SDL.Surface* Surface { get; set; }
    private SDL.Color32 Colour { get; set; }
    internal SDL.Rect Dimensions { get; private set; }
    private readonly FontData _fontData;
    private readonly int _width;
    
    internal TextData(FontData fontData, SDL.Color32 colour, string content, int width)
    {
        _width = width;
        _fontData = fontData;
        Colour = colour;
        Surface = TTF.RenderTextSolidWrapped(fontData.Handle, content, Colour, width);
        Texture = SDL.CreateTextureFromSurface(Graphics.Handle, Surface);
        Dimensions = new SDL.Rect(0, 0, Surface->width, Surface->height);
    }
    
    public bool UpdateText(string content)
    {
        // Create the new resources first
        var newSurface = TTF.RenderTextSolidWrapped(_fontData.Handle, content, Colour, _width);
        if (newSurface == null)
            return false;

        var newTexture = SDL.CreateTextureFromSurface(Graphics.Handle, newSurface);
        if (newTexture == null)
        {
            SDL.DestroySurface(newSurface);
            return false;
        }
        if (Texture != null)
            SDL.DestroyTexture(Texture);

        if (Surface != null)
            SDL.DestroySurface(Surface);

        Surface = newSurface;
        Texture = newTexture;

        Dimensions = new SDL.Rect(
            0,
            0,
            Surface->width,
            Surface->height
        );
        return true;
    }
    
    public void Dispose()
    {
        SDL.DestroyTexture(Texture);
        SDL.DestroySurface(Surface);
    }
}