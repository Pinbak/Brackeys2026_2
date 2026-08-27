using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Engine.System.Tweening;

internal class Tween
{
    internal float TargetX { get; set; }
    internal float TargetY { get; set; }
    internal float TargetRotation { get; set; }
    internal float TargetScale { get; set; }
    
    private float _startX;
    private float _startY;
    private float _startRotation;
    private float _startScale;

    private Tweener.Type _type;
    private Transform _transform;
    private float _duration;
    private float _progress;
    private Func<float, float> _easeFunction;

    public Tween(Transform transform, Tweener.Type type, float duration, Func<float, float>? easeFunction = null)
    {
        easeFunction ??= EaseFunctions.Linear;
        _easeFunction = easeFunction;
        _type = type;
        _transform = transform;
        _duration = duration;
        _startX = transform.X;
        _startY = transform.Y;
        _startRotation = transform.Angle;
        _startScale = transform.Scale;
    }

    public bool Process()
    {
        _progress += Time.DeltaTime / _duration;

        switch (_type)
        {
            case Tweener.Type.Rotation:
                _transform.Angle = float.Lerp(_startRotation, TargetRotation, GetEase());
                break;
            case Tweener.Type.PositionX:
                _transform.X = float.Lerp(_startX, TargetX, GetEase());
                break;
            case Tweener.Type.PositionY:
                _transform.Y = float.Lerp(_startY, TargetY, GetEase());
                break;
            case Tweener.Type.Scale:
                _transform.Scale = float.Lerp(_startScale, TargetScale, GetEase());
                break;
        }
        // if finished
        return _progress >= 1f;
    }

    private float GetEase()
    {
        return _easeFunction(_progress);
    }
}