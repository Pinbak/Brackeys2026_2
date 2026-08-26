namespace Brackeys2026_2.Engine.Entities;

public abstract class GameObjectBase
{
    public bool IsEnabled { get; set; } = true;
    internal static RootGameObject RootGameObject { get; set; } = null!;
    public List<GameObject> Children { get; private set; } = [];
    
    public Transform Transform { get; internal init; } = null!;

    public virtual void OnInitialise()
    {
        foreach (var gameObjectBase in Children)
        {
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.OnInitialise();
        }
    }

    public virtual void OnUpdate()
    {
        foreach (var gameObjectBase in Children)
        {
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.Transform.Update();
            gameObjectBase.OnUpdate();
        }
    }

    public virtual void OnDraw()
    {
        foreach (var gameObjectBase in Children)
        {
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.OnDraw();
        }
        
    }
}