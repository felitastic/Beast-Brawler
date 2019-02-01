/// <summary>
/// Mode the game is currently in
/// </summary>
public enum eGameMode
{
    MatchStart,
    Running,
    MatchOver,
    Pause,
    GameOver
};

/// <summary>
/// State the player is currently in
/// </summary>
public enum ePlayerState
{
    Ready,
    Attacking,
    InAir,
    InAirAttack,
    Blocking,
    Hurt,
    Dead
};

/// <summary>
/// Current stage
/// </summary>
public enum eStage
{
    None,
    Stage1,
    Stage2,
};

public enum eCharacter
{
    CVoice,
    Bruno,
};

public enum eAttacks
{
    None,
    Light,
    Heavy,
    Blockbreak,
    Jump
};



