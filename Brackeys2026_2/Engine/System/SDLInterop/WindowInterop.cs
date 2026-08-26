using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [Flags]
    internal enum WindowFlags : ulong
    {
        Fullscreen       = 0x1,
        Hidden           = 0x08,
        Borderless       = 0x10,
        Resizable        = 0x20,
        Minimized        = 0x40,
        InputFocus       = 0x200,
        MouseFocus       = 0x400,
        Maximized        = 0x080,
        HighPixelDensity = 0x2000,
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Window;
    
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_Init(InitFlags flags);

    internal static bool InitSdl(InitFlags flags) => SDL_Init(flags);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_Quit();

    internal static void QuitSdl() => SDL_Quit();

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Window* SDL_CreateWindow(string title, int w, int h, ulong flags);

    internal static Window* CreateWindow(string title, int w, int h, WindowFlags flags)
        => SDL_CreateWindow(title, w, h, (ulong)flags);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(Window* window);

    internal static void DestroyWindow(Window* window) => SDL_DestroyWindow(window);

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowTitle(Window* window, string title);

    internal static bool SetWindowTitle(Window* window, string title)
        => SDL_SetWindowTitle(window, title);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowSize(Window* window, int w, int h);

    internal static bool SetWindowSize(Window* window, int w, int h) => SDL_SetWindowSize(window, w, h);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMinimumSize(Window* window, int w, int h);

    internal static bool SetWindowMinimumSize(Window* window, int w, int h) => SDL_SetWindowMinimumSize(window, w, h);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowMaximumSize(Window* window, int w, int h);

    internal static bool SetWindowMaximumSize(Window* window, int w, int h) => SDL_SetWindowMaximumSize(window, w, h);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowAspectRatio(Window* window, float min, float max);

    internal static bool SetWindowAspectRatio(Window* window, float min, float max) =>
        SDL_SetWindowAspectRatio(window, min, max);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowResizable(Window* window, [MarshalAs(UnmanagedType.I1)] bool resizable);

    internal static bool SetWindowResizable(Window* window, bool resizable) =>
        SDL_SetWindowResizable(window, resizable);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowFullscreen(Window* window, [MarshalAs(UnmanagedType.I1)] bool fullscreen);

    internal static bool SetWindowFullscreen(Window* window, bool fullscreen) =>
        SDL_SetWindowFullscreen(window, fullscreen);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShowWindow(Window* window);

    internal static bool ShowWindow(Window* window) => SDL_ShowWindow(window);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetWindowIcon(Window* window, Surface* surface);

    internal static bool SetWindowIcon(Window* window, Surface* surface) => SDL_SetWindowIcon(window, surface);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RaiseWindow(Window* window);

    internal static bool RaiseWindow(Window* window) => SDL_RaiseWindow(window);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_MaximizeWindow(Window* window);

    internal static bool MaximizeWindow(Window* window) => SDL_MaximizeWindow(window);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_MinimizeWindow(Window* window);

    internal static bool MinimizeWindow(Window* window) => SDL_MinimizeWindow(window);
}