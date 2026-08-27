namespace Brackeys2026_2.Engine.Behaviours;

public class UpdatableBehaviour : Behaviour
{
    protected virtual void OnUpdate(){}
    protected virtual void OnDraw(){}

    internal void Update()
    {
        OnUpdate();
    }

    internal void Draw()
    {
        OnDraw();
    }
}