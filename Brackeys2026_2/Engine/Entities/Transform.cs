namespace Brackeys2026_2.Engine.Entities;

public class Transform
{
    public float X { get; init; }
    public float Y { get; init; }
    public float Angle { get; set; }
    public float ScaleX { get; set; } = 1.0f;
    public float ScaleY { get; set; } = 1.0f;
    public float Scale { get; set; } = 1.0f;
}