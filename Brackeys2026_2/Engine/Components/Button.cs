using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Engine.Components;

public class Button : SpriteRenderer
{
    public Action? HasPressed { get; set; }
    
    private Collider? _collider;
    private float _startingY;
    private Texture _downTexture;
    private Texture _disabledTexture;
    private bool _isDown;
    private Text _text;
    private bool _isPressable = true;
    private string _defaultText;
    
    public Button(Texture upTexture, Texture downTexture, Texture disabledTexture, Text content) : base(upTexture)
    {
        _downTexture = downTexture;
        _disabledTexture = disabledTexture;
        _defaultText = content.Content;
        _text = content;
        Own(_text);
    }

    protected override void OnInitialise()
    {
        var collider = new Collider(GameObject.Transform, SpriteTexture.Width, SpriteTexture.Height);
        GameObject.AddComponent(collider);
        _collider = collider;
        _startingY = GameObject.Transform.Y;
    }

    protected override void OnUpdate()
    {
        if (_collider is null) return;
        if (!_isPressable) return;
        if (Mouse.HasPressed(Mouse.Button.Left))
        {
            if (_collider.IsMouseInside())
                _isDown = true;
        }

        if (Mouse.HasReleased(Mouse.Button.Left))
        {
            if (_isDown)
            {
                HasPressed?.Invoke();
            }

            _isDown = false;
        }
        
        if (_collider.HasMouseEntered())
        {
            Tweener.RotateTo(GameObject.Transform, 2, 0.1f);
        }
        else if (_collider.HasMouseExited())
        {
            Tweener.RotateTo(GameObject.Transform, 0, 0.1f);
        }
    }

    protected override void OnDraw()
    {
        if (!_isPressable)
        {
            _disabledTexture.Draw(GameObject.Transform);
        }
        else
        {
            if (!_isDown)
                SpriteTexture.Draw(GameObject.Transform);
            else
                _downTexture.Draw(GameObject.Transform);
        }
        _text.Draw(GameObject.Transform);
        
    }

    public void SetText(string text)
    {
        _text.UpdateText(text);
    }
    
    public void EnablePresses()
    {
        _isPressable = true;
        _text.UpdateText(_defaultText);
        Tweener.RotateTo(GameObject.Transform, 0, 0.1f);
    }

    public void DisablePresses(string? disabledMessage = null)
    {
        _isPressable = false;
        _text.UpdateText(disabledMessage ?? _defaultText);
        Tweener.RotateTo(GameObject.Transform, 0, 0.1f);
    }

}