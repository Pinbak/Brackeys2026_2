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
    private bool _isDown;
    private Text _text;
    
    public Button(Texture upTexture, Texture downTexture, Text content) : base(upTexture)
    {
        _downTexture = downTexture;
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
        if (!_isDown)
            SpriteTexture.Draw(GameObject.Transform);
        else
            _downTexture.Draw(GameObject.Transform);
        
        _text.Draw(GameObject.Transform);
    }

}