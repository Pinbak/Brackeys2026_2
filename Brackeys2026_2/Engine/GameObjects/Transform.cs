namespace Brackeys2026_2.Engine.GameObjects;

public class Transform
{
    // the relative positions to its parent
    public float X
    {
        get => _x;
        set
        {
            _x = value;
            _hasUpdated = true;
        }
    }

    public float Y
    {
        get => _y;
        set
        {
            _y = value;
            _hasUpdated = true;
            
        }
    }

    public float Angle
    {
        get;
        set
        {
            field = value;
            _hasUpdated = true;
        }
    }

    // public float ScaleX { get; set; } = 1.0f;
    // public float ScaleY { get; set; } = 1.0f;
    public float Scale
    {
        get;
        set
        {
            field = value;
            _hasUpdated = true;
        }
    } = 1.0f;

    public (float x, float y) Pivot { get; set; } = (.5f, .5f);
    
    public GameObject? GameObject => GameObjectBase as GameObject;

    // actual real world positions
    internal float ActualX { get; private set; }

    internal float ActualY { get; private set; }

    internal float ActualAngle { get; private set; }

    internal float ActualScale { get; private set; } = 1.0f;

    private float _x;
    private float _y;
    private GameObjectBase GameObjectBase { get; set; } // that this belongs to
    private bool _isRoot;
    private bool _hasUpdated;

    internal Transform(GameObjectBase gameObjectBase, float relativeX, float relativeY, Transform? parentTransform)
    {
        GameObjectBase = gameObjectBase;
        _isRoot = gameObjectBase is RootGameObject;
        _x = relativeX;
        _y = relativeY;
        if (parentTransform is null) return;
        UpdateTransformRelatives(parentTransform);
    }
    
    public void Update()
    {
        if (!_hasUpdated) return;
        _hasUpdated = false;
        UpdateTransform();
    }

    private void UpdateTransformRelatives(Transform parent)
    {
        var radians = parent.ActualAngle * MathF.PI / 180f;

        var cos = MathF.Cos(radians);
        var sin = MathF.Sin(radians);

        var scaledX = X * parent.ActualScale;
        var scaledY = Y * parent.ActualScale;

        ActualX = parent.ActualX + scaledX * cos - scaledY * sin;

        ActualY = parent.ActualY + scaledX * sin + scaledY * cos;

        ActualAngle = parent.ActualAngle + Angle;
        ActualScale = parent.ActualScale * Scale;
    }

    private void UpdateTransform()
    {
        if (_isRoot) return;
        var gameObject = GameObjectBase as GameObject;
        UpdateTransformRelatives(gameObject!.Parent.Transform);
        UpdateDescendantsTransforms(gameObject);
    }

    private void UpdateDescendantsTransforms(GameObjectBase currentGameObject)
    {
        foreach (var child in currentGameObject.Children)
        {
            if (!child.IsEnabled) continue;
            child.Transform.UpdateTransformRelatives(currentGameObject.Transform);
            child.Transform.UpdateDescendantsTransforms(child);
        }
    }
}