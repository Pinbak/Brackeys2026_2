using Brackeys2026_2.Engine.GameObjects;
using Brackeys2026_2.Engine.System;

namespace Brackeys2026_2.Engine.Resources;

public interface IPlayable
{
    void Play();
    void Pause();
    void Resume();
    void Stop();
}