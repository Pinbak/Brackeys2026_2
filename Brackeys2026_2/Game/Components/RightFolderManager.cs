using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Game.Components;

public class RightFolderManager : Component
{
    public Button KillButton { get; set; }
    public Button ConvertButton { get; set; }
    public Button BribeButton { get; set; }

    private Collider _collider;
    private bool _tab;
    private Texture _darkerTab;
    private Texture _lighterTab;
    private SpriteRenderer _tab1;
    private SpriteRenderer _tab2;
    private GameObject _playerInformation;
    private GameObject _aiInformation;
    private TextRenderer _ministerName;
    private TextRenderer _killCost;
    private TextRenderer _bribeCost;
    private TextRenderer _convertCost;
    private TextRenderer _feedBack;

    public RightFolderManager(Texture misc2, Texture misc3, SpriteRenderer tab, SpriteRenderer tab2,
        GameObject rightFolder, GameObject rightFolderMinisterView, Collider tabCollider, TextRenderer ministerName,
        Button killButton, Button bribeButton, Button convertButton, TextRenderer killCost, TextRenderer bribeCost,
        TextRenderer convertCost, TextRenderer feedBack)
    {
        _collider = tabCollider;
        _lighterTab = misc2;
        _darkerTab = misc3;
        _tab1 = tab;
        _tab2 = tab2;
        _playerInformation = rightFolder;
        _aiInformation = rightFolderMinisterView;
        KillButton = killButton;
        BribeButton = bribeButton;
        ConvertButton = convertButton;
        _ministerName = ministerName;
        KillButton.DisablePresses();
        BribeButton.DisablePresses();
        ConvertButton.DisablePresses();
        _killCost = killCost;
        _bribeCost = bribeCost;
        _convertCost = convertCost;
        _feedBack = feedBack;
    }

    protected override void OnUpdate()
    {
        if (Mouse.HasPressed(Mouse.Button.Left))
        {
            if (_collider.IsMouseInside())
            {
                _tab = !_tab;
                ChangeUi();
            }
        }
    }

    private void ChangeUi()
    {
        if (!_tab)
        {
            _tab1.ChangeTexture(_lighterTab);
            _tab2.ChangeTexture(_darkerTab);
            _playerInformation.IsEnabled = true;
            _aiInformation.IsEnabled = false;
        }
        else
        {
            _tab1.ChangeTexture(_darkerTab);
            _tab2.ChangeTexture(_lighterTab);
            _playerInformation.IsEnabled = false;
            _aiInformation.IsEnabled = true;
        }
    }

    public void GiveFeedback(string message)
    {
        _feedBack.Set(message);
    }
    
    public void UpdateUi(MinisterFaceCard ministerFaceCard)
    {
        var minister = ministerFaceCard.Minister;
        _ministerName.Set(minister.Name);
    }

    public void UpdateCosts(int toKill, int toConvert, int toBribe)
    {
        _killCost.Set($"{toKill}PC");
        _convertCost.Set($"{toConvert}PC");
        _bribeCost.Set($"{toBribe}PC");
    }
    
    public void RemoveSelected()
    {
        DisablePresses();
        _ministerName.Set("");
    }
    
    public void EnablePresses()
    {
        KillButton.EnablePresses();
        BribeButton.EnablePresses();
        ConvertButton.EnablePresses();
    }
    
    public void UpdateButtonEnables(int playerPoliticalCapital, int costToBribe, int costToConvert, int costToKill)
    {
        if (playerPoliticalCapital >= costToBribe) BribeButton.EnablePresses();
        if (playerPoliticalCapital >= costToConvert) ConvertButton.EnablePresses();
        if (playerPoliticalCapital >= costToKill) KillButton.EnablePresses();
    }

    public void DisablePresses()
    {
        KillButton.DisablePresses();
        BribeButton.DisablePresses();
        ConvertButton.DisablePresses();
    }
    
    // protected override void OnDraw()
    // {
    //     var pivotX = _collider.Width * _collider.GameObject.Transform.Pivot.x * _collider.GameObject.Transform.Scale;
    //     var pivotY = _collider.Height * _collider.GameObject.Transform.Pivot.y * _collider.GameObject.Transform.Scale;
    //     Renderer.DrawRectangle(new Rectangle(_collider.GameObject.Transform.ActualX - pivotX, _collider.GameObject.Transform.ActualY - pivotY,
    //         _collider.Width, _collider.Height));
    // }
    
}