using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public unsafe class TextRenderer : Component
{
    private readonly Text _text;
    private string _content;
    private Colour _colour;

    public TextRenderer(Font font, Colour colour, string content, int width)
    {
        _content = content;
        _colour = colour;
        _text = Own(new Text(content, font, colour, width));
    }

    public TextRenderer(Font font, string content, int width)
    {
        _content = content;
        _colour = Window.Black;
        _text = Own(new Text(content, font, _colour, width));
    }
    
    protected override void OnInitialise()
    {
    }

    public void Set(string content)
    {
        _text.UpdateText(content);
    }

    // public bool IsPointInside(Point point) => _text.IsPointInside(point);

    protected override void OnDraw()
    {
        _text.Draw(GameObject.Transform);
    }
}