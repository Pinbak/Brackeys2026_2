using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    private const string Library = "SDL3";
    
    internal enum AppResult
    {
        Continue,
        Success,
        Failed,
    }
    
    [Flags]
    internal enum InitFlags : uint
    {
        Timer = 0x1,
        Audio = 0x10,
        Video = 0x20,
        Joystick = 0x200,
        Haptic = 0x1000,
        Gamepad = 0x2000,
    }

    #region Enumerators
    
    internal enum ScaleMode
    {
        Nearest,
        Linear,
        Pixel,
    }
    
    internal enum PropertyType
    {
        Invalid,
        Pointer,
        String,
        Number,
        Float,
        Boolean,
    }
    
    internal enum PixelType
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        Arrayu8,
        Arrayu16,
        Arrayu32,
        Arrayf16,
        Arrayf32,
        Index2,
    }
    
    [Flags]
    internal enum Presentation
    {
        Disabled,
        Stretch,
        Letterbox,
        Overscan,
        IntegerScaled,
    }

    internal enum PixelFormat : uint
    {
        Unknown = 0,
        Index1Lsb = 286261504,
        Index1Msb = 287310080,
        Index2Lsb = 470811136,
        Index2Msb = 471859712,
        Index4Lsb = 303039488,
        Index4Msb = 304088064,
        Index8 = 318769153,
        Rgb332 = 336660481,
        Xrgb4444 = 353504258,
        Xbgr4444 = 357698562,
        Xrgb1555 = 353570562,
        Xbgr1555 = 357764866,
        Argb4444 = 355602434,
        Rgba4444 = 356651010,
        Abgr4444 = 359796738,
        Bgra4444 = 360845314,
        Argb1555 = 355667970,
        Rgba5551 = 356782082,
        Abgr1555 = 359862274,
        Bgra5551 = 360976386,
        Rgb565 = 353701890,
        Bgr565 = 357896194,
        Rgb24 = 386930691,
        Bgr24 = 390076419,
        Xrgb8888 = 370546692,
        Rgbx8888 = 371595268,
        Xbgr8888 = 374740996,
        Bgrx8888 = 375789572,
        Argb8888 = 372645892,
        Rgba8888 = 373694468,
        Abgr8888 = 376840196,
        Bgra8888 = 377888772,
        Xrgb2101010 = 370614276,
        Xbgr2101010 = 374808580,
        Argb2101010 = 372711428,
        Abgr2101010 = 376905732,
        Rgb48 = 403714054,
        Bgr48 = 406859782,
        Rgba64 = 404766728,
        Argb64 = 405815304,
        Bgra64 = 407912456,
        Abgr64 = 408961032,
        Rgb48Float = 437268486,
        Bgr48Float = 440414214,
        Rgba64Float = 438321160,
        Argb64Float = 439369736,
        Bgra64Float = 441466888,
        Abgr64Float = 442515464,
        Rgb96Float = 454057996,
        Bgr96Float = 457203724,
        Rgba128Float = 455114768,
        Argb128Float = 456163344,
        Bgra128Float = 458260496,
        Abgr128Float = 459309072,
        Yv12 = 842094169,
        Iyuv = 1448433993,
        Yuy2 = 844715353,
        Uyvy = 1498831189,
        Yvyu = 1431918169,
        Nv12 = 842094158,
        Nv21 = 825382478,
        P010 = 808530000,
        ExternalOes = 542328143,
        Rgba32 = 376840196,
        Argb32 = 377888772,
        Bgra32 = 372645892,
        Abgr32 = 373694468,
        Rgbx32 = 374740996,
        Xrgb32 = 375789572,
        Bgrx32 = 370546692,
        Xbgr32 = 371595268,
    }
    
    internal enum PackedOrder
    {
        None = 0,
        Xrgb = 1,
        Rgbx = 2,
        Argb = 3,
        Rgba = 4,
        Xbgr = 5,
        Bgrx = 6,
        Abgr = 7,
        Bgra = 8,
    }
    
    internal enum PackedLayout
    {
        None,
        Layout332,
        Layout4444,
        Layout1555,
        Layout5551,
        Layout565,
        Layout8888,
        Layout2101010,
        Layout1010102,
    }
    
    internal enum Orientation
    {
        Unknown,
        Landscape,
        LandscapeFlipped,
        Portrait,
        PortraitFlipped
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct PathInfo
    {
        internal PathType Type;
        internal ulong Size;
        internal long Created;
        internal long Modified;
        internal long Accessed;
    }
    
    internal enum PathType
    {
        None,
        File,
        Folder,
        Other
    }
    
    #endregion

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial int SDL_EnterAppMainCallbacks(int argc, IntPtr argv,
        delegate* unmanaged[Cdecl]<IntPtr, int, IntPtr, AppResult> init,
        delegate* unmanaged[Cdecl]<IntPtr, AppResult> iterate,
        delegate* unmanaged[Cdecl]<IntPtr, Event*, AppResult> events,
        delegate* unmanaged[Cdecl]<IntPtr, AppResult, void> quit);

    internal static int EnterAppMainCallbacks(int argc, IntPtr argv,
        delegate* unmanaged[Cdecl]<IntPtr, int, IntPtr, AppResult> init,
        delegate* unmanaged[Cdecl]<IntPtr, AppResult> iterate,
        delegate* unmanaged[Cdecl]<IntPtr, Event*, AppResult> events,
        delegate* unmanaged[Cdecl]<IntPtr, AppResult, void> quit) =>
        SDL_EnterAppMainCallbacks(argc, argv, init, iterate, events, quit);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial int SDL_RunApp(int argc, IntPtr argv, delegate* unmanaged[Cdecl]<int, IntPtr, int> main,
        IntPtr reserved);

    internal static int RunApp(int argc, IntPtr argv, delegate* unmanaged[Cdecl]<int, IntPtr, int> main,
        IntPtr reserved) => SDL_RunApp(argc, argv, main, reserved);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_free(IntPtr memory);

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string SDL_GetPlatform();

    internal static string GetPlatform() => SDL_GetPlatform();

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_Log(string message);

    internal static void Log(string message) => SDL_Log(message);

    #region Error Handling

    // Get Error
    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial string SDL_GetError();

    internal static string GetError() => SDL_GetError();

    #endregion
}