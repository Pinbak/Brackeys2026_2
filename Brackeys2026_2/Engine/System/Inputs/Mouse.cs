namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Mouse
{
    private const int ButtonLeft   = 1;
    private const int ButtonMiddle = 2;
    private const int ButtonRight  = 3;
    private const int ButtonSide1  = 4;
    private const int ButtonSide2  = 5;
    
    public enum MouseWheelDirection
    {
        Normal ,
        Flipped,
    }
    
    [Flags]
    public enum MouseButtonFlags : uint
    {
        Left         = 1u << (ButtonLeft   - 1),
        Middle       = 1u << (ButtonMiddle - 1),
        Right        = 1u << (ButtonRight  - 1),
        SideButton1  = 1u << (ButtonSide1  - 1),
        SideButton2  = 1u << (ButtonSide2  - 1)
    }
    
    public enum MouseButton
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

    private static readonly HashSet<MouseButton> HeldButtons = [];
    private static readonly HashSet<MouseButton> PressedButtons = [];
    private static readonly HashSet<MouseButton> ReleasedButtons = [];
    private static bool _mouseMotion;

    internal static void Listen(SDL.Event @event)
    {
        PressedButtons.Clear();
        ReleasedButtons.Clear();

        var button = (MouseButton)@event.OnMouseButton.Button;
        
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

    public static bool IsPressed(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = X;
        lastY = Y;
        var isPressed = PressedButtons.Contains(mouseButton);
        PressedButtons.Remove(mouseButton);
        return isPressed;
    }
    
    public static bool IsPressed(MouseButton mouseButton)
    {
        var isPressed = PressedButtons.Contains(mouseButton);
        PressedButtons.Remove(mouseButton);
        return isPressed;
    }

    public static bool IsDown(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = X;
        lastY = Y;
        return HeldButtons.Contains(mouseButton);
    }
    
    public static bool IsDown(MouseButton mouseButton)
    {
        return HeldButtons.Contains(mouseButton);
    }

    public static bool IsReleased(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = X;
        lastY = Y;
        var isPressed = ReleasedButtons.Contains(mouseButton);
        ReleasedButtons.Remove(mouseButton);
        return isPressed;
    }
    
    public static bool IsReleased(MouseButton mouseButton)
    {
        var isPressed = ReleasedButtons.Contains(mouseButton);
        ReleasedButtons.Remove(mouseButton);
        return isPressed;
    }
}