using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextData : IDisposable
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    internal SDL.Texture* Texture { get; private set; }
    private SDL.Surface* Surface { get; set; }
    private SDL.Color32 Colour { get; set; }
    internal SDL.Rect Dimensions { get; private set; }
    private readonly FontData _fontData;
    private readonly int _wrapWidth;
    
    internal TextData(FontData fontData, SDL.Color32 colour, string content, int wrapWidth)
    {
        _wrapWidth = wrapWidth;
        _fontData = fontData;
        Colour = colour;
        Surface = TTF.RenderTextSolidWrapped(fontData.Handle, content, Colour, wrapWidth);
        Texture = SDL.CreateTextureFromSurface(Graphics.Handle, Surface);
        TTF.GetStringSizeWrapped(fontData.Handle, content, wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new SDL.Rect(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
    }
    
    public bool UpdateText(string content)
    {
        // Create the new resources first
        var newSurface = TTF.RenderTextSolidWrapped(_fontData.Handle, content, Colour, _wrapWidth);
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

        TTF.GetStringSizeWrapped(_fontData.Handle, content, _wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new SDL.Rect(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
        return true;
    }
    
    public void Dispose()
    {
        SDL.DestroyTexture(Texture);
        SDL.DestroySurface(Surface);
    }
}