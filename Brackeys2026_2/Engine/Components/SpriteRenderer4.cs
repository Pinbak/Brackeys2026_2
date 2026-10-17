using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Components;

public class SpriteRenderer4 : Component
{
    private static readonly List<SpriteRenderer4> Sprites = [];
    private static readonly List<Batch> Batches = [];
    
    private Texture _spriteTexture;
    private readonly float _red = 1;
    private readonly float _green = 1;
    private readonly float _blue = 1;
    private readonly FColour _colour;
    private static Vertex[] _vertices;
    private static int[] _indices;
    private float _width;
    private float _height;
    
    public SpriteRenderer4(Texture texture)
    {
        _spriteTexture = texture;
        _width = _spriteTexture.Width;
        _height = _spriteTexture.Height;
    }

    protected override void OnInitialise()
    {
        Sprites.Add(this);
    }

    public static void DrawBatches()
    {
        foreach (var batch in Batches)
        {
            batch.Blit(); // a single draw call
        }
    }

    public static void GenerateVertices()
    {
        Batches.Clear();
        var lastTexture = Sprites[0]._spriteTexture;
        var textureGroup = new List<GameObject>();
        foreach (var sprite in Sprites)
        {
            if (lastTexture != sprite._spriteTexture)
            {
                CreateBatch(lastTexture, textureGroup);
                textureGroup.Clear();
                lastTexture = sprite._spriteTexture;
            }

            textureGroup.Add(sprite.GameObject);
        }

        CreateBatch(lastTexture, textureGroup);
    }

    private static void CreateBatch(Texture lastTexture, List<GameObject> textureGroup)
    {
        var batch = new Batch(lastTexture, textureGroup.ToArray());
        batch.GenerateVertices();
        Batches.Add(batch);
    }
}