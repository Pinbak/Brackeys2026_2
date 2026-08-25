using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine;

public abstract partial class Core
{
    protected virtual void Setup(){}
    private bool IsInitialised { get; set; }
    internal bool IsRunning { get; private set; }
    internal Window? Window { get; set; }
    internal Renderer? Renderer { get; set; }
    internal Resources? Resources { get; set; }
    
    public void Run()
    {
        if (IsInitialised) return;
        IsInitialised = true;
        IsRunning = true;
        Launcher.Setup(this);
    }

    internal void MainInitialise()
    {
        GameObjectBase.Root = new Root();
        Window = new Window();
        Renderer = new Renderer();
        Resources = new Resources();
        InitialiseSystems();
        Setup();
        InitialiseEntities();
    }

    internal void MainLoop(Queue<SDL.Event> events)
    {
        if (!IsRunning) return;
        DrawBegin();
        while (events.Count > 0)
        {
            var @event = events.Dequeue(); // get the next event
            if (@event.type == SDL.EventType.Quit)
            {
                Quit();
                return;
            }
            TriggerSystemComponents(@event);
        }

        UpdateAll();
        DrawAll();
        DrawEnd();
    }

    public void Quit()
    {
        if (IsRunning)
        {
            IsRunning = false;
            RemoveSystemComponents();
            SDL.Quit();
            TTF.Quit();
        }
    }

    private static void RemoveSystemComponents()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnRemove();
        }
    }

    private static void TriggerSystemComponents(SDL.Event @event)
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnEvent(@event);
        }
    }
    
    private static void DrawAll()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnDraw();
        }
        
        foreach (var entity in GameObjectBase.GameObjects)
        {
            if (!entity.IsEnabled) continue;
            entity.OnDraw();
        }
    }
    
    private void DrawBegin()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnDrawBegin();
        }
    }

    private void DrawEnd()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnDrawEnd();
        }
    }

    private static void UpdateAll()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnUpdate();
        }

        foreach (var entity in GameObjectBase.GameObjects)
        {
            if (!entity.IsEnabled) continue;
            entity.OnUpdate();
        }
    }
    
    private static void InitialiseSystems()
    {
        foreach (var module in SystemComponent.AllSystemComponents)
        {
            module.OnInitialise();
        }
    }

    private static void InitialiseEntities()
    {
        foreach (var entity in GameObjectBase.GameObjects)
        {
            entity.OnInitialise();
        }
    }

}