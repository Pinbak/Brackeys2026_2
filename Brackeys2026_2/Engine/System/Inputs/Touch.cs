namespace Brackeys2026_2.Engine.System.Inputs;

public static unsafe class Touch
{
    internal enum TouchDeviceType
    {
        Invalid          = -1,
        Direct           =  0,
        IndirectAbsolute =  1,
        IndirectRelative =  2,
    }
}