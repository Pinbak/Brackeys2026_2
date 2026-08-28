using Brackeys2026_2.Game.Components;

namespace Brackeys2026_2.Game.Classes;

public class President(RoleType roleType, string name) : Role(roleType, name)
{
    
    
    public override void PerformAction(Minister thisPlayer)
    {
        base.PerformAction(thisPlayer);
        // reshuffle cabinet


        
        GameManager.FinishTurn(Faction.All[0]);
    }
}

public class PresidentsAide : Role
{
    public PresidentsAide(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class MinisterOfNationalSecurity : Role
{
    public MinisterOfNationalSecurity(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class SupremeCommander : Role
{
    public SupremeCommander(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class SecretaryOfImmigration : Role
{
    public SecretaryOfImmigration(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class MinisterOfCommunication : Role
{
    public MinisterOfCommunication(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class MinisterOfInterior : Role
{
    public MinisterOfInterior(RoleType roleType, string name) : base(roleType, name)
    {
    }
}

public class SecretaryForPropaganda : Role
{
    public SecretaryForPropaganda(RoleType roleType, string name) : base(roleType, name)
    {
    }
}