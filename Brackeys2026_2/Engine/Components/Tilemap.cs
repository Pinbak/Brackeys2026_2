using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public class Tilemap : Component
{
    private Texture _tileset;
    private int _tileWidth = 100;
    private int _tileHeight = 100;
    private int _mapWidth = 100;
    private int _mapHeight = 100;
    private Vertex[] _vertices;
    private int[] _indices;

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

        var tile = 0;
        float srcX = tile * _tileWidth;
        float srcY = 0;

        var u0 = srcX / _tileset.Width;
        var v0 = srcY / _tileset.Height;
        var u1 = (srcX + _tileWidth) / _tileset.Width;
        var v1 = (srcY + _tileHeight) / _tileset.Height;

        var white = new FColour(1, 1, 1, 1);

        var destX0 = GameObject.Transform.ActualX * _tileWidth;
        var destY0 = GameObject.Transform.ActualY * _tileHeight;
        var destX1 = destX0 + _tileWidth;
        var destY1 = destY0 + _tileHeight;

        
        _vertices =
        [
            // new Vertex(new Point(destX0, destY0), white, new Point(u0, v0)),
            // new Vertex(new Point(destX1, destY0), white, new Point(u1, v0)),
            // new Vertex(new Point(destX0, destY1), white, new Point(u0, v1)),
            // new Vertex(new Point(destX1, destY1), white, new Point(u1, v1)),
            new Vertex(new Point(0, 0), white, new Point(0, 0)),
            new Vertex(new Point(_tileWidth, 0), white, new Point(1, 0)),
            new Vertex(new Point(0, _tileHeight), white, new Point(0, 1)),
            new Vertex(new Point(_tileWidth, _tileHeight), white, new Point(1, 1)),
        ];
        _indices =
        [
            0, 1, 2, 1, 3, 2
        ];
    }
}