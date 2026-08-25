using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public unsafe class Text
{
    public Font Font { get; private set; }

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

    public Text(Font font, string content)
    {
        Colour = new SDL.Color32(0, 0, 0, 255);
        Font = font;
        Content = content;
    }

    private void UpdateTexture()
    {
        if (Surface != null)
            SDL.DestroySurface(Surface);
        if (Texture != null)
            SDL.DestroyTexture(Texture);
        Surface = TTF.RenderTextShadedWrapped(Font.Handle, Content, Colour, new SDL.Color32(0, 0, 0, 0), Width);
        Texture = SDL.CreateTextureFromSurface(Renderer.Handle, Surface);
    }

    ~Text()
    {
        SDL.DestroyTexture(Texture);
        SDL.DestroySurface(Surface);
    }
}