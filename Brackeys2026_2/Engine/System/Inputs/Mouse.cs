namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Mouse
{
    public enum MouseButton
    {
        None = 0,
        Left = 1,
        Middle = 2,
        Right = 3,
        SideButton1 = 4,
        SideButton2 = 5,
    }

    public static float X { get; private set; }
    public static float Y { get; private set; }

    private static readonly HashSet<MouseButton> HeldButtons = new();
    private static readonly HashSet<MouseButton> PressedButtons = new();
    private static readonly HashSet<MouseButton> ReleasedButtons = new();
    private static bool _mouseMotion;

    internal static void Listen(SDL.Event @event)
    {
        PressedButtons.Clear();
        ReleasedButtons.Clear();

        var button = (MouseButton)@event.mouseButton.button;
        
        switch (@event.type)
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
                X = @event.mouseMotion.x;
                Y = @event.mouseMotion.y;
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