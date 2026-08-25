using Brackeys2026_2.Engine.Behaviours;

namespace Brackeys2026_2.Engine.Entities;

public sealed class GameObject : GameObjectBase
{
    public GameObjectBase Parent { get; set; }
    public Transform Transform { get; set; }
    private List<UpdatableBehaviour> Components { get; set; } = [];
    
    public GameObject(float x, float y, GameObjectBase? parent)
    {
        Transform = new Transform(this, x, y);
        if (parent is null) Parent = RootGameObject;
        else Parent = parent;
        Parent.Children.Add(this);
    }
    
    public void AddBehaviour(Behaviour behaviour)
    {
        behaviour.GameObject = this;
        if (behaviour is UpdatableBehaviour updatableBehaviour)
        {
            Components.Add(updatableBehaviour);
        }
    }

    public void RemoveBehaviour(Behaviour behaviour)
    {
        if (behaviour is UpdatableBehaviour updatableBehaviour)
        {
            Components.Remove(updatableBehaviour);
        }
    }

    // intentionally recursive
    public override void OnInitialise()
    {
        base.OnInitialise();
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.OnInitialise();
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.OnUpdate();
        }
    }

    public override void OnDraw()
    {
        base.OnDraw();
        foreach (var drawableBehaviour in Components)
        {
            if (!drawableBehaviour.IsEnabled) continue;
            drawableBehaviour.OnDraw();
        }
    }

    ~GameObject()
    {
        Parent.Children.Remove(this);
    }
}