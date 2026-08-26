using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.Behaviours.ExperimentBehaviours;

public class InputTesting : UpdatableBehaviour
{
    
    public override void OnUpdate()
    {
        if (Input.IsMousePressed(Input.MouseButton.Left, out var x, out var y))
        {
            Console.WriteLine($"Left button pressed x: {x}, y: {y}");
        }

        if (Input.IsMouseReleased(Input.MouseButton.Left))
        {
            Console.WriteLine("Left button released");
        }
        
    }
}