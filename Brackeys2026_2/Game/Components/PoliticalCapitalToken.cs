using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Game.Components;

public class PoliticalCapitalToken(float delay) : Component
{
    protected override void OnInitialise()
    {
        Tweener.MoveXTo(GameObject.Transform, 1500, 0.8f, EaseFunctions.OutQuart, delay, true);
        Tweener.MoveYTo(GameObject.Transform, 475, 0.8f, EaseFunctions.OutQuart, delay, true);
        Tweener.ScaleTo(GameObject.Transform, 0.1f, 0.8f, EaseFunctions.OutQuart, delay, true);
    }
}