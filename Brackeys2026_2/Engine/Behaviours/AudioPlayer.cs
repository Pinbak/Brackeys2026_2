using Brackeys2026_2.Engine.Behaviours.Experiments;
using Brackeys2026_2.Engine.Resources;
using Brackeys2026_2.Engine.System.Inputs;

namespace Brackeys2026_2.Engine.Behaviours;

public class AudioPlayer : UpdatableBehaviour
{
    private readonly Audio _audio;
    
    public AudioPlayer(Audio audio)
    {
        _audio = audio;
    }

    public override void OnUpdate()
    {
        // if (Mouse.IsPressed(Mouse.MouseButton.Left))
        // {
        //     _audio.Play();
        // }
    }
}