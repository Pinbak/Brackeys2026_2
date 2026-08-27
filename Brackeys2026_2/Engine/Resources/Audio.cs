using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Audio : Resource, IPlayable, IDisposable
{
    private MIX.Audio* Handle { get; set; }
    private MIX.AudioTrack* _track;
    
    public Audio(string audioPath)
    {
        GCHandle gcHandle = default;
        MIX.Audio* audio;
        try
        {
            var stream = CreateStream(audioPath, out gcHandle);
            audio = MIX.LoadAudioIo(Mixer.Handle, stream, false, true);
            if (audio == null)
            {
                throw new Exception($"Failed to load audio '{audioPath}': {SDL.GetError()}");
            }

            _track = MIX.CreateTrack(Mixer.Handle);
            MIX.SetTrackAudio(_track, audio);
        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }

        Handle = audio;
    }
    
    
    public void Dispose()
    {
        if (Handle != null)
        {
            MIX.DestroyAudio(Handle);
            Handle = null;
        }

        if (_track != null)
        {
            MIX.DestroyTrack(_track);
            _track = null;
        }
        
    }

    public void Play()
    {
        Mixer.PlayTrack(_track);
    }

    public void Pause()
    {
        Mixer.PauseTrack(_track);
    }

    public void Resume()
    {
        Mixer.ResumeTrack(_track);
    }

    public void Stop()
    {
        Mixer.StopTrack(_track);
    }
}