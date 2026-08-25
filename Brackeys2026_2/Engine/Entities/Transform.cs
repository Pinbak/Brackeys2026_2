namespace Brackeys2026_2.Engine.Entities;

public class Transform
{
    // the relative positions to its parent
    public float X
    {
        get => _x;
        set
        {
            _x = value;
            UpdateTransform();
        }
    }

    public float Y
    {
        get => _y;
        set
        {
            _y = value;
            UpdateTransform();
        }
    }

    public float Angle
    {
        get;
        set
        {
            field = value;
            UpdateTransform();
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
            UpdateTransform();
        }
    } = 1.0f;

    // actual real world positions
    public float ActualX
    {
        get;
        set
        {
            field = value;
        }
    }

    public float ActualY
    {
        get;
        set
        {
            field = value;
        }
    }

    public float ActualAngle
    {
        get;
        set
        {
            field = value;
        }
    }

    public float ActualScale
    {
        get;
        set
        {
            field = value;
        }
    } = 1.0f;

    private float _x;
    private float _y;
    private GameObjectBase GameObjectBase { get; set; } // that this belongs to
    private bool _isRoot;

    internal Transform(GameObjectBase gameObjectBase, float relativeX, float relativeY, Transform? parentTransform)
    {
        GameObjectBase = gameObjectBase;
        _isRoot = gameObjectBase is RootGameObject;
        _x = relativeX;
        _y = relativeY;
        if (parentTransform is null) return;
        UpdateTransformRelatives(parentTransform);
    }

    private void UpdateTransformRelatives(Transform parent)
    {
        ActualX = parent.ActualX + X;
        ActualY = parent.ActualY + Y;
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
        foreach (var child in GameObjectBase.Children)
        {
            child.Transform.UpdateDescendantsTransforms(child);
            child.Transform.UpdateTransformRelatives(currentGameObject.Transform);
        }
        
    }
}