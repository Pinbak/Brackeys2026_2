using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Engine.Behaviours;

public abstract class Behaviour
{
    public bool IsEnabled { get; set; } = true;
    public GameObject GameObject { get; set; } = null!;
    protected virtual void OnInitialise(){}
    internal void Initialise()
    {
        OnInitialise();
    }
}