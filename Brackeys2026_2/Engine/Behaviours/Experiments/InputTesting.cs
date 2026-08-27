using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Engine.Behaviours.Experiments;

public class InputTesting : UpdatableBehaviour
{
    protected override void OnUpdate()
    {
        if (Mouse.IsPressed(Mouse.Button.Left))
        {
            Tweener.MoveXTo(GameObject.Transform, GameObject.Transform.X + 50, 2f, EaseFunctions.OutElastic);
            // Tweener.ScaleTo(GameObject.Transform, 2f, 5f, EaseFunctions.OutElastic);
            
            Console.WriteLine($"Left button pressed x: {Mouse.X}, y: {Mouse.Y}");
        }

        if (Mouse.IsReleased(Mouse.Button.Left))
        {
            Console.WriteLine("Left button released");
        }
        
        // Console.WriteLine(Time.Fps);
    }
}