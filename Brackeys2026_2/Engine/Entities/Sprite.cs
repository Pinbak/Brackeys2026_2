namespace Brackeys2026_2.Engine.Entities;

public class Sprite : Entity
{
    private Texture _texture; 
    
    public Sprite(string texturePath, float x = 0f, float y = 0f, EntityBase? parent = null) : base(parent)
    {
        _texture = new Texture(texturePath, x, y);
    }

    public override void OnInitialise()
    {
        base.OnInitialise();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnDraw()
    {
        _texture.Draw();
        base.OnDraw();
    }
}