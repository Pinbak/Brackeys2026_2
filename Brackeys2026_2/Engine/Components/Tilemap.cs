using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public class Tilemap : Component
{
    private Texture _tileset;
    private int _tileWidth = 100;
    private int _tileHeight = 100;
    private int _mapWidth = 3;
    private int _mapHeight = 4;
    private Vertex[] _vertices;
    private int[] _indices;
    
    private Random _random = new();

    public Tilemap(Texture tileset)
    {
        _tileset = tileset;
    }

    protected override void OnInitialise()
    {
        GenerateVertices();
    }

    protected override void OnDraw()
    {
        _tileset.Draw(_vertices, _indices);
    }

    private void GenerateVertices()
    {
        var totalTiles = _mapWidth * _mapHeight;
        var vertexCount = 0;
        var indicesCount = 0;
        _vertices = new Vertex[4 * totalTiles];
        _indices = new int[6 * totalTiles];

        var white = new FColour(1, 1, 1, 1);
        
        for (var y = 0; y < _mapHeight; y++)
        for (var x = 0; x < _mapWidth; x++)
        {
            // source image
            var tile = _random.Next(0, 2);
            float srcX = 0;
            float srcY = tile * _tileHeight;

            var u0 = srcX / _tileset.Width;
            var v0 = srcY / _tileset.Height;
            var u1 = (srcX + _tileWidth) / _tileset.Width;
            var v1 = (srcY + _tileHeight) / _tileset.Height;
        
            // add a destination to the vertex array
            var destX0 = x * _tileWidth + GameObject.Transform.ActualX;
            var destY0 = y * _tileHeight + GameObject.Transform.ActualY;
            var destX1 = destX0 + _tileWidth;
            var destY1 = destY0 + _tileHeight;

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