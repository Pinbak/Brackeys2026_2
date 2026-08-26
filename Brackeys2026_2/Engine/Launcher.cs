using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine;

internal static unsafe class Launcher
{
    private static Core? _engine;
    private static Queue<SDL.Event> _pendingEvents = new();
    private static Queue<SDL.Event> _currentEvents = new();
    
    internal static void Setup(Core core)
    {
        _engine = core;
        Initialise();
        SDL.RunApp(0, IntPtr.Zero, &SdlEntry, IntPtr.Zero);
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static int SdlEntry(int argc, IntPtr argv)
    {
        SDL.EnterAppMainCallbacks(argc, argv, &SdlInit, &SdlIterate, &SdlEvent, &SdlQuit);
        return 0;
    }
    
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static SDL.AppResult SdlInit(IntPtr state, int argc, IntPtr argv)
    {
        _engine?.MainInitialise();
        return SDL.AppResult.Continue;
    }
        
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static SDL.AppResult SdlIterate(IntPtr appState)
    {
        var events = _pendingEvents;
        _pendingEvents = new Queue<SDL.Event>();
        _engine?.MainLoop(events);
        _currentEvents.Clear();
        
        if (_engine is null) return StopEngineMessage();
        return !_engine.IsRunning ? StopEngineMessage() : SDL.AppResult.Continue;
        
    }
    
    private static SDL.AppResult StopEngineMessage()
    {
        return SDL.AppResult.Success;
    }
        
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static SDL.AppResult SdlEvent(IntPtr appState, SDL.Event* @event)
    {
        _pendingEvents.Enqueue(*@event);
        {
            return SDL.AppResult.Continue;
        }
    }
        
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SdlQuit(IntPtr appState, SDL.AppResult result)
    {
        if (_engine is null)
        {
            throw new Exception("Tried to quit while engine is null.");
        }
        if (_engine.IsRunning)
        {
            _engine.Quit();
        }
    }

    private static void Initialise()
    {
        if (!SDL.InitSdl(SDL.InitFlags.Video) || !TTF.Init())
        {
            throw new Exception(SDL.GetError());
        }
    }
}