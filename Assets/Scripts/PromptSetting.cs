using UnityEngine;
public abstract class PromptSetting : MonoBehaviour, IScenePriority, IUpdateImpact
{
    /// <summary>
    /// 跟扣耐性有關的設定
    /// </summary>
    public const int PromptDefaultInt = 3600;
    public const int PromptLength = 4;
    public const float ConstantZero = 0f;
    [Tooltip("僅供開發者填入")]
    [SerializeField] protected SceneParameter_SO sceneDefaultParameter;
    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
        if (sceneDefaultParameter.sceneSprite == null) UIManager.instance.backgroundSprite.enabled = false;
        else UIManager.instance.backgroundSprite.sprite = sceneDefaultParameter.sceneSprite;
        if (sceneDefaultParameter.sceneBossSprite == null) UIManager.instance.bossSprite.enabled = false;
        else UIManager.instance.bossSprite.sprite = sceneDefaultParameter.sceneBossSprite;
    }
    protected AudioClip currentSceneResultSound
    {
        get
        {
            if (sceneDefaultParameter.sceneResultSound.Length == 0) return null;
            return sceneDefaultParameter.sceneResultSound[Random.Range(0, sceneDefaultParameter.sceneResultSound.Length)];
        }
    }
    protected AudioClip currentSceneBGM => sceneDefaultParameter.sceneBGM;
    protected int currentScenePromptAmount => sceneDefaultParameter.scenePrompts.promptsItem.Count;
    #region 目前場景參數
    public float inputTime { get; protected set; }
    public int currentScenePromptTypeNumber { get; protected set; }
    public float currentSceneMinPromptOdds { get; protected set; }
    public int currentSceneMaxPromptsNumber { get; protected set; }
    public int currentSceneMinPromptsNumber { get; protected set; }
    public float currentScenePatienceMinus { get; protected set; }
    public int currentSceneTotalStage { get; protected set; }
    public float currentSceneWrongInputPatincePenalty { get; protected set; }
    public bool currentSceneDestroyAllOrNot { get; protected set; }
    //public SO currentSceneEffect { get; protected set; } 
    public float currentScenePromptEveryPopTime { get; protected set; }
    public float currentScenePromptRespawnTime { get; protected set; }
    public bool currentSceneBossSceneOrNot { get; protected set; }
    public float currentSceneBossRushOdds { get; protected set; }
    public float currentSceneBossRushTime { get; protected set; }
    public float currentSceneBossHealth { get; protected set; }
    #endregion 目前場景參數
    /// <summary>
    /// 場景參數改變
    /// </summary>
    /// <param name="player"></param>
    protected abstract void ParameterSetting(Player player, bool isInitial);
    protected abstract void PlayerImpactOnInput(Player player);
    #region currentScene優先順序
    public int scenePromptTypeNumberPriority(Player player)
    {
        //玩家角色的能力
        if (player.adjustScenePromptTypeNumber != 0)
        {
            return player.adjustScenePromptTypeNumber;
        }
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasPromptTypeNumberImpactOrNot) return sceneDefaultParameter.scenePromptTypeNumber;
        //玩家有裝備、裝備未磨損、裝備有能力
        else return player.playerMainEquipmentImpact_PromptTypeNumber;
    }
    public float sceneMinPromptOddsPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasMinPromptOddsImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.sceneMinPromptOdds + player.adjustSceneMinPromptOdds;
            if (valid_PlayerProperty < 0f) return 0f;
            else if (valid_PlayerProperty > 1f) return 1f;
            else return valid_PlayerProperty;
        }
        //玩家有自身能力、有裝備、裝備未磨損、裝備有能力
        else
        {
            if (player.playerMainEquipmentImpact_MinPromptOddsBias != 0) return player.playerMainEquipmentImpact_MinPromptOddsBias;
            float valid_PlayerAbility = sceneDefaultParameter.sceneMinPromptOdds + player.playerMainEquipmentImpact_MinPromptOddsDelta + player.adjustSceneMinPromptOdds;
            if (valid_PlayerAbility < 0f) return 0f;
            else if (valid_PlayerAbility > 1f) return 1f;
            else return valid_PlayerAbility;
        }
    }
    public int sceneMaxPromptsNumberPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasMaxPromptNumberImpactOrNot)
        {
            int valid_PlayerProperty = sceneDefaultParameter.sceneMaxPromptsNumber + player.adjustSceneMaxPromptsNumber;
            if (valid_PlayerProperty < 1) return 1;
            else return valid_PlayerProperty;
        }
        //玩家有裝備、裝備未磨損、裝備有能力
        else
        {
            if (player.playerMainEquipmentImpact_MaxPromptNumberBias != 0) return player.playerMainEquipmentImpact_MaxPromptNumberBias;
            int valid_PlayerAbility = sceneDefaultParameter.sceneMaxPromptsNumber + player.playerMainEquipmentImpact_MaxPromptNumberDelta + player.adjustSceneMaxPromptsNumber;
            if (valid_PlayerAbility < 1) return 1;
            else return valid_PlayerAbility;
        }
    }
    public int sceneMinPromptsNumberPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasMinPromptNumberImpactOrNot)
        {
            int valid_PlayerProperty = sceneDefaultParameter.sceneMinPromptsNumber + player.adjustSceneMinPromptsNumber;
            if (valid_PlayerProperty < 1) return 1;
            else return valid_PlayerProperty;
        }
        //玩家有裝備、裝備未磨損、裝備有能力
        else
        {
            if (player.playerMainEquipmentImpact_MinPromptNumberBias != 0) return player.playerMainEquipmentImpact_MinPromptNumberBias;
            int valid_Ability = sceneDefaultParameter.sceneMinPromptsNumber + player.playerMainEquipmentImpact_MinPromptNumberDelta + player.adjustSceneMinPromptsNumber;
            if (valid_Ability < 1) return 1;
            else return valid_Ability;
        }
    }
    public float scenePatienceMinusPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasPatienceMinusImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.scenePatienceMinus + player.adjustScenePatienceMinus;
            if (valid_PlayerProperty < 1f) return 1f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_PatienceMinusBias != 0) return player.playerMainEquipmentImpact_PatienceMinusBias;
            float valid_PlayerAbility = sceneDefaultParameter.scenePatienceMinus + player.playerMainEquipmentImpact_PatienceMinusDelta + player.adjustScenePatienceMinus;
            if (valid_PlayerAbility < 1f) return 1f;
            else return valid_PlayerAbility;
        }
    }
    public int sceneTotalStagePriority(Player player, bool isInitial)
    {
        if (isInitial)
        {
            //玩家沒有裝備
            //或玩家裝備已磨損
            //或玩家的裝備沒有能力
            if (!player.isPlayerMainEquipmentEquipped ||
                !player.playerMainEquipmentInWellCondition ||
                !player.playerMainEquipmentHasTotalStageImpactOrNot)
            {
                int valid_PlayerProperty = sceneDefaultParameter.sceneTotalStage + player.adjustSceneTotalStage;
                if (valid_PlayerProperty < 0) return 0;
                else return valid_PlayerProperty;
            }
            //玩家有裝備、裝備未磨損、裝備有能力
            //在Start
            //(目前不實裝)能同時有兩個能力
            //if(player.playerMainEquipmentImpact_TotalStageBias != 0 && player.playerMainEquipmentImpact_TotalStageDelta != 0)

            //能力是把TotalStage變成固定數量
            if (player.playerMainEquipmentImpact_TotalStageBias != 0) return player.playerMainEquipmentImpact_TotalStageBias;
            //能力是加減TotalStage的數量           
            int valid_PlayerAbility = sceneDefaultParameter.sceneTotalStage + player.playerMainEquipmentImpact_TotalStageDelta + player.adjustSceneTotalStage;
            if (valid_PlayerAbility < 0) return 0;
            else return valid_PlayerAbility;
        }
        //在Update
        return currentSceneTotalStage;
    }
    public float sceneWrongInputPatincePenaltyPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasWrongInputPatincePenaltyImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.sceneWrongInputPatincePenalty + player.adjustSceneWrongInputPatincePenalty;
            if (valid_PlayerProperty > 1f) return 0.999f;
            else if (valid_PlayerProperty < 0f) return 0.001f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_WrongInputPatincePenaltyBias != 0) return player.playerMainEquipmentImpact_WrongInputPatincePenaltyBias;
            float valid_PlayerAbility = sceneDefaultParameter.sceneWrongInputPatincePenalty + player.playerMainEquipmentImpact_WrongInputPatincePenaltyDelta + player.adjustSceneWrongInputPatincePenalty;
            if (valid_PlayerAbility > 1f) return 0.999f;
            else if (valid_PlayerAbility < 0f) return 0.001f;
            else return valid_PlayerAbility;
        }
    }
    public bool sceneDestroyAllOrNotPriority(Player player)
    {
        //玩家角色的能力
        if (player.adjustSceneDestroyAllOrNotIndex != 0)
        {
            if (player.adjustSceneDestroyAllOrNotIndex == 1) return true;
            if (player.adjustSceneDestroyAllOrNotIndex == 2) return false;
        }
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasDestroyAllOrNotImpactOrNot) return sceneDefaultParameter.sceneDestroyAllOrNot;
        //玩家有裝備、裝備未磨損、裝備有能力
        else
        {
            //裝備能力是強制摧毀全部
            if (player.playerMainEquipmentImpact_DestroyAll) return player.playerMainEquipmentImpact_DestroyAll;
            //裝備能力是強制摧毀一個
            else return player.playerMainEquipmentImpact_NotDestroyAll;
        }
    }
    public float scenePromptEveryPopTimePriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasPromptEveryPopTimeImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.scenePromptEveryPopTime + player.adjustScenePromptEveryPopTime;
            if (valid_PlayerProperty < 0f) return 0.001f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_PromptEveryPopTimeBias != 0) return player.playerMainEquipmentImpact_PromptEveryPopTimeBias;
            float valid_PlayerAbility = sceneDefaultParameter.scenePromptEveryPopTime + player.playerMainEquipmentImpact_PromptEveryPopTimeDelta + player.adjustScenePromptEveryPopTime;
            if (valid_PlayerAbility < 0f) return 0.001f;
            else return valid_PlayerAbility;
        }
    }
    public float scenePromptRespawnTimePriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasPromptRespawnTimeImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.scenePromptRespawnTime + player.adjustScenePromptRespawnTime;
            if (valid_PlayerProperty < 0f) return 0.001f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_PromptRespawnTimeBias != 0) return player.playerMainEquipmentImpact_PromptRespawnTimeBias;
            float valid_PlayerAbility = sceneDefaultParameter.scenePromptRespawnTime + player.playerMainEquipmentImpact_PromptRespawnTimeDelta + player.adjustScenePromptRespawnTime;
            if (valid_PlayerAbility < 0f) return 0.001f;
            else return valid_PlayerAbility;
        }
    }
    public bool sceneBossSceneOrNotPriority()
    {
        return sceneDefaultParameter.sceneBossSceneOrNot;
    }
    public float sceneBossRushOddsPriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasBossRushOddsImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.sceneBossRushOdds + player.adjustSceneBossRushOdds;
            if (valid_PlayerProperty < 0f) return 0f;
            else if (valid_PlayerProperty > 1f) return 1f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_BossRushOddsBias != 0) return player.playerMainEquipmentImpact_BossRushOddsBias;
            float valid_PlayerAbility = sceneDefaultParameter.sceneBossRushOdds + player.playerMainEquipmentImpact_BossRushOddsDelta + player.adjustSceneBossRushOdds;
            if (valid_PlayerAbility < 0f) return 0f;
            else if (valid_PlayerAbility > 1f) return 1f;
            else return valid_PlayerAbility;
        }
    }
    public float sceneBossRushTimePriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasBossRushTimeImpactOrNot)
        {
            float valid_PlayerProperty = sceneDefaultParameter.sceneBossRushTime + player.adjustSceneBossRushTime;
            if (valid_PlayerProperty < 0f) return 0f;
            else return valid_PlayerProperty;
        }
        else
        {
            if (player.playerMainEquipmentImpact_BossRushTimeBias != 0) return player.playerMainEquipmentImpact_BossRushTimeBias;
            float valid_PlayerAbility = sceneDefaultParameter.sceneBossRushTime + player.playerMainEquipmentImpact_BossRushTimeDelta + player.adjustSceneBossRushTime;
            if (valid_PlayerAbility < 0f) return 0f;
            else return valid_PlayerAbility;
        }
    }
    public float sceneBossHealthPriority(Player player, bool isInitial)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (isInitial)
        {
            if (!player.isPlayerMainEquipmentEquipped ||
                !player.playerMainEquipmentInWellCondition ||
                !player.playerMainEquipmentHasBossHealthImpactOrNot)
            {
                float valid_PlayerProperty = sceneDefaultParameter.sceneBossHealth + player.adjustSceneBossHealth;
                if (valid_PlayerProperty < 0f) return 0f;
                else return valid_PlayerProperty;
            }
            if (player.playerMainEquipmentImpact_BossHealthBias != 0) return player.playerMainEquipmentImpact_BossHealthBias;
            float valid_PlayerAbility = sceneDefaultParameter.sceneBossHealth + player.playerMainEquipmentImpact_BossHealthDelta + player.adjustSceneBossHealth;
            if (valid_PlayerAbility < 0f) return 0f;
            else return valid_PlayerAbility;
        }
        return currentSceneBossHealth;
    }
    #endregion currentScene優先順序
    #region Update優先順序
    public float inputTimePriority(Player player)
    {
        //玩家沒有裝備
        //或玩家裝備已磨損
        //或玩家的裝備沒有能力
        if (!player.isPlayerMainEquipmentEquipped ||
            !player.playerMainEquipmentInWellCondition ||
            !player.playerMainEquipmentHasInputTimeImpactOrNot) return inputTime;
        else
        {
            if (player.playerMainEquipmentImpact_InputTimeBias != 0) return player.playerMainEquipmentImpact_InputTimeBias;
            float valid_PlayerAbility = inputTime + player.playerMainEquipmentImpact_InputTimeDelta;
            if (valid_PlayerAbility < 0f) return 0f;
            else return valid_PlayerAbility;
        }
    }
    #endregion Update優先順序
}