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
        GameObject.Transform.X += 100f * Time.DeltaTime;
        // Console.WriteLine(Time.Fps);
    }

    public override void OnDraw()
    {
    }
}