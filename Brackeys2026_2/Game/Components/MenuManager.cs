using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Game.Components;

public class MenuManager : Component
{
    private readonly GameObject _mainMenu;
    private readonly GameObject _game;
    private readonly GameObject _gameOverScreen;
    private readonly TextRenderer _gameOver;
    private readonly Button _playButton;

    public MenuManager(GameObject mainMenu, GameObject game, GameObject gameOverScreen, TextRenderer gameOver,
        Button playButton)
    {
        _mainMenu = mainMenu;
        _game = game;
        _gameOverScreen = gameOverScreen;
        _gameOver = gameOver;
        _playButton = playButton;
        playButton.HasPressed += PlayButton;
    }

    private void PlayButton()
    {
        _mainMenu.IsEnabled = false;
        _game.IsEnabled = true;
    }

    protected override void OnInitialise()
    {
        _mainMenu.IsEnabled = true;
        _game.IsEnabled = false;
        _gameOverScreen.IsEnabled = false;
    }

    public void GameOver(bool isWin)
    {
        _game.IsEnabled = false;
        _gameOverScreen.IsEnabled = true;
        _gameOver.Set(isWin
            ? "All ministers are aligned. You have won! Congratulations on reaching unity in the cabinet."
            : "You have been removed from office. Perhaps you were unfortunate, but your peers don't seem to have agreed with you.");
    }
}