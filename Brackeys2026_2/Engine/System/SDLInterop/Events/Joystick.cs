using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System.Inputs;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoystickAxisEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint JoystickID;
        internal byte Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        internal short Value;
        private ushort Padding4;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoystickBallEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint JoystickID;
        internal byte Ball;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        internal short RelativeX;
        internal short RelativeY;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoystickButtonEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint JoystickID;
        internal byte Button;
        internal bool IsDown;
        private byte Padding1;
        private byte Padding2;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoystickDeviceEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint JoystickID;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct JoystickHatEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint JoystickID;
        internal byte Hat;
        internal byte Value;
        private byte Padding1;
        private byte Padding2;
    }
}