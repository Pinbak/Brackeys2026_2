using System.Globalization;
using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Game.Classes;

public class FpsCounter(Font font) : Component
{
    private Text _text;

    protected override void OnInitialise()
    {
        _text = new Text("Test", font, 250);
    }
    
    protected override void OnUpdate()
    {
        // var childrenCount = GameObjectBase.RootGameObject.CountChildren(0);
        _text.UpdateText($"{(int)Time.Fps}");
    }

    protected override void OnDraw()
    {
        _text.Draw(GameObject.Transform);
    }
}