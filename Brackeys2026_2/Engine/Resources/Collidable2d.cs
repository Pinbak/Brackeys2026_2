using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public unsafe class Collidable2D : Resource
{
    public float Width { get; protected set; }
    public float Height { get; protected set; }
    internal Rectangle Dimensions { get; set; }

    public bool IsPointInside(Point point)
    {
        return point.X >= Dimensions.X &&
               point.X < Dimensions.X + Dimensions.Width &&
               point.Y >= Dimensions.Y &&
               point.Y < Dimensions.Y + Dimensions.Height;
    }
}