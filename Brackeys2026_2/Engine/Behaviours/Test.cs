using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class Test : UpdatableBehaviour
{
    private Text _text;
    private string _fontPath;

    public Test(string fontPath)
    {
        _fontPath = fontPath;
    }
   
    public override void OnInitialise()
    {
        // GameObject.IsEnabled = false;
        var font = new Font(_fontPath);
        _text = new Text(font, "Hello World");
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnDraw()
    {
        Renderer.RenderText(_text);
    }
}