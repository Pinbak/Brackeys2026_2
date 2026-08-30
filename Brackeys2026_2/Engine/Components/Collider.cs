using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.Components;

public class Collider : Component
{
    public float Width => _width;

    public float Height => _height;

    private Rectangle _collisionShape;
    private readonly Transform _transform;
    private float _width;
    private float _height;
    private bool _isMouseEntered;
    private bool _mouseEnteredThisFrame;
    private bool _mouseLeftThisFrame;

    public Collider(Transform transform, float width, float height)
    {
        _transform = transform;
        _width = width;
        _height = height;
        UpdateCollisionShape();
    }

    private void UpdateCollisionShape()
    {
        var pivotX = _width * _transform.Pivot.x * _transform.Scale;
        var pivotY = _height * _transform.Pivot.y * _transform.Scale;
        
        _collisionShape.X = _transform.ActualX - pivotX;
        _collisionShape.Y = _transform.ActualY - pivotY;
        _collisionShape.Width = _width * _transform.ActualScale;
        _collisionShape.Height = _height * _transform.ActualScale;
    }
    
    protected override void OnUpdate()
    {
        UpdateMouseState();
    }

    private void UpdateMouseState()
    {
        var isInside = IsMouseInside();

        _mouseEnteredThisFrame = isInside && !_isMouseEntered;
        _mouseLeftThisFrame = !isInside && _isMouseEntered;

        _isMouseEntered = isInside;
    }
    
    public bool HasMouseEntered()
    {
        return _mouseEnteredThisFrame;
    }

    public bool HasMouseExited()
    {
        return _mouseLeftThisFrame;
    }

    public bool IsMouseInside()
    {
        var point = new Point(Mouse.X, Mouse.Y);
        UpdateCollisionShape();
        return point.X >= _collisionShape.X &&
               point.X < _collisionShape.X + _collisionShape.Width &&
               point.Y >= _collisionShape.Y &&
               point.Y < _collisionShape.Y + _collisionShape.Height;
    }
    
    public bool IsPointInside(Point point)
    {
        UpdateCollisionShape();
        return point.X >= _collisionShape.X &&
               point.X < _collisionShape.X + _collisionShape.Width &&
               point.Y >= _collisionShape.Y &&
               point.Y < _collisionShape.Y + _collisionShape.Height;
    }

    public void UpdateWidth(float newWidth)
    {
        _width = newWidth;
        _collisionShape.Width = newWidth;
    }

    public void UpdateHeight(float newHeight)
    {
        _height = newHeight;
        _collisionShape.Height = newHeight;
    }
}