using Brackeys2026_2.Engine.GameObjects;

namespace Brackeys2026_2.Engine.Components;

public abstract class Component
{
    public bool IsOwned { get; set; }
    public bool IsEnabled { get; set; } = true;
    public GameObject GameObject { get; set; } = null!;
    protected virtual void OnInitialise(){}
    protected virtual void OnUpdate(){}

    protected virtual void OnDraw(){}

    internal void Initialise()
    {
        OnInitialise();
    }

    internal void Update()
    {
        OnUpdate();
    }

    internal void Draw()
    {
        OnDraw();
    }
    
    private readonly List<IDisposable> _ownedResources = new();

    protected T Own<T>(T resource) where T : IDisposable
    {
        _ownedResources.Add(resource);
        return resource;
    }

    internal void OnDestroy()
    {
        for (var i = _ownedResources.Count - 1; i >= 0; i--)
        {
            _ownedResources[i].Dispose();
        }

        _ownedResources.Clear();
    }
}