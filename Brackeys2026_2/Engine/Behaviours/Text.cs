using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public unsafe class Text : UpdatableBehaviour
{
    private TextData _textData;
    private string _content;
    private Colour _colour;

    public Text(FontData fontData, Colour colour, string content, int width)
    {
        _content = content;
        _colour = colour;
        _textData = Graphics.NewText(fontData, colour, content, width);
    }
    
    public override void OnInitialise()
    {
    }

    public void Set(string content)
    {
        _textData.UpdateText(content);
    }

    public override void OnDraw()
    {
        Graphics.Draw(_textData, GameObject.Transform);
    }
}