using SDL3;

namespace Brackeys2026_2.Engine.System;

public sealed class Window : SystemComponent
{
    public static IntPtr Handle { get; private set; }

    internal override void OnInitialise()
    {
        Handle = SDL.CreateWindow("", 800, 600, SDL.WindowFlags.Hidden);
        Show();
    }

    internal override void OnRemove()
    {
        if (Handle == IntPtr.Zero)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyWindow(Handle);
    }

    private void Show()
    {
        SDL.ShowWindow(Handle);
        SDL.RaiseWindow(Handle);
    }
}