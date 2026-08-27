namespace Brackeys2026_2.Engine.GameObjects;

public abstract class GameObjectBase
{
    public bool IsEnabled { get; set; } = true;
    internal static RootGameObject RootGameObject { get; set; } = null!;
    public List<GameObject> Children { get; private set; } = [];
    
    public Transform Transform { get; internal init; } = null!;

    public virtual void OnInitialise()
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            var gameObjectBase = Children[i];
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.OnInitialise();
        }
    }

    public virtual void OnUpdate()
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            var gameObjectBase = Children[i];
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.Transform.Update();
            gameObjectBase.OnUpdate();
        }
    }

    public virtual void OnDraw()
    {
        for (var i = Children.Count - 1; i >= 0; i--)
        {
            var gameObjectBase = Children[i];
            if (!gameObjectBase.IsEnabled) continue;
            gameObjectBase.OnDraw();
        }
    }
}