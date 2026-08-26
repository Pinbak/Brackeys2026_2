using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [Flags]
    internal enum SurfaceFlags : uint
    {
        Preallocated =  0x00000001u /* Surface uses preallocated pixel memory */,
        LockNeeded   =  0x00000002u /* Surface needs to be locked to access pixels */,
        Locked       =  0x00000004u /* Surface is currently locked */,
        SimdAligned  =  0x00000008u/* Surface uses pixel memory allocated with SDL_aligned_alloc() */
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct Surface
    {
        internal SurfaceFlags flags;
        internal PixelFormat format;
        internal int width;
        internal int height;
        internal int pitch;
        internal IntPtr pixels;
        private int refcount;
        private IntPtr reserved;
    }
    
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Surface* SDL_CreateSurface(int w, int h, PixelFormat format);

    internal static Surface* CreateSurface(int w, int h, PixelFormat format) => SDL_CreateSurface(w, h, format);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_DestroySurface(Surface* surface);

    internal static void DestroySurface(Surface* surface) => SDL_DestroySurface(surface);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Surface* SDL_ConvertSurface(Surface* surface, PixelFormat format);

    internal static Surface* ConvertSurface(Surface* surface, PixelFormat format) =>
        SDL_ConvertSurface(surface, format);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Surface* SDL_RotateSurface(Surface* surface, float angle);

    internal static Surface* RotateSurface(Surface* surface, float angle) => SDL_RotateSurface(surface, angle);
}