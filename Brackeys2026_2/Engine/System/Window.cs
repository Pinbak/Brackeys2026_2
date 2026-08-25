
namespace Brackeys2026_2.Engine.System;

public sealed unsafe class Window : SystemComponent
{
    private const string Title = "Hello world";
    public static SDL.Window* Handle { get; private set; }

    internal override void OnInitialise()
    {
        Handle = SDL.CreateWindow(Title, 1280, 720, SDL.WindowFlags.Hidden);
        Show();
    }

    internal override void OnLateInitialise()
    {
        SDL.SetRenderLogicalPresentation(Renderer.Handle, 1280, 720, SDL.Presentation.Letterbox);
        // SDL.SetWindowResizable(Handle, true);
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

    private static void Show()
    {
        SDL.ShowWindow(Handle);
        SDL.RaiseWindow(Handle);
    }
}