using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System.Tweening;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class Card(Minister minister, Collider collider, GameObject cardOutline, Button nextTurnButton) : Component
{
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

    private void MoveToNewPosition(float x, float y)
    {
        const float durationSeconds = 3f;
        Tweener.MoveXTo(GameObject.Transform, x, durationSeconds);
        Tweener.MoveYTo(GameObject.Transform, y, durationSeconds);
    }

}