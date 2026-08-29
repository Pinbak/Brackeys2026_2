using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Tweening;
using Brackeys2026_2.Game.Classes;
using Brackeys2026_2.Game.Classes.Ministers;

namespace Brackeys2026_2.Game.Components;

public class MinisterFaceCard(Minister minister, Collider collider, GameObject cardOutline, Button nextTurnButton, SpriteRenderer face) : Component
{
    public Role Role => _minister.Role;

    public Minister Minister => _minister;

    public TextRenderer? RoleText { get; set; }
    public TextRenderer? NameText { get; set; }
    private SpriteRenderer _face = face;

    private static readonly Random Random = new();
    private GameObject _outline = cardOutline;
    private Minister _minister = minister;

    protected override void OnInitialise()
    {
        nextTurnButton.HasPressed += NextTurn;
    }

    private void NextTurn()
    {
        UpdateOutline();
    }

    protected override void OnUpdate()
    {
        if (collider.HasMouseEntered())
        {
            Tweener.RotateTo(GameObject.Transform, Random.Next(-5, 5), 0.1f);
        }

        else if (collider.HasMouseExited())
        {
            Tweener.RotateTo(GameObject.Transform, 0f, 0.1f);
        }
    }

    private void UpdateOutline()
    {
        if (_minister.IsTurn)
        {
            _outline.IsEnabled = true;
            Tweener.ScaleTo(GameObject.Transform, 1.05f, 0.1f, EaseFunctions.OutElastic);
            GameObject.BringToFront();
        }
        else
        {
            _outline.IsEnabled = false;
            Tweener.ScaleTo(GameObject.Transform, 1f, 0.1f);
        }
    }

    public void UpdateRoleText()
    {
        RoleText?.Set(_minister.Role.Name);
    }

    public void UpdateNameText()
    {
        NameText?.Set(_minister.Name);
    }

    public void ChangeMinister(Minister newMinister)
    {
        _minister = newMinister;
        UpdateRoleText();
        UpdateNameText();
        face.ChangeTexture(newMinister.Face);
    }
    
    public void MoveToNewPosition(float x, float y)
    {
        const float durationSeconds = 2f;
        Tweener.MoveXTo(GameObject.Transform, x, durationSeconds, EaseFunctions.OutQuart, .5f);
        Tweener.MoveYTo(GameObject.Transform, y, durationSeconds, EaseFunctions.OutQuart, .5f);
    }

}