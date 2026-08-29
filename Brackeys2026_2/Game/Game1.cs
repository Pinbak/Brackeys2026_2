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
        var folderTexture = new Texture("Game/Assets/folder.png");
        var folderLighterTexture = new Texture("Game/Assets/folderLighter.png");
        var oldFont = new Font("Game/Assets/IBMPlexSerif-Light.ttf");
        var oldFontSmaller = new Font("Game/Assets/IBMPlexSerif-Light.ttf", 20);
        var boldFont = new Font("Game/Assets/Staatliches-Regular.ttf", 34);
        var boldFontSmaller = new Font("Game/Assets/Staatliches-Regular.ttf", 28);
        var boldFontSmallerCentered = new Font("Game/Assets/Staatliches-Regular.ttf", 28);
        boldFontSmallerCentered.SetAlignmentCentre();
        var misc1 = new Texture("Game/Assets/arrow.png");
        var misc2 = new Texture("Game/Assets/sideThing.png");
        var divider = new Texture("Game/Assets/divider.png");
        var horizontalDivider = new Texture("Game/Assets/horizontalDivider.png");
        var card = new Texture("Game/Assets/card.png");
        var face1 = new Texture("Game/Assets/p1.png");
        var face2 = new Texture("Game/Assets/p2.png");
        var face3 = new Texture("Game/Assets/p3.png");
        var face4 = new Texture("Game/Assets/p4.png");
        var face5 = new Texture("Game/Assets/p5.png");
        var face6 = new Texture("Game/Assets/p6.png");
        var face7 = new Texture("Game/Assets/p7.png");
        var face8 = new Texture("Game/Assets/p8.png");
        var face9 = new Texture("Game/Assets/p9.png");
        var face10 = new Texture("Game/Assets/p10.png");
        var face11 = new Texture("Game/Assets/p11.png");
        var note = new Texture("Game/Assets/note.png");
        var button = new Texture("Game/Assets/button.png");
        var buttonDown = new Texture("Game/Assets/buttonDown.png");
        var buttonDisabled = new Texture("Game/Assets/buttonDisabled.png");
        var outline = new Texture("Game/Assets/outline.png");

        const int leftFolderHeight = 850;
        var black = Window.Black;
        var leftFolder = new GameObject(-20, 100, null, [
            new Tiled9SliceSpriteRenderer(folderTexture, 1300, leftFolderHeight, 45)
        ]);
        var text = new GameObject(1100, 80, null, [
            new TextRenderer(oldFont, black, "Cabinet", 150)
        ]);
        var arrow = new GameObject(1180, 80, null, [
            new SpriteRenderer(misc1)
        ]);
        text.Transform.Angle = -5;
        
        // dividers
        _ = new GameObject(400, 100, null, [
            new TiledSpriteRenderer(divider, 30, leftFolderHeight)
        ]);
        _ = new GameObject(450, 100, null, [
            new TiledSpriteRenderer(divider, 30, leftFolderHeight)
        ]); 
        
        // right folder
        const int rightFolderWidth = 600;
        const int rightFolderHeight = 1080;
        var rightFolder = new GameObject(1400, 0, null, [
            new Tiled9SliceSpriteRenderer(folderTexture, rightFolderWidth, rightFolderHeight, 45),
        ]);
            _ = new GameObject(-10, 120, rightFolder, [
                new SpriteRenderer(misc2),
                new TextRenderer(oldFont, black, "You", 150),
            ])
            {
                Transform =
                {
                    Angle = -90
                }
            };
            const int rightFolderOverlap = 20;
            var playerDescriptionText = new TextRenderer(boldFont, black, "Insert name here", 500);
            _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 40, rightFolder, [
                playerDescriptionText
            ]);
            var jobDescriptionText = new TextRenderer(boldFontSmallerCentered, black, "Minister of finance", 500);
            _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 85, rightFolder, [
                jobDescriptionText
            ]);
            var secretText = new TextRenderer(boldFontSmallerCentered, black, "Secretly...", 500);
            _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 130, rightFolder, [
                secretText
            ]);

            const float padding = 10;
            var currentX = 120f;
            const float gap = 50f;
            _ = new GameObject(0, currentX += gap, rightFolder, [
                new TiledSpriteRenderer(horizontalDivider, rightFolderWidth, 30),
            ]);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                new TextRenderer(boldFont, black, "Goal:", 200)
            ]){ Transform = { Pivot = (0, 0) } };
            var goalText = new TextRenderer(oldFont, black, "Have all the ministers support the cause for dots", 500);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                goalText
            ]){ Transform = { Pivot = (0, 0) } };
            
            _ = new GameObject(0, currentX += gap * 2, rightFolder, [
                new TiledSpriteRenderer(horizontalDivider, rightFolderWidth, 30),
            ]);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                new TextRenderer(boldFont, black, "Action:", 200)
            ]){ Transform = { Pivot = (0, 0) } };
            var actionText = new TextRenderer(oldFont, black, "Bribe the target, reducing their suspicion.", 500);
            _ = new GameObject(padding, currentX += gap, rightFolder, [
                actionText
            ]) { Transform = { Pivot = (0, 0) } };
            
            var stickyNote = new GameObject(padding, currentX += gap, rightFolder, [
                new SpriteRenderer(note),
            ]) { Transform = { Pivot = (0, 0) } };

            var confirmButton = new Button(button, buttonDown, buttonDisabled, new Text("Confirm Action", boldFontSmaller, 300));
            var confirmAction = new GameObject(padding + 50 + button.Width *.5f, currentX + 430, rightFolder, [
                confirmButton,
            ]);
            confirmButton.DisablePresses();
            var nextTurnButton = new Button(button, buttonDown, buttonDisabled, new Text("Next turn", boldFontSmaller, 300));
            var nextTurn = new GameObject(padding + 170 + button.Width *.5f, currentX + 430 + button.Height + 10, rightFolder, [
                nextTurnButton,
            ]);
            
        // logic
        var player = new PlayerInteraction(confirmButton, nextTurnButton, playerDescriptionText, jobDescriptionText, secretText, goalText, actionText);
        var playerGameObject = new GameObject(0, 0, null, [
            player
        ]);
        
        var ministers = new GameObject(520, 10, leftFolder, [
            new GameManager(card, outline, [face1, face2, face3, face4, face5, face6, face7, face8, face9, face10, face11], boldFontSmallerCentered, oldFontSmaller
            , confirmButton, nextTurnButton, player)
        ]);
        

    }
}