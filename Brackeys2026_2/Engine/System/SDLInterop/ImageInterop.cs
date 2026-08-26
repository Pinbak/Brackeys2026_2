using System.Runtime.InteropServices;

public static unsafe partial class IMG
{
    private const string Library = "SDL3_image";
    
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial SDL.Surface* IMG_Load_IO(SDL.IoStream* stream, [MarshalAs(UnmanagedType.I1)] bool close);
    internal static SDL.Surface* LoadIo(SDL.IoStream* stream, bool close) => IMG_Load_IO(stream, close);
}
