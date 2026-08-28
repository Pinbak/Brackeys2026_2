namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Mouse
{
    private const int ButtonLeft   = 1;
    private const int ButtonMiddle = 2;
    private const int ButtonRight  = 3;
    private const int ButtonSide1  = 4;
    private const int ButtonSide2  = 5;
    
    public enum WheelDirection
    {
        Normal ,
        Flipped,
    }
    
    [Flags]
    public enum ButtonFlags : uint
    {
        Left         = 1u << (ButtonLeft   - 1),
        Middle       = 1u << (ButtonMiddle - 1),
        Right        = 1u << (ButtonRight  - 1),
        SideButton1  = 1u << (ButtonSide1  - 1),
        SideButton2  = 1u << (ButtonSide2  - 1)
    }
    
    public enum Button
    {
        None,
        Left,
        Middle,
        Right,
        SideButton1,
        SideButton2
    }
    
    public static float X { get; private set; }
    public static float Y { get; private set; }

    private static readonly HashSet<Button> HeldButtons = [];
    private static readonly HashSet<Button> PressedButtons = [];
    private static readonly HashSet<Button> ReleasedButtons = [];
    private static bool _mouseMotion;

    internal static void ClearEvents()
    {
        PressedButtons.Clear();
        ReleasedButtons.Clear();
        _mouseMotion = false;
    }

    internal static void ProcessEvent(SDL.Event @event)
    {
        var button = (Button)@event.OnMouseButton.Button;
        
        switch (@event.Type)
        {
            case SDL.EventType.MouseButtonDown:
                HeldButtons.Add(button);
                PressedButtons.Add(button);
                break;
            
            case SDL.EventType.MouseButtonUp:
                HeldButtons.Remove(button);
                ReleasedButtons.Add(button);
                break;
            
            case SDL.EventType.MouseMotion:
                _mouseMotion = true;
                X = @event.OnMouseMotion.AbsoluteX;
                Y = @event.OnMouseMotion.AbsoluteY;
                break;
        }
    }

    public static bool IsMoving()
    {
        return _mouseMotion;
    }

    public static bool HasPressed(Button button)
    {
        return PressedButtons.Contains(button);
    }
    
    public static bool IsDown(Button button)
    {
        return HeldButtons.Contains(button);
    }

    public static bool HasReleased(Button button)
    {
        return ReleasedButtons.Contains(button);
    }
}