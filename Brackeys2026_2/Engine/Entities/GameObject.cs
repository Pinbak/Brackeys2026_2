using Brackeys2026_2.Engine.Behaviours;

namespace Brackeys2026_2.Engine.Entities;

public sealed class GameObject : GameObjectBase
{
    public GameObjectBase Parent { get; set; }
    private List<UpdatableBehaviour> Components { get; set; } = [];
    
    public GameObject(float relativeX, float relativeY, GameObjectBase? parent)
    {
        if (parent is null) Parent = RootGameObject;
        else Parent = parent;
        Transform = new Transform(this, relativeX, relativeY, Parent.Transform);
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
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.OnInitialise();
        }

        base.OnInitialise();
    }

    public override void OnUpdate()
    {
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.OnUpdate();
        }

        base.OnUpdate();
    }

    public override void OnDraw()
    {
        foreach (var drawableBehaviour in Components)
        {
            if (!drawableBehaviour.IsEnabled) continue;
            drawableBehaviour.OnDraw();
        }

        base.OnDraw();
    }

    ~GameObject()
    {
        Parent.Children.Remove(this);
    }
}