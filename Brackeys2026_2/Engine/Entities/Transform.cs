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
            UpdateDescendantsTransforms();
        }
    }

    public float Y
    {
        get => _y;
        set
        {
            _y = value;
            UpdateDescendantsTransforms();
        }
    }

    public float Angle
    {
        get;
        set
        {
            field = value;
            UpdateDescendantsTransforms();
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
            UpdateDescendantsTransforms();
        }
    } = 1.0f;

    // actual real world positions
    public float ActualX
    {
        get;
        set
        {
            field = value;
             UpdateDescendantsTransforms();
        }
    }

    public float ActualY
    {
        get;
        set
        {
            field = value;
            UpdateDescendantsTransforms();
        }
    }

    public float ActualAngle
    {
        get;
        set
        {
            field = value;
            UpdateDescendantsTransforms();
        }
    }

    public float ActualScale
    {
        get;
        set
        {
            field = value;
            UpdateDescendantsTransforms();
        }
    } = 1.0f;

    private float _x;
    private float _y;
    private GameObject GameObject { get; set; } // that this belongs to

    internal Transform(GameObject gameObject, float relativeX, float relativeY)
    {
        GameObject = gameObject;
        _x = relativeX;
        _y = relativeY;
        ActualX = _x;
        ActualY = _y;
    }

    private void UpdateTransformRelatives(Transform parent)
    {
        ActualX = parent.ActualX + X;
        ActualY = parent.ActualY + Y;
        ActualAngle = parent.ActualAngle + Angle;
        ActualScale = parent.ActualScale * Scale;
    }

    private void UpdateDescendantsTransforms()
    {
        foreach (var child in GameObject.Children)
        {
            child.Transform.UpdateTransformRelatives(GameObject.Transform);
        }
        
    }
}