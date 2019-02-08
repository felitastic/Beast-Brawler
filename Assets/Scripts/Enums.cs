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
    JumpTakeOff,
    InAir,
    InAirAttack,
    DiveAttack,
    Blocking,
    Knockeddown,
    Hurt,
    Dead
};

public enum eStun
{
    normal,
    blockbroken,
    blocking,
    jumped
}

/// <summary>
/// Current stage
/// </summary>
public enum eStage
{
    None,
    Stage1,
    Stage2,
};

public enum eAttacks
{
    None,
    Light,
    Heavy,
    Blockbreak,
    Jump,
    Dive
};



