using System.Data;
using Brackeys2026_2.Engine.Behaviours;

namespace Brackeys2026_2.Engine.Entities;

public abstract class EntityBase
{
    public bool IsEnabled { get; private set; } = true;
    public static Root Root { get; set; } = null!;
    public static List<EntityBase> Entities { get; private set; } = [];
    protected List<UpdatableBehaviour> Updatables { get; private set; } = [];
    protected List<DrawableBehaviour> Drawables { get; private set; } = [];

    public Transform Transform { get; set; }

    protected EntityBase()
    {
        Entities.Add(this);
    }

    public virtual void OnInitialise()
    {
        foreach (var updatableBehaviour in Updatables)
        {
            updatableBehaviour.OnInitialise();
        }
        
        foreach (var drawableBehaviour in Drawables)
        {
            drawableBehaviour.OnInitialise();
        }
    }

    public virtual void OnUpdate()
    {
        foreach (var updatableBehaviour in Updatables)
        {
            updatableBehaviour.OnUpdate();
        }
    }

    public virtual void OnDraw()
    {
        foreach (var drawableBehaviour in Drawables)
        {
            drawableBehaviour.OnDraw();
        }
    }
}