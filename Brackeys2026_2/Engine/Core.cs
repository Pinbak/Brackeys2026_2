using Brackeys2026_2.Engine.Behaviours.Experiments;
using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;

namespace Brackeys2026_2.Engine;

public abstract class Core
{
    protected virtual void Layout(){}
    private bool IsInitialised { get; set; }
    internal bool IsRunning { get; private set; }
    
    public void Run()
    {
        if (IsInitialised) return;
        IsInitialised = true;
        IsRunning = true;
        Setup.Run(this);
    }

    internal void MainInitialise()
    {
        Time.Setup();
        GameObjectBase.RootGameObject = new RootGameObject();
        WindowManager.CreateWindow();
        Renderer.Setup();
        Mixer.Setup();
        Resource.ImportAllEmbedded();
        WindowManager.SetPresentationLetterbox();
        
        Layout(); // run the game code
        GameObjectBase.RootGameObject.OnInitialise(); // inits all game objects as is recursive
    }

    internal void MainLoop(Queue<SDL.Event> events)
    {
        if (!IsRunning) return;
        Time.Update();
        Renderer.DrawBegin();
        Mouse.ClearEvents();
        while (events.Count > 0)
        {
            var @event = events.Dequeue(); // get the next event
            if (@event.Type == SDL.EventType.Quit)
            {
                Quit();
                return;
            }
            Mouse.ProcessEvent(@event);
        }
        Tweener.Process();
        GameObjectBase.RootGameObject.OnUpdate(); // updates all game objects as is recursive
        GameObjectBase.RootGameObject.OnDraw(); // draw all game objects as is recursive
        Renderer.DrawEnd();
    }

    public void Quit()
    {
        if (IsRunning)
        {
            IsRunning = false;
            WindowManager.CleanUpResources();
            Renderer.CleanUpResources();
            Mixer.CleanupResources();
            SDL.QuitSdl();
            TTF.QuitTtf();
        }
    }

}