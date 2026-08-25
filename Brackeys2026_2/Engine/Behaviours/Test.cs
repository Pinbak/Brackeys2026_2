using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Behaviours;

public class Test : UpdatableBehaviour
{
    private Text _text;
    private string _fontPath;
    private Sprite _sprite;

    public Test(Sprite sprite)
    {
        _sprite = sprite;
    }
   
    public override void OnInitialise()
    {
        // GameObject.IsEnabled = false;
        
    }

    public override void OnUpdate()
    {
        GameObject.Transform.ActualX += 100f * Time.DeltaTime;
    }

    public override void OnDraw()
    {
    }
}