using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    // needs field offset as is a union; the following all share the same address in memory
    [StructLayout(LayoutKind.Explicit)]
    internal struct Event
    {
        [FieldOffset(0)] internal EventType Type;

        [FieldOffset(0)] internal CommonEvent OnCommon;

        [FieldOffset(0)] internal DisplayEvent OnDisplay;

        [FieldOffset(0)] internal WindowEvent OnWindow;

        [FieldOffset(0)] internal KeyboardDeviceEvent OnKeyboardDevice;

        [FieldOffset(0)] internal KeyboardEvent OnKeyboard;

        [FieldOffset(0)] internal TextEditingEvent OnTextEditing;

        [FieldOffset(0)] internal TextEditingCandidatesEvent OnTextEditingCandidates;

        [FieldOffset(0)] internal TextInputEvent OnTextInput;

        [FieldOffset(0)] internal MouseDeviceEvent OnMouseDevice;

        [FieldOffset(0)] internal MouseMotionEvent OnMouseMotion;

        [FieldOffset(0)] internal MouseButtonEvent OnMouseButton;

        [FieldOffset(0)] internal MouseWheelEvent OnMouseWheel;

        [FieldOffset(0)] internal JoystickDeviceEvent OnJoystickDevice;

        [FieldOffset(0)] internal JoystickAxisEvent OnJoystickAxis;

        [FieldOffset(0)] internal JoystickBallEvent OnJoystickBall;

        [FieldOffset(0)] internal JoystickHatEvent OnJoystickHat;

        [FieldOffset(0)] internal JoystickButtonEvent OnJoystickButton;

        [FieldOffset(0)] internal GamepadDeviceEvent OnGamepadDevice;

        [FieldOffset(0)] internal GamepadAxisEvent OnGamepadAxis;

        [FieldOffset(0)] internal GamepadButtonEvent OnGamepadButton;

        [FieldOffset(0)] internal AudioDeviceEvent OnAudioDevice;

        [FieldOffset(0)] internal QuitEvent OnQuit;

        [FieldOffset(0)] internal TouchFingerEvent OnTouchFinger;

        [FieldOffset(0)] internal RenderEvent OnRender;

        [FieldOffset(0)] internal ClipboardEvent OnClipboard;

        [FieldOffset(0)] internal fixed byte Padding[128];
    }
}