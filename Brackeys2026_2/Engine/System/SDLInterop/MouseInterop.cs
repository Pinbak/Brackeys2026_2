using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_ShowCursor();

    internal static bool ShowCursor() => SDL_ShowCursor();

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_HideCursor();

    internal static bool HideCursor() => SDL_HideCursor();
}