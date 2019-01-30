/// <summary>
/// Mode the game is currently in
/// </summary>
public enum eGameMode
{
    Running,
    MatchOver,
    Cutscene,
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
    Blocking,
    Knockdown,
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
}

public enum eCharacter
{
    CVoice,
    Bruno,
};

public enum eAttacks
{
    Punch,
    Bite,
    Dash,
    CCoop,
    BCoop
}


