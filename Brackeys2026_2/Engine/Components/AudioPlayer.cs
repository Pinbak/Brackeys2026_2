using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.Components;

public class AudioPlayer : Component
{
    private readonly Audio _audio;
    
    public AudioPlayer(Audio audio)
    {
        _audio = audio;
    }

    protected override void OnUpdate()
    {
        if (Mouse.HasPressed(Mouse.Button.Left))
        {
            _audio.Play();
        }
    }
}