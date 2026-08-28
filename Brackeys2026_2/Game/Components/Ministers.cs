using Brackeys2026_2.Engine.Components;
using Brackeys2026_2.Game.Classes;

namespace Brackeys2026_2.Game.Components;

public class Ministers : Component
{
    public List<Minister> CurrentMinisters { get; set; } = [];
    private const int NumberOfMinisters = 11;

    protected override void OnInitialise()
    {
        for (var i = 0; i < NumberOfMinisters; i++)
        {
            CurrentMinisters.Add(new Minister((RoleType)i));
        }
        
    }
}