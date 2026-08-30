namespace Brackeys2026_2.Engine.System;

internal static unsafe class Mixer
{
    internal static MIX.Mixer* Handle { get; private set; }

    internal static void Setup()
    {
        Handle = MIX.CreateMixerDevice(MIX.DefaultPlaybackDevice, new MIX.AudioSpec(MIX.AudioFormat.S32Le, 2, 44100));
        if (Handle == null)
        {
            throw new Exception(SDL.GetError());
        }
    }

    internal static void CleanupResources()
    {
        if (Handle == null)
        {
            throw new Exception("Cannot destroy audio mixer as it doesn't exist.");
        }

        MIX.DestroyMixer(Handle);
        Handle = null;
    }

    internal static void PlayTrack(MIX.AudioTrack* track)
    {
        MIX.PlayTrack(track, 0);
    }
    
    internal static void StopTrack(MIX.AudioTrack* track)
    {
        MIX.StopTrack(track, 10);
    }

    internal static void PauseTrack(MIX.AudioTrack* track)
    {
        MIX.PauseTrack(track);
    }
    
    internal static void ResumeTrack(MIX.AudioTrack* track)
    {
        MIX.ResumeTrack(track);
    }

    public static void SetLoop(MIX.AudioTrack* track, int loops)
    {
        MIX.SetTrackLoops(track, loops);
    }
}