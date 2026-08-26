namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Input
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

    private static readonly HashSet<MouseButton> HeldButtons = new();
    private static readonly HashSet<MouseButton> PressedButtons = new();
    private static readonly HashSet<MouseButton> ReleasedButtons = new();
    private static float _lastX;
    private static float _lastY;
    
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
                _lastX = @event.mouseButton.x;
                _lastY = @event.mouseButton.y;
                break;
            
            case SDL.EventType.MouseButtonUp:
                HeldButtons.Remove(button);
                ReleasedButtons.Add(button);
                _lastX = @event.mouseButton.x;
                _lastY = @event.mouseButton.y;
                break;
        }
    }

    public static bool IsMousePressed(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = _lastX;
        lastY = _lastY;
        var isPressed = PressedButtons.Contains(mouseButton);
        PressedButtons.Remove(mouseButton);
        return isPressed;
    }
    
    public static bool IsMousePressed(MouseButton mouseButton)
    {
        var isPressed = PressedButtons.Contains(mouseButton);
        PressedButtons.Remove(mouseButton);
        return isPressed;
    }

    public static bool IsMouseDown(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = _lastX;
        lastY = _lastY;
        return HeldButtons.Contains(mouseButton);
    }
    
    public static bool IsMouseDown(MouseButton mouseButton)
    {
        return HeldButtons.Contains(mouseButton);
    }

    public static bool IsMouseReleased(MouseButton mouseButton, out float lastX, out float lastY)
    {
        lastX = _lastX;
        lastY = _lastY;
        var isPressed = ReleasedButtons.Contains(mouseButton);
        ReleasedButtons.Remove(mouseButton);
        return isPressed;
    }
    
    public static bool IsMouseReleased(MouseButton mouseButton)
    {
        var isPressed = ReleasedButtons.Contains(mouseButton);
        ReleasedButtons.Remove(mouseButton);
        return isPressed;
    }
}