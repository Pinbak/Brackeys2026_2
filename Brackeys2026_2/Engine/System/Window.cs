namespace Brackeys2026_2.Engine.System;

internal static unsafe class Window
{
    public static Colour Black = new Colour(50, 50, 50, 255);
    
    private const string Title = "Totally Legitimate Republic";
    internal static SDL.Window* Handle { get; private set; }

    internal static void CreateWindow()
    {
        Handle = SDL.CreateWindow(Title, 1280, 720, SDL.WindowFlags.Hidden);
        Show();
    }

    internal static void SetPresentationLetterbox()
    {
        if (Renderer.Handle is null)
        {
            throw new Exception("Renderer cannot be null when calling SetPresentation.");
        }
        SDL.SetRenderLogicalPresentation(Renderer.Handle, 1920, 1080, SDL.Presentation.Letterbox);
        SDL.SetWindowResizable(Handle, true);
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

    public static void ChangeBackgroundColour(Colour colour)
    {
        Renderer.SetDrawColour(colour);
    }

    public static void SetFramerateLimit(int limit)
    {
        Time.SetFramerateLimit(limit);
    }
}