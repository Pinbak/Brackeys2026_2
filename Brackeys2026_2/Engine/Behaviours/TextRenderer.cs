using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public unsafe class TextRenderer : UpdatableBehaviour
{
    private Text _text;
    private string _content;
    private Colour _colour;

    public TextRenderer(Font font, Colour colour, string content, int width)
    {
        _content = content;
        _colour = colour;
        _text = new Text(content, font, colour, width);
    }
    
    protected override void OnInitialise()
    {
    }

    public void Set(string content)
    {
        _text.UpdateText(content);
    }

    protected override void OnDraw()
    {
        _text.Draw(GameObject.Transform);
    }
}