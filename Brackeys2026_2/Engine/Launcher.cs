using SDL3;

namespace Brackeys2026_2.Engine;

internal static class Launcher
{
    private static Core? _engine;
    private static Queue<SDL.Event> _pendingEvents = new();
    private static Queue<SDL.Event> _currentEvents = new();
    
    internal static void Setup(Core core)
    {
        _engine = core;
        Initialise();
        {
            SDL.RunApp(0, [], SdlEntry, (IntPtr)505);
        }
        
    }
    
    private static int SdlEntry(int argc, string[]? argv)
    {
        SDL.EnterAppMainCallbacks(0, [], SdlInit, SdlIterate, SdlEvent, SdlQuit);
        return 0;
    }
    
    private static SDL.AppResult SdlInit(ref IntPtr appState, int argc, string[]? argv)
    {
        _engine?.MainInitialise();
        return SDL.AppResult.Continue;
    }
        
    private static SDL.AppResult SdlIterate(IntPtr appState)
    {
        (_pendingEvents, _currentEvents) = (_currentEvents, _pendingEvents); // get and clear the current events
        {
            _engine?.MainLoop(_currentEvents);
            if (_engine is null) return StopEngineMessage();
            return !_engine.IsRunning ? StopEngineMessage() : SDL.AppResult.Continue;
        }
    }

    private static SDL.AppResult StopEngineMessage()
    {
        return SDL.AppResult.Success;
    }
        
    private static SDL.AppResult SdlEvent(IntPtr appState, ref SDL.Event @event)
    {
        _pendingEvents.Enqueue(@event);
        {
            return SDL.AppResult.Continue;
        }
    }
        
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
        SDL.SetMainReady();

        if (!SDL.Init(SDL.InitFlags.Video))
        {
            throw new Exception(SDL.GetError());
        }
    }
}