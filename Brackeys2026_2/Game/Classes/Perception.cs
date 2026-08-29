namespace Brackeys2026_2.Game.Classes;

public class Perception
{
    // how the AI thinks its being perceived as
    private readonly Dictionary<Faction, float> _believedValues = new();
    private const float BeliefIncrease = 2f;

    public Perception()
    {
        foreach (var faction in Faction.All)
        {
            _believedValues.Add(faction, 0f);
        }
    }

    public void UpdateBeliefs(Faction lastPolicy)
    {
        _believedValues[lastPolicy] += BeliefIncrease;
    }

    public Faction GetBeliefOfMajority()
    {
        var youThinkTheMajorityThinkYouAre = _believedValues.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        return youThinkTheMajorityThinkYouAre;
    }

    public bool IsPerceivedAThreat(Dictionary<Faction, int> poll)
    {
        var youThinkTheMajorityAre = poll.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        var youThinkTheMajorityThinkYouAre = _believedValues.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        return youThinkTheMajorityThinkYouAre != youThinkTheMajorityAre;
    }
}