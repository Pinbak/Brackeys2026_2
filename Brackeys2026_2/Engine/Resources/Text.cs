using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Text : Resource, IDrawable, IDisposable
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public string Content { get; private set; }
    private Rectangle Dimensions { get; set; }
    private static readonly Colour Transparent = new(0, 0, 0, 0);
    private SDL.Texture* Handle { get; set; }
    private SDL.Surface* Surface { get; set; }
    private Colour Colour { get; set; }
    private Font Font { get; }
    private readonly int _wrapWidth;
    private bool _isDisposed;
    
    public Text(string content, Font font, int wrapWidth)
    {
        Content = content;
        _wrapWidth = wrapWidth;
        Font = font;
        Colour = Window.Black;
        if (Font.Handle == null) throw new Exception("Font was disposed of. Try removing using or not calling dispose it this text needs updating.");
        // https://stackoverflow.com/questions/60181474/how-to-improve-font-quality-in-sdl-ttf-and-dynamically-display-text-and-numbers
        Surface = TTF.RenderTextShadedWrapped(font.Handle, content, Colour, Transparent, wrapWidth);
        Handle = SDL.CreateTextureFromSurface(Renderer.Handle, Surface);
        TTF.GetStringSizeWrapped(font.Handle, content, wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new Rectangle(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
    }
    
    public Text(string content, Font font, Colour colour, int wrapWidth)
    {
        _wrapWidth = wrapWidth;
        Font = font;
        Colour = colour;
        if (Font.Handle == null) throw new Exception("Font was disposed of. Try removing using or not calling dispose it this text needs updating.");
        // https://stackoverflow.com/questions/60181474/how-to-improve-font-quality-in-sdl-ttf-and-dynamically-display-text-and-numbers
        Surface = TTF.RenderTextShadedWrapped(font.Handle, content, Colour, Transparent, wrapWidth);
        Handle = SDL.CreateTextureFromSurface(Renderer.Handle, Surface);
        TTF.GetStringSizeWrapped(font.Handle, content, wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new Rectangle(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
    }
    
    ~Text()
    {
        Release();
    }
    
    public void Dispose()
    {
        Release();
        GC.SuppressFinalize(this);
    }
    
    private void Release()
    {
        if (_isDisposed) return;
        SDL.DestroyTexture(Handle);
        SDL.DestroySurface(Surface);
        _isDisposed = true;
    }
    
    public bool UpdateText(string content)
    {
        // Create the new resources first
        if (Font.Handle == null)
        {
            throw new Exception("Font was disposed of. Try removing using or not calling dispose it this text needs updating.");
        }
        var newSurface = TTF.RenderTextShadedWrapped(Font.Handle, content, Colour, Transparent, _wrapWidth);
        if (newSurface == null)
            return false;

        var newTexture = SDL.CreateTextureFromSurface(Renderer.Handle, newSurface);
        if (newTexture == null)
        {
            SDL.DestroySurface(newSurface);
            return false;
        }
        if (Handle != null)
            SDL.DestroyTexture(Handle);

        if (Surface != null)
            SDL.DestroySurface(Surface);

        Surface = newSurface;
        Handle = newTexture;

        TTF.GetStringSizeWrapped(Font.Handle, content, _wrapWidth, out var actualWidth, out var actualHeight);
        Dimensions = new Rectangle(0, 0, actualWidth, actualHeight);
        Width = actualWidth;
        Height = actualHeight;
        return true;
    }
    
    public void Draw(Transform position, Rectangle? frame = null)
    {
        Renderer.Draw(Handle, position, Dimensions.Width, Dimensions.Height, frame);
    }
    
}