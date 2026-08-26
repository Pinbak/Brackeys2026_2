using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.System;

public static unsafe partial class SDL
{
    #region Texturing
    
    internal enum TextureAccess
    {
        Static,
        Streaming,
        Target,
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct Texture
    {
        internal PixelFormat Format;
        internal int Width;
        internal int Height;
        private int RefCount;
    }
    
    internal enum FlipMode
    {
        None,
        Horizontal,
        Vertical,
        HorizontalVertical = (Horizontal | Vertical)
    }
    
    internal enum BlendMode : uint
    {
        None = 0x00000000,
        AlphaBlend = 0x00000001,
        AlphaBlendPremultiplied = 0x00000010,
        Additive = 0x00000002,
        AdditivePremultiplied = 0x00000020,
        ColorModulate = 0x00000004,
        ColorMultiply = 0x00000008,
        Invalid = 0x7FFFFFFF
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Texture* SDL_CreateTexture(Renderer* renderer, PixelFormat format, TextureAccess access,
        int w, int h);

    internal static Texture*
        CreateTexture(Renderer* renderer, PixelFormat format, TextureAccess access, int w, int h) =>
        SDL_CreateTexture(renderer, format, access, w, h);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Texture* SDL_CreateTextureFromSurface(Renderer* renderer, Surface* surface);

    internal static Texture* CreateTextureFromSurface(Renderer* renderer, Surface* surface) =>
        SDL_CreateTextureFromSurface(renderer, surface);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(Texture* texture);

    internal static void DestroyTexture(Texture* texture) => SDL_DestroyTexture(texture);

    // default is linear
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetDefaultTextureScaleMode(Renderer* renderer, ScaleMode mode);

    internal static bool SetDefaultTextureScaleMode(Renderer* renderer, ScaleMode mode) =>
        SDL_SetDefaultTextureScaleMode(renderer, mode);

    // default is linear
    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextureScaleMode(Texture* texture, ScaleMode mode);

    internal static bool SetTextureScaleMode(Texture* texture, ScaleMode mode) =>
        SDL_SetTextureScaleMode(texture, mode);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextureColorMod(Texture* texture, byte r, byte g, byte b);

    internal static bool SetTextureColorMod(Texture* texture, byte r, byte g, byte b) =>
        SDL_SetTextureColorMod(texture, r, g, b);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextureAlphaMod(Texture* texture, byte alpha);

    internal static bool SetTextureAlphaMod(Texture* texture, byte alpha) => SDL_SetTextureAlphaMod(texture, alpha);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetTextureBlendMode(Texture* texture, BlendMode mode);

    internal static bool SetTextureBlendMode(Texture* texture, BlendMode mode) =>
        SDL_SetTextureBlendMode(texture, mode);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTexture(Renderer* renderer, Texture* texture, Rectangle* src, Rectangle* dst);

    internal static bool RenderTexture(Renderer* renderer, Texture* texture, Rectangle? source, Rectangle? destination)
    {
        var sourceValue = source.GetValueOrDefault();
        var destinationValue = destination.GetValueOrDefault();
        return SDL_RenderTexture(renderer, texture,
            source is null ? null : &sourceValue,
            destination is null ? null : &destinationValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTextureRotated(Renderer* renderer, Texture* texture, Rectangle* src, Rectangle* dst,
        double angle, Point* center, FlipMode flip);

    internal static bool RenderTextureRotated(Renderer* renderer, Texture* texture, Rectangle? source, Rectangle? destination, double angle,
        Point? centre, FlipMode flip)
    {
        var sourceValue = source.GetValueOrDefault();
        var destinationValue = destination.GetValueOrDefault();
        var centreValue = centre.GetValueOrDefault();
        return SDL_RenderTextureRotated(renderer, texture,
            source is null ? null : &sourceValue,
            destination is null ? null : &destinationValue,
            angle,
            centre is null ? null : &centreValue,
            flip);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTextureAffine(Renderer* renderer, Texture* texture, Rectangle* src, Point* origin,
        Point* right, Point* down);

    internal static bool RenderTextureAffine(Renderer* renderer, Texture* texture, Rectangle? source, Point? origin,
        Point? right, Point? down)
    {
        var sourceValue = source.GetValueOrDefault();
        var originValue = origin.GetValueOrDefault();
        var rightValue = right.GetValueOrDefault();
        var downValue = down.GetValueOrDefault();
        return SDL_RenderTextureAffine(renderer, texture,
            source is null ? null : &sourceValue,
            origin is null ? null : &originValue,
            right is null ? null : &rightValue,
            down is null ? null : &downValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTextureTiled(Renderer* renderer, Texture* texture, Rectangle* src, float scale,
        Rectangle* dst);

    internal static bool RenderTextureTiled(Renderer* renderer, Texture* texture, Rectangle? source, float scale, Rectangle? destination)
    {
        var sourceValue = source.GetValueOrDefault();
        var destinationValue = destination.GetValueOrDefault();
        return SDL_RenderTextureTiled(renderer, texture, 
            source is null ? null : &sourceValue,
            scale,
            destination is null ? null : &destinationValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTexture9Grid(Renderer* renderer, Texture* texture, Rectangle* src, float leftWidth,
        float rightWidth, float topHeight, float bottomHeight, float scale, Rectangle* dst);

    internal static bool RenderTexture9Grid(Renderer* renderer, Texture* texture, Rectangle? source, float leftWidth,
        float rightWidth, float topHeight, float bottomHeight, float scale, Rectangle? destination)
    {
        var sourceValue = source.GetValueOrDefault();
        var destinationValue = destination.GetValueOrDefault();
        return SDL_RenderTexture9Grid(renderer, texture,
            source is null ? null : &sourceValue,
            leftWidth, rightWidth,
            topHeight, bottomHeight, scale,
            destination is null ? null : &destinationValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderTexture9GridTiled(Renderer* renderer, Texture* texture, Rectangle* src,
        float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, Rectangle* dst,
        float tileScale);

    internal static bool RenderTexture9GridTiled(Renderer* renderer, Texture* texture, Rectangle? source, float leftWidth,
        float rightWidth, float topHeight, float bottomHeight, float scale, Rectangle? destination, float tileScale)
    {
        var sourceValue = source.GetValueOrDefault();
        var destinationValue = destination.GetValueOrDefault();
        return SDL_RenderTexture9GridTiled(renderer, texture,
            source is null ? null : &sourceValue,
            leftWidth, rightWidth, topHeight, bottomHeight, scale,
            destination is null ? null : &destinationValue,
            tileScale);
    }

    #endregion

    #region Rendering
    
    [StructLayout(LayoutKind.Sequential)]
    public struct Renderer;

    [LibraryImport(Library, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Renderer* SDL_CreateRenderer(Window* window, string? name);

    internal static Renderer* CreateRenderer(Window* window, string? name) => SDL_CreateRenderer(window, name);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRenderDrawColor(Renderer* renderer, byte r, byte g, byte b, byte a);

    internal static bool SetRenderDrawColor(Renderer* renderer, byte r, byte g, byte b, byte a) =>
        SDL_SetRenderDrawColor(renderer, r, g, b, a);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderPresent(Renderer* renderer);

    internal static bool RenderPresent(Renderer* renderer) => SDL_RenderPresent(renderer);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(Renderer* renderer);

    internal static void DestroyRenderer(Renderer* renderer) => SDL_DestroyRenderer(renderer);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRenderLogicalPresentation(Renderer* renderer, int w, int h,
        Presentation presentation);

    internal static bool SetRenderLogicalPresentation(Renderer* renderer, int w, int h, Presentation presentation) =>
        SDL_SetRenderLogicalPresentation(renderer, w, h, presentation);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRenderScale(Renderer* renderer, float x, float y);

    internal static bool SetRenderScale(Renderer* renderer, float x, float y) => SDL_SetRenderScale(renderer, x, y);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRenderVSync(Renderer* renderer, int vsync);

    internal static bool SetRenderVSync(Renderer* renderer, int vsync) => SDL_SetRenderVSync(renderer, vsync);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_SetRenderDrawBlendMode(Renderer* renderer, BlendMode mode);

    internal static bool SetRenderDrawBlendMode(Renderer* renderer, BlendMode mode) =>
        SDL_SetRenderDrawBlendMode(renderer, mode);

    # region Drawing Methods

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderPoint(Renderer* renderer, float x, float y);

    internal static bool RenderPoint(Renderer* renderer, float x, float y) => SDL_RenderPoint(renderer, x, y);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderPoints(Renderer* renderer, Point[] points, int count);

    internal static bool RenderPoints(Renderer* renderer, Point[] points, int count) =>
        SDL_RenderPoints(renderer, points, count);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderLine(Renderer* renderer, float x1, float y1, float x2, float y2);

    internal static bool RenderLine(Renderer* renderer, float x1, float y1, float x2, float y2) =>
        SDL_RenderLine(renderer, x1, y1, x2, y2);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderLines(Renderer* renderer, Point[] points, int count);

    internal static bool RenderLines(Renderer* renderer, Point[] points, int count) =>
        SDL_RenderLines(renderer, points, count);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderRect(Renderer* renderer, Rectangle* rect);

    internal static bool RenderRect(Renderer* renderer, Rectangle? rectangle)
    {
        var rectangleValue = rectangle.GetValueOrDefault();
        return SDL_RenderRect(renderer, rectangle is null ? null : &rectangleValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderRects(Renderer* renderer, Rectangle[] rects, int count);

    internal static bool RenderRects(Renderer* renderer, Rectangle[] rects, int count) =>
        SDL_RenderRects(renderer, rects, count);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderFillRect(Renderer* renderer, Rectangle* rect);

    internal static bool RenderFillRect(Renderer* renderer, Rectangle? rectangle)
    {
        var rectangleValue = rectangle.GetValueOrDefault();
        return SDL_RenderFillRect(renderer, rectangle is null ? null : &rectangleValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderFillRects(Renderer* renderer, Rectangle[] rects, int count);

    internal static bool RenderFillRects(Renderer* renderer, Rectangle[] rects, int count) =>
        SDL_RenderFillRects(renderer, rects, count);

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static partial Surface* SDL_RenderReadPixels(Renderer* renderer, RectangleInt* rect);

    internal static Surface* RenderReadPixels(Renderer* renderer, RectangleInt? rectangle)
    {
        var rectangleValue = rectangle.GetValueOrDefault();
        return SDL_RenderReadPixels(renderer, rectangle is null ? null : &rectangleValue);
    }

    [LibraryImport(Library)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RenderClear(Renderer* renderer);

    internal static bool RenderClear(Renderer* renderer) => SDL_RenderClear(renderer);

    #endregion

    #endregion
}