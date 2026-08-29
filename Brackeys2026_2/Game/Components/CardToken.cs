using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Game.Components;

public class CardToken : Component
{
    protected override void OnInitialise()
    {
        Tweener.MoveXTo(GameObject.Transform, 230 - 520, 0.8f, EaseFunctions.OutQuart, 0f, true);
        Tweener.MoveYTo(GameObject.Transform, 300 - 10, 0.8f, EaseFunctions.OutQuart, 0f, true);
    }
}