using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TouchFingerEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal ulong TouchDeviceID;
        internal ulong FingerID;
        internal float X;
        internal float Y;
        internal float DeltaX;
        internal float DeltaY;
        internal float Pressure;
        internal uint WindowID;
    }
}