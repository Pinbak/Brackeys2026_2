using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.System;
using Brackeys2026_2.Engine.System.Inputs;
using Brackeys2026_2.Engine.System.Tweening;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class PlayerCard : Component
{
    public Action<Faction> CardSelected { get; set; }
    public Action<Faction> CardDeselected { get; set; }
    public bool IsSelected { get; set; }

    private float _delay;
    private Collider _collider;
    private float _time;
    private Faction _faction;
    
    public PlayerCard(Collider collider, Faction faction, float delay)
    {
        _delay = delay;
        _collider = collider;
        _faction = faction;
    }

    protected override void OnInitialise()
    {
        Tweener.MoveYTo(GameObject.Transform, 1000, 0.5f, EaseFunctions.OutQuart, _delay);
    }

    protected override void OnUpdate()
    {
        if (_collider.HasMouseEntered())
        {
            if (!IsSelected)
                Tweener.MoveYTo(GameObject.Transform, 950, 0.2f, EaseFunctions.OutQuart);
        }
        else if (_collider.HasMouseExited())
        {
            if (!IsSelected)
                Tweener.MoveYTo(GameObject.Transform, 1000, 0.2f, EaseFunctions.OutQuart);
        }
        
        if (Mouse.HasPressed(Mouse.Button.Left))
        {
            if (_collider.IsMouseInside())
            {
                if (!IsSelected)
                    SelectCard();
                else
                    DeselectCard();
            }
        }
        
        if (Mouse.HasPressed(Mouse.Button.Right))
            DeselectCard();

        if (IsSelected)
        {
            _time += Time.DeltaTime;
            GameObject.Transform.Angle = float.Sin(_time * 2) * 2f;
        }
            
    }

    private void SelectCard()
    {
        CardSelected?.Invoke(_faction);
        Tweener.MoveYTo(GameObject.Transform, 900, 0.2f, EaseFunctions.OutQuart);
        IsSelected = true;
    }

    private void DeselectCard()
    {
        CardDeselected?.Invoke(_faction);
        Tweener.MoveYTo(GameObject.Transform, 1000, 0.2f, EaseFunctions.OutQuart);
        IsSelected = false;
        GameObject.Transform.Angle = 0f;
    }

    public void RemoveTween(float delay)
    {
        if (IsSelected)
        {
            Tweener.MoveXTo(GameObject.Transform, 650, 0.5f, EaseFunctions.OutQuart, delay, true);
            Tweener.MoveYTo(GameObject.Transform, 190, 0.5f, EaseFunctions.OutQuart, delay, true);
            Tweener.ScaleTo(GameObject.Transform, 0.1f, 0.5f, EaseFunctions.OutQuart, delay, true);
        }
        else
        {
            Tweener.MoveYTo(GameObject.Transform, 1400, 0.5f, EaseFunctions.OutQuart, 0f, true);
        }
    }
}