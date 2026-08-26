using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class TextData : IDisposable
{
    private static readonly SDL.Color32 Transparent = new(0, 0, 0, 0);
    public float Width { get; private set; }
    public float Height { get; private set; }
    internal SDL.Texture* Texture { get; private set; }
    private SDL.Surface* Surface { get; set; }
    private SDL.Color32 Colour { get; set; }
    internal SDL.Rect Dimensions { get; private set; }
    internal FontData FontData { get; }
    private readonly int _wrapWidth;
    
    internal TextData(FontData fontData, SDL.Color32 colour, string content, int wrapWidth)
    {
        _wrapWidth = wrapWidth;
        FontData = fontData;
        Colour = colour;
        // https://stackoverflow.com/questions/60181474/how-to-improve-font-quality-in-sdl-ttf-and-dynamically-display-text-and-numbers
        Surface = TTF.RenderTextShadedWrapped(fontData.Handle, content, Colour, Transparent, wrapWidth);
        Texture = SDL.CreateTextureFromSurface(Graphics.Handle, Surface);
        TTF.GetStringSizeWrapped(fontData.Handle, content, wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new SDL.Rect(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
    }
    
    public bool UpdateText(string content)
    {
        // Create the new resources first
        if (FontData.Handle == null)
        {
            throw new Exception("Font was disposed of. Try removing using or not calling dipose it this text needs updating.");
        }
        var newSurface = TTF.RenderTextShadedWrapped(FontData.Handle, content, Colour, Transparent, _wrapWidth);
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

        TTF.GetStringSizeWrapped(FontData.Handle, content, _wrapWidth, out var actualWidth, out var actualHeight);
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