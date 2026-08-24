using SDL3;

namespace Brackeys2026_2.Engine.System;

public abstract class SystemComponent
{
    internal static List<SystemComponent> AllSystemComponents { get; set; } = [];

    internal SystemComponent()
    {
        AllSystemComponents.Add(this);
    }
    
    internal virtual void OnInitialise() {}
    internal virtual void OnUpdate() {}
    internal virtual void OnDraw() {}
    internal virtual void OnRemove() {}
    internal virtual void OnEvent(SDL.Event @event) {}
}