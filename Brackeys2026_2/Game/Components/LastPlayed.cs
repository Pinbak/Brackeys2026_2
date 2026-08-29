using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Tweening;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class LastPlayed : Component
{
    private SpriteRenderer _card;
    private Texture _federalist;
    private Texture _loyalist;
    private Texture _agrarian;
    private Texture _technocrat;
    private Texture _outline;
    private static readonly Random Random = new();

    public LastPlayed(Texture outline, Texture federalist, Texture loyalist, Texture agrarian, Texture technocrat)
    {
        _federalist = federalist;
        _loyalist = loyalist;
        _agrarian = agrarian;
        _technocrat = technocrat;
        _outline = outline;
    }

    protected override void OnInitialise()
    {
        _card = new SpriteRenderer(_outline);
        GameObject.AddComponent(_card);
    }

    public void UpdateTexture(Faction type)
    {
        _card.ChangeTexture(type.Texture);
        GameObject.Transform.Angle = 0f;
        Tweener.RotateTo(GameObject.Transform, Random.Next(-5, 5), 0.2f, EaseFunctions.OutElastic);
    }
}