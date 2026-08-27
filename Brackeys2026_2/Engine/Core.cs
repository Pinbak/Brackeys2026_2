using Brackeys2026_2.Engine.Entities;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Engine;

public abstract class Core
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
        Time.Setup();
        GameObjectBase.RootGameObject = new RootGameObject();
        Window.CreateWindow();
        Graphics.CreateGraphics();
        Resource.Setup();
        Window.SetPresentationLetterbox();
        
        Setup();
        InitGameObjects();
    }

    internal void MainLoop(Queue<SDL.Event> events)
    {
        if (!IsRunning) return;
        Time.Update();
        DrawBegin();
        while (events.Count > 0)
        {
            var @event = events.Dequeue(); // get the next event
            if (@event.Type == SDL.EventType.Quit)
            {
                Quit();
                return;
            }
            Mouse.Listen(@event);
        }
        Tweener.Process();
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
            Graphics.CleanUpResources();
            SDL.QuitSdl();
            TTF.Quit();
        }
    }
    
    private static void DrawBegin()
    {
        Graphics.DrawBegin();
    }

    private static void DrawEnd()
    {
        Graphics.DrawEnd();
    }

    private static void InitGameObjects()
    {
        GameObjectBase.RootGameObject.OnInitialise();
    }

    private static void UpdateGameObjects()
    {
        GameObjectBase.RootGameObject.OnUpdate();
    }

    private static void DrawGameObjects()
    {
        GameObjectBase.RootGameObject.OnDraw();
    }

}