using SDL3;

namespace Brackeys2026_2.Engine.System;

public sealed class Window : SystemComponent
{
    private IntPtr _this;

    internal override void OnInitialise()
    {
        _this = SDL.CreateWindow("", 800, 600, SDL.WindowFlags.Hidden);
        Show();
    }

    internal override void OnRemove()
    {
        if (_this == IntPtr.Zero)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyWindow(_this);
    }

    internal override void OnEvent(SDL.Event @event)
    {
    }
    
    internal void Show()
    {
        SDL.ShowWindow(_this);
        SDL.RaiseWindow(_this);
    }
}