using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct IoStream; // opaque handle
    
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial IoStream* SDL_IOFromMem(void* mem, nuint size);

    internal static IoStream* OpenIo(void* memory, nuint size) => SDL_IOFromMem(memory, size);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_CloseIO(IoStream* stream);

    internal static bool CloseIo(IoStream* stream) => SDL_CloseIO(stream);
}