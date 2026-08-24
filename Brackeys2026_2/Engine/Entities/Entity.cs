namespace Brackeys2026_2.Engine.Entities;

public class Entity : EntityBase
{
    protected EntityBase Parent { get; set; }
    
    public Entity(EntityBase? parent) : base()
    {
        if (parent is null) Parent = Root;
        else Parent = parent;
    }
}