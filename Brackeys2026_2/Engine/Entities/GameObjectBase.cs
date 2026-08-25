using System.Data;
using System.Reflection.Metadata.Ecma335;
using Brackeys2026_2.Engine.Behaviours;

namespace Brackeys2026_2.Engine.Entities;

public abstract class GameObjectBase
{
    public bool IsEnabled { get; set; } = true;
    public static Root Root { get; set; } = null!;
    public static List<GameObjectBase> GameObjects { get; private set; } = [];
    private List<UpdatableBehaviour> Components { get; set; } = [];

    public Transform Transform { get; set; }

    protected GameObjectBase(float x, float y)
    {
        GameObjects.Add(this);
        Transform = new Transform
        {
            X = x,
            Y = y
        };
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

    public virtual void OnInitialise()
    {
        foreach (var updatableBehaviour in Components)
        {
            updatableBehaviour.OnInitialise();
        }
    }

    public virtual void OnUpdate()
    {
        foreach (var updatableBehaviour in Components)
        {
            if (!updatableBehaviour.IsEnabled) continue;
            updatableBehaviour.OnUpdate();
        }
    }

    public virtual void OnDraw()
    {
        foreach (var drawableBehaviour in Components)
        {
            if (!drawableBehaviour.IsEnabled) continue;
            drawableBehaviour.OnDraw();
        }
    }
}