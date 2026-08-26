using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System.Inputs;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal uint KeyboardID;
        internal Keyboard.ScanCode ScanCode;
        internal Keyboard.KeyCode KeyCode;
        internal Keyboard.KeyModifier KeyModifier;
        internal ushort Raw;
        internal bool Down;
        internal bool Repeat;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardDeviceEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint KeyboardID;
    }
}