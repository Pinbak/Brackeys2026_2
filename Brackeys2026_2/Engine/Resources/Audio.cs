using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Audio : Resource, IPlayable, IDisposable
{
    private MIX.Audio* Handle { get; set; }
    private MIX.AudioTrack* _track;
    internal SDL.IoStream* Stream { get; set; }
    internal GCHandle GCHandle;
    private bool _isDisposed;
    
    public Audio(string audioPath)
    {
        Stream = CreateStream(audioPath, out GCHandle);
        Handle = MIX.LoadAudioIo(Mixer.Handle, Stream, false, false);
        if (Handle == null)
        {
            throw new Exception($"Failed to load audio '{audioPath}': {SDL.GetError()}");
        }

        _track = MIX.CreateTrack(Mixer.Handle);
        MIX.SetTrackAudio(_track, Handle);
    }

    ~Audio()
    {
        Release();
    }
    
    public void Dispose()
    {
        Release();
        GC.SuppressFinalize(this);
    }

    private void Release()
    {
        if (_isDisposed) return;
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

        if (GCHandle.IsAllocated)
        {
            GCHandle.Free();
        }

        if (Stream != null)
        {
            SDL.CloseIo(Stream);
        }
        _isDisposed = true;
    }

    public void Loop()
    {
        Mixer.SetLoop(_track, -1);
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