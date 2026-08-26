using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioSpec
    {
        internal AudioFormat Format;    // Audio data format
        internal int Channels;          // Number of channels: 1 mono, 2 stereo, etc */
        internal int SampleRate;        // Sample rate: sample frames per second
    }
    
    internal enum AudioFormat : uint
    {
        Unknown = 0x0000U,              // Unspecified audio format
        U8 = 0x0008U,                   // Unsigned 8-bit samples
        S8 = 0x8008U,                   // Signed 8-bit samples
        S16Le = 0x8010U,                // Signed 16-bit samples
        S16Be = 0x9010U,                // As above, but big-endian byte order
        S32Le = 0x8020U,                // 32-bit integer samples
        S32Be = 0x9020U,                // As above, but big-endian byte order
        F32Le = 0x8120U,                // 32-bit floating point samples
        F32Be = 0x9120U                 // As above, but big-endian byte order
    }
    
    // Default Devices
    internal const uint DefaultPlaybackDevice = 0xFFFFFFFFu;
    internal const uint DefaultRecordingDevice = 0xFFFFFFFEu;

    // Open Audio Device
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial uint SDL_OpenAudioDevice(uint device, AudioSpec* spec);

    internal static uint OpenAudioDevice(uint device, AudioSpec spec) => SDL_OpenAudioDevice(device, &spec);

    // Close Audio Device
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_CloseAudioDevice(uint device);

    internal static void CloseAudioDevice(uint device) => SDL_CloseAudioDevice(device);
}