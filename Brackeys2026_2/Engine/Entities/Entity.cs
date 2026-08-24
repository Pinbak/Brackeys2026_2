namespace Brackeys2026_2.Engine.Entities;

public abstract class Entity
{
    public static List<Entity> Entities { get; private set; } = [];
    
    public Transform Transform { get; set; }

    protected Entity()
    {
        Entities.Add(this);
    }
    
    public virtual void OnInitialise(){}
    public virtual void OnUpdate(){}
    public virtual void OnDraw(){}
}