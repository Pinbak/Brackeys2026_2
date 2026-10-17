using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer3 : Component
{
    private Texture _spriteTexture;
    private readonly float _red = 1;
    private readonly float _green = 1;
    private readonly float _blue = 1;
    private readonly FColour _colour;
    private static Vertex[] _vertices;
    private static int[] _indices;
    private float _width;
    private float _height;
    
    public SpriteRenderer3(Texture texture)
    {
        _spriteTexture = texture;
        _width = _spriteTexture.Width;
        _height = _spriteTexture.Height;
    }

    protected override void OnInitialise()
    {
        GenerateVertices();
    }

    protected override void OnDraw()
    {
        UpdateVertices();
        _spriteTexture.Draw(_vertices, _indices);
    }

    private void UpdateVertices()
    {
        var destX0 = GameObject.Transform.ActualX;
        var destY0 = GameObject.Transform.ActualY;
        var destX1 = destX0 + _width;
        var destY1 = destY0 + _height;
        _vertices[0].Position.X = destX0;
        _vertices[0].Position.Y = destY0;
        _vertices[1].Position.X = destX1;
        _vertices[1].Position.Y = destY0;
        _vertices[2].Position.X = destX0;
        _vertices[2].Position.Y = destY1;
        _vertices[3].Position.X = destX1;
        _vertices[3].Position.Y = destY1;
    }

    private void GenerateVertices()
    {
        _vertices = new Vertex[4];
        _indices = new int[6];
        
        // source image
        var u0 = 0f;
        var v0 = 0f;
        var u1 = 1f;
        var v1 = 1f;
        
        var white = new FColour(1, 1, 1, 1);
        
        // destination image
        var destX0 = GameObject.Transform.ActualX;
        var destY0 = GameObject.Transform.ActualY;
        var destX1 = destX0 + _spriteTexture.Width;
        var destY1 = destY0 + _spriteTexture.Height;

        _vertices[0] = new Vertex(new Point(destX0, destY0), white, new Point(u0, v0));
        _vertices[1] = new Vertex(new Point(destX1, destY0), white, new Point(u1, v0));
        _vertices[2] = new Vertex(new Point(destX0, destY1), white, new Point(u0, v1));
        _vertices[3] = new Vertex(new Point(destX1, destY1), white, new Point(u1, v1));

        _indices[0] = 0;
        _indices[1] = 1;
        _indices[2] = 2;
        _indices[3] = 1;
        _indices[4] = 3;
        _indices[5] = 2;
    }
}