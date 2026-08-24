using SDL3;

namespace Brackeys2026_2.Engine.System;

public sealed class Renderer : SystemComponent
{
    public static IntPtr Handle { get; private set; }

    internal override void OnInitialise()
    {
        Handle = SDL.CreateRenderer(Window.Handle, null);
    }

    internal override void OnDrawBegin()
    {
        SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
        SDL.RenderClear(Handle);
    }

    internal override void OnDrawEnd()
    {
        SDL.RenderPresent(Handle);
    }

    internal override void OnRemove()
    {
        if (Handle == IntPtr.Zero)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyRenderer(Handle);
    }
}