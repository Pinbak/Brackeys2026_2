using System.Runtime.InteropServices;

public static unsafe partial class MIX
{
    private const string Library = "SDL3";
    private const string MixerLibrary = "SDL3_mixer";
    internal const uint DefaultPlaybackDevice = 0xFFFFFFFFu;


    #region SDL Audio

    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioSpec(AudioFormat format, int channels, int sampleRate)
    {
        internal AudioFormat Format = format; // Audio data format
        internal int Channels = channels; // Number of channels: 1 mono, 2 stereo, etc */
        internal int SampleRate = sampleRate; // Sample rate: sample frames per second
    }

    internal enum AudioFormat : uint
    {
        Unknown = 0x0000U, // Unspecified audio format
        U8 = 0x0008U, // Unsigned 8-bit samples
        S8 = 0x8008U, // Signed 8-bit samples
        S16Le = 0x8010U, // Signed 16-bit samples
        S16Be = 0x9010U, // As above, but big-endian byte order
        S32Le = 0x8020U, // 32-bit integer samples
        S32Be = 0x9020U, // As above, but big-endian byte order
        F32Le = 0x8120U, // 32-bit floating point samples
        F32Be = 0x9120U // As above, but big-endian byte order
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial uint SDL_OpenAudioDevice(uint device, AudioSpec* spec);

    internal static uint OpenAudioDevice(uint device, AudioSpec spec) => SDL_OpenAudioDevice(device, &spec);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_CloseAudioDevice(uint device);

    internal static void CloseAudioDevice(uint device) => SDL_CloseAudioDevice(device);

    #endregion

    [StructLayout(LayoutKind.Sequential)]
    internal struct Audio;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Mixer;

    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioTrack;

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_Init();

    internal static bool Init() => MIX_Init();

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void MIX_Quit();

    internal static void Quit() => MIX_Quit();

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Mixer* MIX_CreateMixerDevice(uint deviceId, AudioSpec* spec);

    internal static Mixer* CreateMixerDevice(uint deviceId, AudioSpec spec) =>
        MIX_CreateMixerDevice(deviceId, &spec);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void MIX_DestroyMixer(Mixer* mixer);

    internal static void DestroyMixer(Mixer* mixer) => MIX_DestroyMixer(mixer);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Audio* MIX_LoadAudio_IO(Mixer* mixer, SDL.IoStream* stream,
        [MarshalAs(UnmanagedType.I1)] bool predecode, [MarshalAs(UnmanagedType.I1)] bool close);

    internal static Audio* LoadAudioIo(Mixer* mixer, SDL.IoStream* stream, bool predecode, bool close)
        => MIX_LoadAudio_IO(mixer, stream, predecode, close);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial AudioTrack* MIX_CreateTrack(Mixer* mixer);

    internal static AudioTrack* CreateTrack(Mixer* mixer) => MIX_CreateTrack(mixer);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_SetTrackAudio(AudioTrack* track, Audio* audio);

    internal static bool SetTrackAudio(AudioTrack* track, Audio* audio) => MIX_SetTrackAudio(track, audio);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_SetTrackLoops(AudioTrack* track, int loops);

    internal static bool SetTrackLoops(AudioTrack* track, int loops) => MIX_SetTrackLoops(track, loops);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_TrackPlaying(AudioTrack* track);

    internal static bool TrackPlaying(AudioTrack* track) => MIX_TrackPlaying(track);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_PlayTrack(AudioTrack* track, uint properties);

    internal static bool PlayTrack(AudioTrack* track, uint properties) => MIX_PlayTrack(track, properties);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_PauseTrack(AudioTrack* track);

    internal static bool PauseTrack(AudioTrack* track) => MIX_PauseTrack(track);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_ResumeTrack(AudioTrack* track);

    internal static bool ResumeTrack(AudioTrack* track) => MIX_ResumeTrack(track);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool MIX_StopTrack(AudioTrack* track, long fadeframes);

    internal static bool StopTrack(AudioTrack* track, long fadeframes) => MIX_StopTrack(track, fadeframes);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void MIX_DestroyTrack(AudioTrack* track);

    internal static void DestroyTrack(AudioTrack* track)
        => MIX_DestroyTrack(track);

    [LibraryImport(MixerLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void MIX_DestroyAudio(Audio* audio);

    internal static void DestroyAudio(Audio* audio)
        => MIX_DestroyAudio(audio);
}