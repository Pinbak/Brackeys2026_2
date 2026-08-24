
namespace Brackeys2026_2.Engine.System;

public sealed unsafe class Window : SystemComponent
{
    public static SDL.Window* Handle { get; private set; }

    internal override void OnInitialise()
    {
        Handle = SDL.CreateWindow("", 800, 600, SDL.WindowFlags.Hidden);
        Show();
    }

    internal override void OnRemove()
    {
        if (Handle == null)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyWindow(Handle);
        Handle = null;
    }

    private void Show()
    {
        SDL.ShowWindow(Handle);
        SDL.RaiseWindow(Handle);
    }
}