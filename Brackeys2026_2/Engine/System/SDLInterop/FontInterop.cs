using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

public static unsafe partial class TTF
{
    private const string Library = "SDL3_ttf";

    [StructLayout(LayoutKind.Sequential)]
    internal struct Font;

    internal enum HorizontalAlignment
    {
        Invalid = -1,
        Left = 0,
        Center = 1,
        Right = 2
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_Init();

    internal static bool Init() => TTF_Init();

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void TTF_Quit();

    internal static void QuitTtf() => TTF_Quit();

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Font* TTF_OpenFontIO(SDL.IoStream* stream, [MarshalAs(UnmanagedType.I1)] bool close,
        float size);

    internal static Font* OpenFontIo(SDL.IoStream* stream, bool close, float size) =>
        TTF_OpenFontIO(stream, close, size);

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial SDL.Surface* TTF_RenderText_Solid_Wrapped(Font* font, string text, UIntPtr size,
        Colour foreground, int wrapWidth);

    internal static SDL.Surface* RenderTextSolidWrapped(Font* font, string text, Colour foreground, int wrapWidth)
        => TTF_RenderText_Solid_Wrapped(font, text, (UIntPtr)text.Length, foreground, wrapWidth);

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial SDL.Surface* TTF_RenderText_Shaded_Wrapped(Font* font, string text, UIntPtr size,
        Colour foreground, Colour background, int wrapWidth);

    internal static SDL.Surface* RenderTextShadedWrapped(Font* font, string text, Colour foreground,
        Colour background, int wrapWidth)
        => TTF_RenderText_Shaded_Wrapped(font, text, (UIntPtr)text.Length, foreground, background, wrapWidth);

    // Render Text Blended Wrapped
    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial SDL.Surface* TTF_RenderText_Blended_Wrapped(Font* font, string text, UIntPtr size,
        Colour color, int wrapWidth);

    internal static SDL.Surface* RenderTextBlendedWrapped(Font* font, string text, Colour color, int wrapWidth)
        => TTF_RenderText_Blended_Wrapped(font, text, (UIntPtr)text.Length, color, wrapWidth);

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_GetStringSizeWrapped(Font* font, string text, UIntPtr size, int wrapWidth,
        out int w, out int h);

    internal static bool GetStringSizeWrapped(Font* font, string text, int wrapWidth, out int w, out int h)
        => TTF_GetStringSizeWrapped(font, text, (UIntPtr)text.Length, wrapWidth, out w, out h);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void TTF_CloseFont(Font* font);

    internal static void CloseFont(Font* font) => TTF_CloseFont(font);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool TTF_SetFontSDF(Font* font, [MarshalAs(UnmanagedType.I1)] bool enabled);

    internal static bool SetFontSdf(Font* font, bool enabled) => TTF_SetFontSDF(font, enabled);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void TTF_SetFontWrapAlignment(Font* font, HorizontalAlignment alignment);

    internal static void SetFontWrapAlignment(Font* font, HorizontalAlignment alignment)
        => TTF_SetFontWrapAlignment(font, alignment);
}