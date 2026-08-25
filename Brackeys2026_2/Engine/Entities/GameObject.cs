namespace Brackeys2026_2.Engine.Entities;

public class GameObject : GameObjectBase
{
    protected GameObjectBase Parent { get; set; }
    
    public GameObject(float x, float y, GameObjectBase? parent) : base(x, y)
    {
        if (parent is null) Parent = RootGameObject;
        else Parent = parent;
    }
}