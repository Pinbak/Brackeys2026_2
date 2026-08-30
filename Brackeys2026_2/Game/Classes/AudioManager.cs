using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Engine.Resources;

namespace Brackeys2026_2.Game.Classes;

public class AudioManager : Component
{
    private readonly Audio _soundTrack;
    private readonly Audio _crowdReaction;
    private readonly Audio _nextTurnWav;

    public AudioManager(Audio soundTrack, Audio crowdReaction, Audio nextTurnWav)
    {
        _soundTrack = soundTrack;
        _crowdReaction = crowdReaction;
        _nextTurnWav = nextTurnWav;
    }

    protected override void OnInitialise()
    {
        _soundTrack.Play();
    }

    public void OnDeath()
    {
        _crowdReaction.Play();
    }
    

    public void NewTurn()
    {
        _nextTurnWav.Play();
    }
    
}