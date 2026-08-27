using Brackeys2026_2.Engine.Behaviours;

namespace Brackeys2026_2.Engine.GameObjects;

public sealed class GameObject : GameObjectBase
{
    public GameObjectBase Parent { get; set; }
    private List<UpdatableBehaviour> Components { get; set; } = [];
    
    public GameObject(float relativeX, float relativeY, GameObjectBase? parent, Behaviour[]? behaviours = null)
    {
        if (parent is null) Parent = RootGameObject;
        else Parent = parent;
        Transform = new Transform(this, relativeX, relativeY, Parent.Transform);
        Parent.Children.Add(this);
        
        if (behaviours is null) return;
        foreach (var behaviour in behaviours) AddBehaviour(behaviour);
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
            updatableBehaviour.Initialise();
        }

        base.OnInitialise();
    }

    public override void OnUpdate()
    {
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.Update();
        }

        base.OnUpdate();
    }

    public override void OnDraw()
    {
        foreach (var drawableBehaviour in Components)
        {
            if (!drawableBehaviour.IsEnabled) continue;
            drawableBehaviour.Draw();
        }

        base.OnDraw();
    }

    ~GameObject()
    {
        Parent.Children.Remove(this);
    }
}