using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System.Inputs;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct GamepadAxisEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint GamepadID;
        internal Gamepad.GamepadAxis Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        internal short Value;
        private ushort Padding4;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct GamepadButtonEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint GamepadID;
        internal Gamepad.GamepadButton Button;
        internal bool Down;
        private byte Padding1;
        private byte Padding2;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct GamepadDeviceEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint GamepadID;
    }
}