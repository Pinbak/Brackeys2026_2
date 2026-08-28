using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.System;

[StructLayout(LayoutKind.Sequential)]
public struct Rectangle(float x, float y, float width, float height)
{
    public float X = x;
    public float Y = y;
    public float Width = width;
    public float Height = height;
}

[StructLayout(LayoutKind.Sequential)]
public struct RectangleInt(int x, int y, int width, int height)
{
    public int X = x;
    public int Y = y;
    public int Width = width;
    public int Height = height;
}

[StructLayout(LayoutKind.Sequential)]
public struct Colour(byte red, byte green, byte blue, byte alpha)
{
    public byte Red = red;
    public byte Green = green;
    public byte Blue = blue;
    public byte Alpha = alpha;
}

[StructLayout(LayoutKind.Sequential)]
public struct Point(float x, float y)
{
    public float X = x;
    public float Y = y;
}

internal static unsafe class Renderer
{
    internal static SDL.Renderer* Handle { get; private set; }
    private static Colour _renderColour;

    internal static void Setup()
    {
        _renderColour = new Colour(255, 255, 255, 255);
        Handle = SDL.CreateRenderer(Window.Handle, null);
        if (Handle == null)
        {
            throw new Exception(SDL.GetError());
        }
    }

    internal static void CleanUpResources()
    {
        if (Handle == null)
        {
            throw new Exception("Cannot destroy a window that doesn't exist.");
        }
        SDL.DestroyRenderer(Handle);
        Handle = null;
    }

    internal static void SetDrawColour(Colour colour)
    {
        _renderColour = colour;
    }
    
    internal static void DrawBegin()
    {
        SDL.SetRenderDrawColor(Handle, _renderColour.Red, _renderColour.Green, _renderColour.Blue, _renderColour.Alpha);
        SDL.RenderClear(Handle);
    }

    internal static void DrawEnd()
    {
        SDL.RenderPresent(Handle);
    }
    
    internal static void Draw(SDL.Texture* texture, Transform position, float width, float height, Rectangle? frame)
    {
        var pivotX = width * position.Pivot.x * position.Scale;
        var pivotY = height * position.Pivot.y * position.Scale;
        var destination = new Rectangle(position.ActualX - pivotX, position.ActualY - pivotY, width * position.ActualScale, height * position.ActualScale);

        SDL.RenderTextureRotated(Handle, texture, frame, destination, position.ActualAngle, null, SDL.FlipMode.None);
#if DEBUG
        DrawBoundingBox(destination);
#endif
    }
    
    internal static void Draw(SDL.Texture* texture, Rectangle destination, float scale)
    {
        SDL.RenderTextureTiled(Handle, texture, null, scale, destination);
#if DEBUG
        DrawBoundingBox(destination);
#endif
    }
    
    internal static void Draw(SDL.Texture* texture, Rectangle destination, float left, float right, float top, float bottom, float scale)
    {
        SDL.RenderTexture9GridTiled(Handle, texture, null, left, right, top, bottom, 0f, destination, scale);
#if DEBUG
        DrawBoundingBox(destination);
#endif
    }

    private static void DrawBoundingBox(Rectangle destination)
    {
        SDL.SetRenderDrawColor(Handle, 0, 255, 0, 255);
        SDL.RenderRect(Handle, destination);
        SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
    }

    public static void DrawMousePosition()
    {
        SDL.SetRenderDrawColor(Handle, 0, 255, 0, 255);
        SDL.RenderRect(Handle, new Rectangle(Mouse.X, Mouse.Y, 10, 10));
        SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
    }
}