using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class Ministers(Texture card, Texture[] faces, Font nameFont) : Component
{
    public List<Minister> CurrentMinisters { get; set; } = [];
    private const int NumberOfMinisters = 11;
    
    private static readonly Random Random = new();
    private Font _nameFont = nameFont;

    protected override void OnInitialise()
    {
        for (var i = 0; i < NumberOfMinisters; i++)
        {
            CurrentMinisters.Add(new Minister((RoleType)i, GetRandomFaceTexture()));
        }

        var width = card.Width;
        var gap = 10f;
        var padding = 10f;
        var rowMax = 3;
        var currentX = 0;
        var currentY = card.Height * .5f;
        for (var i = 0; i < CurrentMinisters.Count; i++)
        {
            if (currentX == rowMax)
            {
                currentX = 0;
                currentY += (int)card.Height + gap;
            }

            var minister = CurrentMinisters[i];
            var ministerGameObject = new GameObject((width + gap) * currentX + padding, padding + currentY, GameObject);
            currentX += 1;

            var card1 = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(card)
            ]);
            
            var face = new GameObject(0, -40, card1, [
                new SpriteRenderer(minister.Face)
            ]); 
            
        }
    }

    private Texture GetRandomFaceTexture()
    {
        return faces[Random.Next(0, faces.Length - 1)];
    }
}