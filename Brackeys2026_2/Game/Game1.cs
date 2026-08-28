using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.Components.Experiments;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    protected override void Layout()
    {
        Window.ChangeBackgroundColour(new Colour(242, 218, 175, 255));
        var folderTexture = new Texture("Game/Assets/folderDarker.png");
        var folderLighterTexture = new Texture("Game/Assets/folderLighter.png");
        var oldFont = new Font("Game/Assets/IBMPlexSerif-Light.ttf");
        var oldFontSmaller = new Font("Game/Assets/IBMPlexSerif-Light.ttf", 20);
        var boldFont = new Font("Game/Assets/RubikMonoOne-Regular.ttf", 20);
        boldFont.SetAlignmentCentre();
        var boldFontSmaller = new Font("Game/Assets/RubikMonoOne-Regular.ttf", 18);
        var misc1 = new Texture("Game/Assets/arrow.png");
        var misc2 = new Texture("Game/Assets/sideThing.png");
        var divider = new Texture("Game/Assets/divider.png");
        var horizontalDivider = new Texture("Game/Assets/horizontalDivider.png");

        const int leftFolderHeight = 520;
        var black = new Colour(50, 50, 50, 255);
        var leftFolder = new GameObject(-20, 100, null, [
            new Tiled9SliceSpriteRenderer(folderTexture, 900, leftFolderHeight, 20)
        ]);
        var text = new GameObject(700, 80, null, [
            new TextRenderer(oldFont, black, "Cabinet", 150)
        ]);
        var arrow = new GameObject(780, 80, null, [
            new SpriteRenderer(misc1)
        ]);
        text.Transform.Angle = -5;
        
        // dividers
        _ = new GameObject(300, 100, null, [
            new TiledSpriteRenderer(divider, 11, leftFolderHeight)
        ]);
        _ = new GameObject(330, 100, null, [
            new TiledSpriteRenderer(divider, 11, leftFolderHeight)
        ]); 
        
        // right folder
        const int rightFolderWidth = 320;
        const int rightFolderHeight = 720;
        var rightFolder = new GameObject(980, 0, null, [
            new Tiled9SliceSpriteRenderer(folderTexture, rightFolderWidth, rightFolderHeight, 20),
        ]);
            _ = new GameObject(-10, 80, rightFolder, [
                new SpriteRenderer(misc2),
                new TextRenderer(oldFont, black, "You", 150),
            ])
            {
                Transform =
                {
                    Angle = -90
                }
            };
            var playerDescriptionText = new TextRenderer(boldFont, black, "Insert name here", 300);
            _ = new GameObject(rightFolderWidth * .5f, 40, rightFolder, [
                playerDescriptionText
            ]);
            var jobDescriptionText = new TextRenderer(oldFontSmaller, black, "Minister of finance", 300);
            _ = new GameObject(rightFolderWidth * .5f, 70, rightFolder, [
                jobDescriptionText
            ]);
            var secretText = new TextRenderer(oldFontSmaller, black, "Secretly Supports Circles", 300);
            _ = new GameObject(rightFolderWidth * .5f, 100, rightFolder, [
                secretText
            ]);

            const float padding = 10;
            var currentX = 120f;
            const float gap = 30f;
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                new TiledSpriteRenderer(horizontalDivider, rightFolderWidth, 11),
            ]);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                new TextRenderer(boldFontSmaller, black, "Goal:", 150)
            ]){ Transform = { Pivot = (0, 0) } };
            var goalText = new TextRenderer(oldFontSmaller, black, "Have all the ministers support the cause for circles", 290);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                goalText
            ]){ Transform = { Pivot = (0, 0) } };
            
            _ = new GameObject(padding, currentX += gap * 2, rightFolder, [
                new TiledSpriteRenderer(horizontalDivider, rightFolderWidth, 11),
            ]);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                new TextRenderer(boldFontSmaller, black, "Action:", 150)
            ]){ Transform = { Pivot = (0, 0) } };
            var actionText = new TextRenderer(oldFontSmaller, black, "Bribe the target, reducing their suspicion.", 290);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                actionText
            ]) { Transform = { Pivot = (0, 0) } };
            
        // logic
        var ministers = new GameObject(360, 0, leftFolder, [
            new Ministers()
        ]);

        var you = new GameObject(0, 0, null, [
            
        ]);

    }
}