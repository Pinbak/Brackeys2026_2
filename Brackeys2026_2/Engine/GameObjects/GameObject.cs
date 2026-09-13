using Brackeys2026_2.Engine.Components;

namespace Brackeys2026_2.Engine.GameObjects;

public sealed class GameObject : GameObjectBase
{
    public GameObjectBase Parent { get; set; }
    private List<Component> Components { get; set; } = [];
    private bool _isDisposed;
    
    public GameObject(float relativeX, float relativeY, GameObjectBase? parent, Component[]? components = null)
    {
        if (parent is null) Parent = RootGameObject;
        else Parent = parent;
        Transform = new Transform(this, relativeX, relativeY, Parent.Transform);
        Parent.Children.Insert(0, this);
        
        if (components is null) return;
        foreach (var behaviour in components) AddComponent(behaviour);
    }
    
    ~GameObject()
    {
        Release();
    }
    
    public void Destroy()
    {
        Release();
        GC.SuppressFinalize(this);
    }

    private void Release()
    {
        if (_isDisposed) return;
        foreach (var component in Components) component.OnDestroy();
        Components.Clear();
        Children.Clear();
        Parent.Children.Remove(this);
        _isDisposed = true;
    }
    
    public void AddComponent(Component component)
    {
        if (component.IsOwned)
        {
            throw new Exception("Cannot assign the same component to multiple game objects");
        }
        component.GameObject = this;
        component.IsOwned = true;
        Components.Insert(0, component);
        component.Initialise();
        // if (behaviour is Component updatableBehaviour)
        // {
        //     
        // }
    }

    public void RemoveComponent(Component component)
    {
        component.OnDestroy();
        Components.Remove(component);
    }

    // intentionally recursive
    public override void OnUpdate()
    {
        for (var i = Components.Count - 1; i >= 0; i--)
        {
            var updatableBehaviour = Components[i];
            if (!updatableBehaviour.IsEnabled || _isDisposed) continue;
            updatableBehaviour.Update();
        }

        base.OnUpdate();
    }

    public override void OnDraw()
    {
        for (var i = Components.Count - 1; i >= 0; i--)
        {
            var drawableBehaviour = Components[i];
            if (!drawableBehaviour.IsEnabled || _isDisposed) continue;
            drawableBehaviour.Draw();
        }

        base.OnDraw();
    }


    public void BringToFront()
    {
        Parent.Children.Remove(this);
        Parent.Children.Insert(0, this);
    }
}