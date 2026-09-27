using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer2 : Component
{

    private static List<GameObject> _sprites = [];
    
    private static Texture _spriteTexture;
    private readonly float _red = 1;
    private readonly float _green = 1;
    private readonly float _blue = 1;
    private readonly FColour _colour;
    private static Vertex[] _vertices;
    private static int[] _indices;
    
    public SpriteRenderer2(Texture texture)
    {
        _spriteTexture = texture;
        
    }

    protected override void OnInitialise()
    {
        _sprites.Add(GameObject);
    }

    // public SpriteRenderer2(Texture texture, float red, float green, float blue)
    // {
    //     _spriteTexture = texture;
    //     _red = red;
    //     _green = green;
    //     _blue = blue;
    //     _colour = new FColour(_red, _green, _blue, 1);
    // }

    public static void BatchDraw()
    {
        if (_sprites.Count == 0) return;
        UpdateVertices();
        _spriteTexture.Draw(_vertices, _indices);
    }

    private static void UpdateVertices()
    {
        var vertexCount = 0;
        foreach (var gameObject in _sprites)
        {
            var destX0 = gameObject.Transform.ActualX;
            var destY0 = gameObject.Transform.ActualY;
            var destX1 = destX0 + _spriteTexture.Width;
            var destY1 = destY0 + _spriteTexture.Height;
            _vertices[vertexCount].Position.X = destX0;
            _vertices[vertexCount].Position.Y = destY0;
            _vertices[vertexCount + 1].Position.X = destX1;
            _vertices[vertexCount + 1].Position.Y = destY0;
            _vertices[vertexCount + 2].Position.X = destX0;
            _vertices[vertexCount + 2].Position.Y = destY1;
            _vertices[vertexCount + 3].Position.X = destX1;
            _vertices[vertexCount + 3].Position.Y = destY1;
            vertexCount += 4;
        }
    }

    public static void GenerateVertices()
    {
        var count = _sprites.Count;
        _vertices = new Vertex[4 * count];
        _indices = new int[6 * count];
        var vertexCount = 0;
        var indicesCount = 0;
        
        // source image
        var u0 = 0f;
        var v0 = 0f;
        var u1 = 1f;
        var v1 = 1f;
        
        var white = new FColour(1, 1, 1, 1);
        
        foreach (var gameObject in _sprites)
        {
            // destination image
            var destX0 = gameObject.Transform.ActualX;
            var destY0 = gameObject.Transform.ActualY;
            var destX1 = destX0 + _spriteTexture.Width;
            var destY1 = destY0 + _spriteTexture.Height;

            _vertices[vertexCount] = new Vertex(new Point(destX0, destY0), white, new Point(u0, v0));
            _vertices[vertexCount + 1] = new Vertex(new Point(destX1, destY0), white, new Point(u1, v0));
            _vertices[vertexCount + 2] = new Vertex(new Point(destX0, destY1), white, new Point(u0, v1));
            _vertices[vertexCount + 3] = new Vertex(new Point(destX1, destY1), white, new Point(u1, v1));

            _indices[indicesCount] = vertexCount + 0;
            _indices[indicesCount + 1] = vertexCount + 1;
            _indices[indicesCount + 2] = vertexCount + 2;
            _indices[indicesCount + 3] = vertexCount + 1;
            _indices[indicesCount + 4] = vertexCount + 3;
            _indices[indicesCount + 5] = vertexCount + 2;
            
            vertexCount += 4;
            indicesCount += 6;
        }
    }
}