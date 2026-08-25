using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine;

public abstract partial class Core
{
    protected virtual void Setup(){}
    private bool IsInitialised { get; set; }
    internal bool IsRunning { get; private set; }
    
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
        Window.CreateWindow();
        Renderer.CreateGraphics();
        Resources.GatherResources();
        Window.SetPresentationLetterbox();
        Setup();
        InitGameObjects();
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
            // TriggerSystemComponents(@event);
        }

        UpdateGameObjects();
        DrawGameObjects();
        DrawEnd();
    }

    public void Quit()
    {
        if (IsRunning)
        {
            IsRunning = false;
            Window.CleanUpResources();
            Renderer.CleanUpResources();
            SDL.Quit();
            TTF.Quit();
        }
    }
    
    private static void DrawBegin()
    {
        Renderer.DrawBegin();
    }

    private static void DrawEnd()
    {
        Renderer.DrawEnd();
    }

    private static void UpdateGameObjects()
    {
        foreach (var entity in GameObjectBase.GameObjects)
        {
            if (!entity.IsEnabled) continue;
            entity.OnUpdate();
        }
    }

    private static void InitGameObjects()
    {
        foreach (var entity in GameObjectBase.GameObjects)
        {
            entity.OnInitialise();
        }
    }
    
    private static void DrawGameObjects()
    {
        foreach (var entity in GameObjectBase.GameObjects)
        {
            if (!entity.IsEnabled) continue;
            entity.OnDraw();
        }
    }

}