namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Gamepad
{
    public enum GamepadType
    {
        TypeUnknown,
        TypeStandard,
        TypeXbox360,
        TypeXboxone,
        TypePs3,
        TypePs4,
        TypePs5,
        TypeNintendoSwitchPro,
        TypeNintendoSwitchJoyconLeft,
        TypeNintendoSwitchJoyconRight,
        TypeNintendoSwitchJoyconPair,
        TypeGamecube,
        TypeSteam,
        TypeCount,
    }
    
    public enum GamepadAxis
    {
        AxisInvalid = -1,
        AxisLeftx,
        AxisLefty,
        AxisRightx,
        AxisRighty,
        AxisLeftTrigger,
        AxisRightTrigger,
        AxisCount,
    }
    
    public enum GamepadButton
    {
        ButtonInvalid = -1,
        ButtonSouth,
        ButtonEast,
        ButtonWest,
        ButtonNorth,
        ButtonBack,
        ButtonGuide,
        ButtonStart,
        ButtonLeftStick,
        ButtonRightStick,
        ButtonLeftShoulder,
        ButtonRightShoulder,
        ButtonDpadUp,
        ButtonDpadDown,
        ButtonDpadLeft,
        ButtonDpadRight,
        ButtonMisc1,
        ButtonRightPaddle1,
        ButtonLeftPaddle1,
        ButtonRightPaddle2,
        ButtonLeftPaddle2,
        ButtonTouchpad,
        ButtonMisc2,
        ButtonMisc3,
        ButtonMisc4,
        ButtonMisc5,
        ButtonMisc6,
        ButtonCount,
    }
}