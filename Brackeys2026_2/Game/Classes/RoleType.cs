namespace Brackeys2026_2.Game.Classes;

public enum RoleType
{
    President, // can reshuffle cabinet, has final vote
    PresidentsAide, // always exposed presidents hand
    MinisterOfNationalSecurity, // can kill people
    SupremeCommander, // stage a coup against the president
    SecretaryOfImmigration, // chooses new minister on death
    MinisterOfCommunication,  // change someone's ideology
    MinisterOfInterior, // expose actual ideology of member
    SecretaryForPropaganda, // convince other ministers of someone elses
}