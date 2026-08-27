using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Entities;

public interface IDrawable
{
    void Draw(Transform position, Rectangle? frame);
}