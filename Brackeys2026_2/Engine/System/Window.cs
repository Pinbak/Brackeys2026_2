namespace Brackeys2026_2.Engine.System;

public static unsafe class Window
{
    private const string Title = "Hello world";
    internal static SDL.Window* Handle { get; private set; }

    internal static void CreateWindow()
    {
        Handle = SDL.CreateWindow(Title, 1280, 720, SDL.WindowFlags.Hidden);
        Show();
    }

    internal static void SetPresentationLetterbox()
    {
        if (Graphics.Handle is null)
        {
            throw new Exception("Renderer cannot be null when calling SetPresentation.");
        }
        SDL.SetRenderLogicalPresentation(Graphics.Handle, 1280, 720, SDL.Presentation.Letterbox);
        // SDL.SetWindowResizable(Handle, true);
    }

    internal static void CleanUpResources()
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