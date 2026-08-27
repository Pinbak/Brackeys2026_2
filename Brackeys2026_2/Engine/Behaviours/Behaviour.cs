using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Engine.Behaviours;

public abstract class Behaviour
{
    public bool IsEnabled { get; set; } = true;
    public GameObject GameObject { get; set; } = null!;
    public virtual void OnInitialise(){}
}