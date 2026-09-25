using Brackeys2026_2.Engine;
using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Game.Classes;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game;

public class Game1 : Core
{
    private Random _random = new Random();
    private List<GameObject> _objects = [];
    
    protected override void Layout()
    {
        
        var oldFont = new Font("Game/Assets/IBMPlexSerif-Light.ttf");
        var face1 = new Texture("Game/Assets/p1.png");
        var face2 = new Texture("Game/Assets/p2.png");
        var face3 = new Texture("Game/Assets/p3.png");
        var faces = new Texture("Game/Assets/p1p2.png");
        // for (var i = 0; i < 100000; i++)
        // {
        //     var spriteRenderer1 = new SpriteRenderer(face1, (byte)_random.Next(0, 255), (byte)_random.Next(0, 255), (byte)_random.Next(0, 255));
        //     // var spriteRenderer2 = new SpriteRenderer(face2);
        //     // var spriteRenderer3 = new SpriteRenderer(face3);
        //     var obj = new GameObject(_random.Next(100, 1920), _random.Next(100, 1080), null, [spriteRenderer1, new Bullet()]);
        //     // var obj2 = new GameObject(_random.Next(100, 1920), _random.Next(100, 1080), null, [spriteRenderer2, new Bullet()]);
        //     // var obj3 = new GameObject(_random.Next(100, 1920), _random.Next(100, 1080), null, [spriteRenderer3, new Bullet()]);
        //     _objects.Add(obj);
        //     // _objects.Add(obj2);
        //     // _objects.Add(obj3);
        // }

        var tileMap = new Tilemap(faces);
        var obj = new GameObject(100, 100, null, [tileMap]);
        
        var fpsCounter = new FpsCounter(oldFont);
        var fpsCounterObject = new GameObject(100, 25, null, [fpsCounter]);
        /**

        Window.ChangeBackgroundColour(new Colour(242, 218, 175, 255));
        var folderTexture = new Texture("Game/Assets/folder.png");
        var oldFont = new Font("Game/Assets/IBMPlexSerif-Light.ttf");
        var fpsCounter = new FpsCounter(oldFont);
        var fpsCounterObject = new GameObject(100, 25, null, [fpsCounter]);
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
        var note = new Texture("Game/Assets/note2.png");
        var button = new Texture("Game/Assets/button.png");
        var buttonDown = new Texture("Game/Assets/buttonDown.png");
        var buttonDisabled = new Texture("Game/Assets/buttonDisabled.png");
        var outline = new Texture("Game/Assets/outline.png");
        var federalist = new Texture("Game/Assets/federalistPolicy.png");
        var loyalist = new Texture("Game/Assets/loyalistPolicy.png");
        var agrarian = new Texture("Game/Assets/agrarianPolicy.png");
        var technocrat = new Texture("Game/Assets/technocratPolicy.png");
        var pc = new Texture("Game/Assets/pC.png");
        var policyOutline = new Texture("Game/Assets/policyOutline.png");
        var misc3 = new Texture("Game/Assets/sideThingDarker.png");
        var misc4 = new Texture("Game/Assets/pcBackground.png");
        var yellowNote = new Texture("Game/Assets/yellowNote.png");
        var mainMenuTexture = new Texture("Game/Assets/mainMenu.png");
        var fColour = new Texture("Game/Assets/federalistColour.png");
        var aColour = new Texture("Game/Assets/agrarianColour.png");
        var lColour = new Texture("Game/Assets/loyalistColour.png");
        var tColour = new Texture("Game/Assets/technocratColour.png");

        var soundTrack = new Audio("Game/Assets/VibingOverVenus.ogg");
        var crowd = new Audio("Game/Assets/crowdReaction.ogg");
        var next = new Audio("Game/Assets/nextTurn.ogg");
        var paperFlick = new Audio("Game/Assets/paper.ogg");
        soundTrack.Loop();
        soundTrack.Play();
        const int leftFolderHeight = 850;
        var black = Window.Black;

        var mainMenu = new GameObject(0, 0, null);
            var menuFolder = new GameObject(960, 540, mainMenu, [
                new SpriteRenderer(mainMenuTexture),
            ]);
            var playText = new Text("Play", boldFont, 300);
            var playButton = new Button(button, buttonDown, buttonDisabled, playText);
            _ = new GameObject(50, -50, menuFolder, [playButton]);
            _ = new GameObject(10, 960, mainMenu, [new TextRenderer(boldFontSmaller, "Art and Programming by Joe Ash\nMusic by Kevin MacLeod\nSound effects from FreeSounds", 800)])
                { Transform = { Pivot = (0, 0) } };
            _ = new GameObject(1750, 1020, mainMenu, [new TextRenderer(boldFontSmaller, "Version 0.1", 800)])
                { Transform = { Pivot = (0, 0) } };

        var gameOverScreen = new GameObject(0, 0, null);
        var gameOverText = new TextRenderer(boldFont, black, "", 500);
        _ = new GameObject(960, 540, gameOverScreen, [
            gameOverText
        ]);

        var game = new GameObject(0, 0, null);
        var leftFolder = new GameObject(-20, 100, game, [
            new Tiled9SliceSpriteRenderer(folderTexture, 1300, leftFolderHeight, 45)
        ]);
        var text = new GameObject(800, 75, game, [
            new TextRenderer(oldFont, black, "Cabinet", 150)
        ]);
        var arrow = new GameObject(880, 75, game, [
            new SpriteRenderer(misc1)
        ]);
        text.Transform.Angle = -5;

        // dividers
        _ = new GameObject(400, 100, game, [
            new TiledSpriteRenderer(divider, 30, leftFolderHeight)
        ]);
        _ = new GameObject(450, 100, game, [
            new TiledSpriteRenderer(divider, 30, leftFolderHeight)
        ]);

        // political capital
        _ = new GameObject(20, 1000, game, [new SpriteRenderer(misc4)]){ Transform = { Pivot = (0, 0)} };
        var pcText = new TextRenderer(boldFontSmaller, "Political Capital: 0", 300);
        _ = new GameObject(70, 1020, game, [
            pcText
        ]) { Transform = { Pivot = (0, 0)} };

        // right folder
        const int rightFolderWidth = 600;
        const int rightFolderHeight = 1080;

        var rightFolders = new GameObject(1400, 0, game);
            var tab = new SpriteRenderer(misc2);
            _ = new GameObject(-10, 120, rightFolders, [
                tab,
                new TextRenderer(oldFont, black, "You", 150),
            ])
            {
                Transform =
                {
                    Angle = -90
                }
            };
            var tab2 = new SpriteRenderer(misc3);
            _ = new GameObject(-10, 120 + misc2.Width - 20, rightFolders, [
                tab2,
                new TextRenderer(oldFont, black, "Actions", 150),
            ])
            {
                Transform =
                {
                    Angle = -90
                }
            };
            const float padding = 10;
            var tabColliderGameObject = new GameObject(-10, 200, rightFolders);
            var tabCollider = new Collider(tabColliderGameObject.Transform, misc2.Height - 10, misc2.Width * 2);
            tabColliderGameObject.AddComponent(tabCollider);

            var rightFolderMinisterView = new GameObject(0, 0, rightFolders,
                [new Tiled9SliceSpriteRenderer(folderTexture, rightFolderWidth, rightFolderHeight, 45)]);
                const int rightFolderOverlap = 20;
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 40, rightFolderMinisterView, [
                    new TextRenderer(boldFont, black, "Minister Actions", 500)
                ]);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 110, rightFolderMinisterView, [
                    new TextRenderer(oldFont, black, "Click on a minister to show actions. They cost political capital. Actions can be carried out at the start of your turn.", 500)
                ]);
                var ministerName = new TextRenderer(boldFontSmallerCentered, black, "Currently Selected:", 500);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 200, rightFolderMinisterView, [
                    ministerName
                ]);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 450, rightFolderMinisterView, [
                    new TextRenderer(boldFontSmallerCentered, black, "Policy History:", 500)
                ]);

                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 520, rightFolderMinisterView, [
                    new TextRenderer(boldFontSmallerCentered, black, "Technocratic:", 500)
                ]);

                var tAmount = new TiledSpriteRenderer(tColour, 0, 30);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 100, 495, rightFolderMinisterView, [
                    tAmount
                ]);

                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 560, rightFolderMinisterView, [
                    new TextRenderer(boldFontSmallerCentered, black, "Loyalist:", 500)
                ]);

                var lAmount = new TiledSpriteRenderer(lColour, 0, 30);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 100, 535, rightFolderMinisterView, [
                    lAmount
                ]);

                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 600, rightFolderMinisterView, [
                    new TextRenderer(boldFontSmallerCentered, black, "Federalist:", 500)
                ]);

                var fAmount = new TiledSpriteRenderer(fColour, 0, 30);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 100, 575, rightFolderMinisterView, [
                    fAmount
                ]);

                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 640, rightFolderMinisterView, [
                    new TextRenderer(boldFontSmallerCentered, black, "Agrarian:", 500)
                ]);

                var aAmount = new TiledSpriteRenderer(aColour, 0, 30);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 100, 615, rightFolderMinisterView, [
                    aAmount
                ]);

                var killCost = new TextRenderer(boldFontSmaller, black, "150PC", 500);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 800, rightFolderMinisterView,
                    [killCost]);
                var killButton = new Button(button, buttonDown, buttonDisabled, new Text("'Dispose' of", boldFontSmaller, 300));
                var killAction = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 800, rightFolderMinisterView, [
                    killButton,
                ]);
                var convertCost = new TextRenderer(boldFontSmaller, black, "150PC", 500);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 860, rightFolderMinisterView,
                    [convertCost]);
                var convertButton = new Button(button, buttonDown, buttonDisabled, new Text("Change Ideology", boldFontSmaller, 300));
                var convertAction = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 860, rightFolderMinisterView, [
                    convertButton,
                ]);
                var bribeCost = new TextRenderer(boldFontSmaller, black, "150PC", 500);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap - 200, 920, rightFolderMinisterView,
                    [bribeCost]);
                var bribeButton = new Button(button, buttonDown, buttonDisabled, new Text("Bribe", boldFontSmaller, 300));
                var bribeAction = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 920, rightFolderMinisterView, [
                    bribeButton,
                ]);

                var feedBack = new TextRenderer(oldFont, black, "", 500);
                _ = new GameObject(rightFolderWidth * .5f - rightFolderOverlap, 720, rightFolderMinisterView, [
                    feedBack
                ]);

            var rightFolder = new GameObject(0, 0, rightFolders, [
                new Tiled9SliceSpriteRenderer(folderTexture, rightFolderWidth, rightFolderHeight, 45),
            ]);
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
                    new TextRenderer(boldFont, black, "Perk of the Role:", 350)
                ]){ Transform = { Pivot = (0, 0) } };

                var actionText = new TextRenderer(oldFont, black, "", 500);
                _ = new GameObject(padding, currentX += gap, rightFolder, [
                    actionText
                ]) { Transform = { Pivot = (0, 0) } };

                var stickySprite = new SpriteRenderer(note);
                var stickyNote = new GameObject(padding, currentX += gap + 25, rightFolder, [
                    stickySprite
                ]) { Transform = { Pivot = (0, 0) } };
                var stickyNoteText = new TextRenderer(oldFont, "", 500);
                var noteText = new GameObject(padding, currentX + 25, rightFolder, [stickyNoteText]) { Transform = { Pivot = (0, 0) } };
                var stickyNoteManager = new NoteManager(stickyNoteText, stickySprite, note, yellowNote);
                noteText.AddComponent(stickyNoteManager);

                // var confirmButton = new Button(button, buttonDown, buttonDisabled, new Text("Confirm Action", boldFontSmaller, 300));
                // var confirmAction = new GameObject(padding + 50 + button.Width *.5f, currentX + 430, rightFolder, [
                //     confirmButton,
                // ]);
                // confirmButton.DisablePresses();
                var nextTurnButton = new Button(button, buttonDown, buttonDisabled, new Text("Next turn", boldFontSmaller, 300));
                var nextTurn = new GameObject(padding + 170 + button.Width *.5f, currentX + 430 + button.Height + 10, rightFolder, [
                    nextTurnButton,
                ]);


        // rightFolder.IsEnabled = false;

        // var newsGameObject = new GameObject(500, 500, game);
        // var newsHeadline = new TextRenderer(boldFont, "", 300);
        // newsGameObject.AddComponent(newsHeadline);
        // newsGameObject.AddComponent(new SpriteRenderer(news));
        // var newsComponent = new News();
        // newsGameObject.AddComponent(newsComponent);

        var rightFolderManager = new RightFolderManager(misc2, misc3, tab, tab2, rightFolder, rightFolderMinisterView,
            tabCollider, ministerName, killButton, bribeButton, convertButton, killCost, bribeCost, convertCost,
            feedBack, next, boldFontSmallerCentered, oldFontSmaller, paperFlick, aAmount, tAmount, fAmount, lAmount);
        rightFolders.AddComponent(rightFolderManager);
        rightFolders.Tag = "Right folder";

        // logic
        var menuManager = new MenuManager(mainMenu, game, gameOverScreen, gameOverText, playButton);
        var menu = new GameObject(0, 0, null, [menuManager]);

        var lastPlayed = new LastPlayed(policyOutline, federalist, loyalist, agrarian, technocrat);
        _ = new GameObject(210, 250, leftFolder, [lastPlayed]);
        var bureaucracy = new GameObject(150, -50, leftFolder, [
            new TextRenderer(oldFont, black, "Bureaucracy", 300)
        ]);
        _ = new GameObject(210, 50, leftFolder, [new TextRenderer(boldFontSmallerCentered, "Last Policy Implemented:", 300)]);
        _ = new GameObject(250, -30, leftFolder, [
            new SpriteRenderer(misc1)
        ]);
        bureaucracy.Transform.Angle = 5;

        var player = new PlayerInteraction(nextTurnButton, playerDescriptionText, jobDescriptionText,
            secretText, goalText, actionText, pcText, loyalist, agrarian, technocrat, federalist, pc, rightFolderManager, stickyNoteManager, next);
        var playerGameObject = new GameObject(0, 0, game, [
            player
        ]);

        var ministers = new GameObject(520, 10, leftFolder, [
            new GameManager(card, outline, [face1, face2, face3, face4, face5, face6, face7, face8, face9, face10, face11], boldFontSmallerCentered, oldFontSmaller
            , nextTurnButton, player, lastPlayed, stickyNoteManager, menuManager, crowd, paperFlick)
        ]);
        **/
}
}