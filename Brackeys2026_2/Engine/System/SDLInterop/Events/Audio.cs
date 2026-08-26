using System.Runtime.InteropServices;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioDeviceEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint AudioDeviceID;
        internal bool Recording;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }
}