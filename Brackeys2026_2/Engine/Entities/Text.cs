using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public unsafe class Text
{
    public FontData FontData { get; private set; }

    public string Content
    {
        get;
        set
        {
            field = value;
            UpdateTexture();
        }
    }

    public int Width
    {
        get;
        set
        {
            field = value;
            UpdateTexture();
        }
    } = 150;

    public SDL.Color32 Colour { get; private set; }
    
    internal SDL.Texture* Texture { get; private set; }
    internal SDL.Surface* Surface { get; private set; }

    public Transform Transform { get; set; } = new Transform();

    public Text(FontData fontData, string content)
    {
        Colour = new SDL.Color32(0, 0, 0, 255);
        FontData = fontData;
        Content = content;
    }

    private void UpdateTexture()
    {
        if (Texture != null)
            SDL.DestroyTexture(Texture);
        var surface = TTF.RenderTextSolidWrapped(FontData.Handle, Content, Colour, Width);
        Texture = SDL.CreateTextureFromSurface(Renderer.Handle, surface);
        SDL.DestroySurface(surface);
    }

    ~Text()
    {
        SDL.DestroyTexture(Texture);
        SDL.DestroySurface(Surface);
    }
}