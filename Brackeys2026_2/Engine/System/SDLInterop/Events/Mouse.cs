using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System.Inputs;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseButtonEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal uint MouseID;
        internal byte Button;
        internal bool Down;
        private byte Clicks;
        private byte Padding;
        internal float X;
        internal float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseDeviceEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint MouseID;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseMotionEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal uint MouseID;
        internal Mouse.MouseButtonFlags State;
        internal float AbsoluteX;
        internal float AbsoluteY;
        internal float RelativeX;
        internal float RelativeY;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseWheelEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal uint MouseID;
        internal float X;
        internal float Y;
        internal Mouse.MouseWheelDirection Direction;
        internal float MouseX;
        internal float MouseY;
        internal int IntMouseX;
        internal int IntMouseY;
    }
}