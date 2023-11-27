public abstract class PlayerProperty : PlayerAbility
{
    protected override void Awake()
    {
        base.Awake();
        PlayerPropertyInitialize();
    }
    public int adjustScenePromptTypeNumber { get; protected set; }
    public float adjustSceneMinPromptOdds { get; protected set; }
    public int adjustSceneMaxPromptsNumber { get; protected set; }
    public int adjustSceneMinPromptsNumber { get; protected set; }
    public float adjustScenePatienceMinus { get; protected set; }
    public int adjustSceneTotalStage => (int)(playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneTotalStageParameter);
    public float adjustSceneWrongInputPatincePenalty { get; protected set; }
    public int adjustSceneDestroyAllOrNotIndex { get; protected set; }
    //public ??? adjustSceneEffect { get; protected set; }
    public float adjustScenePromptEveryPopTime { get; protected set; }
    public float adjustScenePromptRespawnTime { get; protected set; }
    //public int adjustSceneBossSceneOrNot { get; protected set; }
    public float adjustSceneBossRushOdds { get; protected set; }
    public float adjustSceneBossRushTime { get; protected set; }
    public float adjustSceneBossHealth { get; protected set; }
    protected virtual void PlayerPropertyInitialize()
    {
        adjustScenePromptTypeNumber = 0;
        adjustSceneMinPromptOdds = 0f;
        adjustSceneMaxPromptsNumber = 0;
        adjustSceneMinPromptsNumber = 0;
        adjustScenePatienceMinus = 0f;
        //adjustSceneTotalStage = 0;
        adjustSceneWrongInputPatincePenalty = 0f;
        adjustSceneDestroyAllOrNotIndex = 0;
        adjustScenePromptEveryPopTime = 0f;
        adjustScenePromptRespawnTime = 0f;
        //adjustSceneBossSceneOrNot = 0;
        adjustSceneBossRushOdds = 0f;
        adjustSceneBossRushTime = 0f;
        adjustSceneBossHealth = 0f;
    }
    private void PlayerPropertyAdjustment()
    {
        if (playerInitial.initialCharacterInfo == null) return;
        adjustScenePromptTypeNumber = playerInitial.initialCharacterInfo.characterScenePromptTypeNumberParameter;
        adjustSceneMinPromptOdds = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneMinPromptOddsParameter;
        adjustSceneMaxPromptsNumber = (int)(playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneMaxPromptsNumberParameter);
        adjustSceneMinPromptsNumber = (int)(playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneMinPromptsNumberParameter);
        adjustScenePatienceMinus = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterScenePatienceMinusParameter;
        //adjustSceneTotalStage = (int)(playerLevel * playerCharacter.characterSceneTotalStageParameter);
        adjustSceneWrongInputPatincePenalty = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneWrongInputPatincePenaltyParameter;
        adjustSceneDestroyAllOrNotIndex = playerInitial.initialCharacterInfo.characterSceneDestroyAllOrNotParameter;
        adjustScenePromptEveryPopTime = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterScenePromptEveryPopTimeParameter;
        adjustScenePromptRespawnTime = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterScenePromptRespawnTimeParameter;
        //adjustSceneBossSceneOrNot = playerCharacter.characterSceneBossSceneOrNot;
        adjustSceneBossRushOdds = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneBossRushOddsParameter;
        adjustSceneBossRushTime = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneBossRushTimeParameter;
        adjustSceneBossHealth = playerInfo.playerLevel * playerInitial.initialCharacterInfo.characterSceneBossHealthParameter;
    }
    protected override void Update()
    {
        base.Update();
        PlayerPropertyAdjustment();
    }
}