using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class Ministers(Texture card, Texture[] faces, Font nameFont, Font smallerFont) : Component
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

        GenerateMinisterGameObjects();
        GeneratePresidentGameObject();
    }

    private void GeneratePresidentGameObject()
    {
        
    }

    private void GenerateMinisterGameObjects()
    {
        // have to hard-code as no ui-system
        var width = card.Width;
        var gap = 10;
        var padding = 10f;
        var rowMax = 2;
        var startingX = (int)width * .5f + 30;
        var currentX = startingX;
        var currentY = card.Height * .5f + 5;
        var c = 0;
        foreach (var minister in CurrentMinisters)
        {
            if (minister.RoleType is RoleType.President) continue;
            if (c == rowMax)
            {
                c = 0;
                currentX = startingX;
                currentY += (int)card.Height + gap;
            }

            var ministerGameObject = new GameObject(currentX + padding, currentY + padding, GameObject);
            currentX += (int)width + gap;
            c++;
            
            var cardGameObject = new GameObject(0, 0, ministerGameObject, [
                new SpriteRenderer(card),
            ]);
            var collider = new Collider(cardGameObject.Transform, card.Width, card.Height);
            var cardComponent = new Card(collider);
            cardGameObject.AddComponent(collider);
            cardGameObject.AddComponent(cardComponent);
            
            var face = new GameObject(-96, 0, cardGameObject, [
                new SpriteRenderer(minister.Face)
            ]);

            var nameText = new GameObject(50, -30, cardGameObject, [
                new TextRenderer(smallerFont, minister.Name, 200)
            ]);
            var roleText = new GameObject(60, 10, cardGameObject, [
                new TextRenderer(smallerFont, minister.GetHumanReadableRole(), 200)
            ]);
        }
    }

    private Texture GetRandomFaceTexture()
    {
        return faces[Random.Next(0, faces.Length)];
    }
}