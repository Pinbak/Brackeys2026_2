using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

public static unsafe partial class SDL
{
    // [LibraryImport(Library)]
    // [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    // [return: MarshalAs(UnmanagedType.I1)]
    // private static partial bool SDL_PointInRect(Point* point, Rectangle* rectangle);
    //
    // internal static bool PointInRect(Point point, Rectangle rectangle) => SDL_PointInRect(&point, &rectangle);
}