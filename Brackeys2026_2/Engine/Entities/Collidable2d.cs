using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public class Collidable2D : Resource
{
    public float Width { get; protected set; }
    public float Height { get; protected set; }
    internal Rectangle Dimensions { get; set; }
}