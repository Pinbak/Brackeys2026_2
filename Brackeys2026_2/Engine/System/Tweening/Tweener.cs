using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Engine.System.Tweening;

public static class Tweener
{
    public enum Type {
        PositionX,
        PositionY,
        Rotation,
        Scale,
    };
    
    private static readonly List<Tween> ActiveTweens = [];

    internal static void Process()
    {
        for (var i = 0; i < ActiveTweens.Count;)
        {
            if (ActiveTweens[i].Process())
            {
                ActiveTweens.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public static void RotateTo(Transform transform, float angle, float duration, Func<float, float>? easeFunction = null)
    {
        var tween = new Tween(transform, Type.Rotation, duration, easeFunction) { TargetRotation = angle };
        ActiveTweens.Add(tween);
    }
    
    public static void MoveXTo(Transform transform, float x, float duration, Func<float, float>? easeFunction = null)
    {
        var tween = new Tween(transform, Type.PositionX, duration, easeFunction) { TargetX = x };
        ActiveTweens.Add(tween);
    }
    
    public static void MoveYTo(Transform transform, float y, float duration, Func<float, float>? easeFunction = null)
    {
        var tween = new Tween(transform, Type.PositionY, duration, easeFunction) { TargetY = y };
        ActiveTweens.Add(tween);
    }
    
    public static void ScaleTo(Transform transform, float scale, float duration, Func<float, float>? easeFunction = null)
    {
        var tween = new Tween(transform, Type.Scale, duration, easeFunction) { TargetScale = scale };
        ActiveTweens.Add(tween);
    }
    
}