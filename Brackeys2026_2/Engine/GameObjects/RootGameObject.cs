namespace Brackeys2026_2.Engine.GameObjects;

internal class RootGameObject : GameObjectBase
{
    public RootGameObject()
    {
        Transform = new Transform(this, 0, 0, null);
    }
} 