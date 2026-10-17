using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.GameObjects;

public class Batch
{
    private Vertex[] _vertices;
    private int[] _indices;
    private GameObject[] _sprites;
    private readonly Texture _texture;

    public Batch(Texture texture, GameObject[] sprites)
    {
        _texture = texture;
        _sprites = sprites;
    }
    
    public void Blit()
    {
        UpdateVertices();
        // foreach (var sprite in _sprites)
        // {
        //     _texture.Draw(sprite.Transform);
        // }
        _texture.Draw(_vertices, _indices);
    }

    public void GenerateVertices()
    {
        var count = _sprites.Length;
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
            var destX1 = destX0 + _texture.Width;
            var destY1 = destY0 + _texture.Height;

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

    private void UpdateVertices()
    {
        var vertexCount = 0;
        foreach (var gameObject in _sprites)
        {
            var destX0 = gameObject.Transform.ActualX;
            var destY0 = gameObject.Transform.ActualY;
            var destX1 = destX0 + _texture.Width;
            var destY1 = destY0 + _texture.Height;
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
}