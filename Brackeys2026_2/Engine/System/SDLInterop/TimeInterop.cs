using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial ulong SDL_GetPerformanceCounter();

    internal static long GetPerformanceCounter() => (long)SDL_GetPerformanceCounter();

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial ulong SDL_GetPerformanceFrequency();

    internal static long GetPerformanceFrequency() => (long)SDL_GetPerformanceFrequency();

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_DelayPrecise(ulong ns);

    internal static void DelayPrecise(ulong ns) => SDL_DelayPrecise(ns);
    
}