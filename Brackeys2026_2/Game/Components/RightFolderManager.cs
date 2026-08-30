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
    private GameObject? _selected;
    private TextRenderer _ministerName;
    private TextRenderer _killCost;
    private TextRenderer _bribeCost;
    private TextRenderer _convertCost;
    private TextRenderer _feedBack;
    private readonly Audio _buttonClick;
    private readonly Font _bold;
    private readonly Font _light;

    public RightFolderManager(Texture misc2, Texture misc3, SpriteRenderer tab, SpriteRenderer tab2,
        GameObject rightFolder, GameObject rightFolderMinisterView, Collider tabCollider, TextRenderer ministerName,
        Button killButton, Button bribeButton, Button convertButton, TextRenderer killCost, TextRenderer bribeCost,
        TextRenderer convertCost, TextRenderer feedBack, Audio buttonClick, Font bold, Font light)
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
        KillButton.HasPressed += buttonClick.Play;
        BribeButton.HasPressed += buttonClick.Play;
        ConvertButton.HasPressed += buttonClick.Play;
        KillButton.DisablePresses();
        BribeButton.DisablePresses();
        ConvertButton.DisablePresses();
        _killCost = killCost;
        _bribeCost = bribeCost;
        _convertCost = convertCost;
        _feedBack = feedBack;
        _buttonClick = buttonClick;
        _bold = bold;
        _light = light;
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
        // var minister = ministerFaceCard.Minister;
        // _ministerName.Set("Minister Selected: " + minister.Name);
        _selected = new GameObject(0, 0, GameObject);
        GenerateFaceCard(ministerFaceCard);
    }

    private void GenerateFaceCard(MinisterFaceCard ministerFaceCard)
    {
        if (_selected is null) return;
        _selected.Destroy();
        var minister = ministerFaceCard.Minister;
        _selected = new GameObject(280, 300, _aiInformation);
        var cardGameObject = new GameObject(0, 0, _selected, [
            new SpriteRenderer(ministerFaceCard.Card),
        ]);

        var face = new SpriteRenderer(minister.Face);
        _ = new GameObject(-96, 0, cardGameObject, [
            face
        ]);
        
        var roleText = new TextRenderer(_bold, minister.Role.Name, 200);
        _ = new GameObject(60, -10, cardGameObject, [
            roleText 
        ]);
        var nameText = new TextRenderer(_light, minister.Name, 200);
        _ = new GameObject(50, 40, cardGameObject, [
            nameText
        ]);
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
        _selected?.Destroy();
        _selected = null;
    }
    
    public void EnablePresses()
    {
        KillButton.EnablePresses();
        BribeButton.EnablePresses();
        ConvertButton.EnablePresses();
    }
    
    public void UpdateButtonEnables(int playerPoliticalCapital, int costToBribe, int costToConvert, int costToKill)
    {
        DisablePresses();
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