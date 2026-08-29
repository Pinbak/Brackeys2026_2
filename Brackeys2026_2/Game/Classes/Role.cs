using Brackeys2026_2.Game.Classes.Ministers;
using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public abstract class Role
{
    public RoleType RoleType { get; set; }
    protected GameManager GameManager { get; set; }
    
    public string Name { get; }

    public abstract void PerformAction(Minister thisPlayer);
    
    protected Role(RoleType roleType, string name, GameManager gameManager)
    {
        RoleType = roleType;
        GameManager = gameManager;
        Name = name;
    }
}