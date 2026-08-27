using System.Runtime.InteropServices;
using Brackeys2026_2.Engine.GameObjects;

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

    internal static void CreateGraphics()
    {
        Handle = SDL.CreateRenderer(WindowManager.Handle, null);
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

    private static void SetDrawColour(Colour colour)
    {
        SDL.SetRenderDrawColor(Handle, colour.Red, colour.Green, colour.Blue, colour.Alpha);
    }

    private static void SetDrawColourWhite()
    {
        SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
    }

    internal static void DrawBegin()
    {
        SetDrawColourWhite();
        SDL.RenderClear(Handle);
    }
    
    internal static void DrawBegin(Colour colour)
    {
        SetDrawColour(colour);
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

        // if (drawDebug)
        // {
        //     SDL.SetRenderDrawColor(Handle, 0, 255, 0, 255);
        //     SDL.RenderRect(Handle, destination);
        //     SDL.SetRenderDrawColor(Handle, 255, 255, 255, 255);
        // }
        SDL.RenderTextureRotated(Handle, texture, frame, destination, position.ActualAngle, null, SDL.FlipMode.None);
    }
    
}