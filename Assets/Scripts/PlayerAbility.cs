using System.Collections.Generic;
public abstract class PlayerAbility : PlayerMainInfo
{ 
    public bool isPlayerMainEquipmentEquipped => playerInfo.playerMainEquipment != null;
    public bool isPlayerSecondaryEquipment1Equipped => playerInfo.playerSecondaryEquipment1 != null;
    public bool isPlayerSecondaryEquipment2Equipped => playerInfo.playerSecondaryEquipment2 != null;
    public bool isPlayerSecondaryEquipment3Equipped => playerInfo.playerSecondaryEquipment3 != null;
    public bool isPlayerSecondaryEquipment4Equipped => playerInfo.playerSecondaryEquipment4 != null;
    public EquipmentType playerMainEquipmentType => playerInfo.playerMainEquipment.equipmentInfo.equipmentType;
    public EquipmentType playerSecondaryEquipment1Type => playerInfo.playerSecondaryEquipment1.equipmentInfo.equipmentType;
    public EquipmentType playerSecondaryEquipment2Type => playerInfo.playerSecondaryEquipment2.equipmentInfo.equipmentType;
    public EquipmentType playerSecondaryEquipment3Type => playerInfo.playerSecondaryEquipment3.equipmentInfo.equipmentType;
    public EquipmentType playerSecondaryEquipment4Type => playerInfo.playerSecondaryEquipment4.equipmentInfo.equipmentType;
    public float playerMainEquipmentDamageDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentDamage + UnityEngine.Random.Range(0f, playerInfo.playerMainEquipment.equipmentInfo.equipmentDamage) + playerDamageDelta;
    public float playerSecondaryEquipment1DamageDelta => playerInfo.playerSecondaryEquipment1.equipmentInfo.equipmentDamage + UnityEngine.Random.Range(0f, playerInfo.playerSecondaryEquipment1.equipmentInfo.equipmentDamage) + playerDamageDelta;
    public float playerSecondaryEquipment2DamageDelta => playerInfo.playerSecondaryEquipment2.equipmentInfo.equipmentDamage + UnityEngine.Random.Range(0f, playerInfo.playerSecondaryEquipment2.equipmentInfo.equipmentDamage) + playerDamageDelta;
    public float playerSecondaryEquipment3DamageDelta => playerInfo.playerSecondaryEquipment3.equipmentInfo.equipmentDamage + UnityEngine.Random.Range(0f, playerInfo.playerSecondaryEquipment3.equipmentInfo.equipmentDamage) + playerDamageDelta;
    public float playerSecondaryEquipment4DamageDelta => playerInfo.playerSecondaryEquipment4.equipmentInfo.equipmentDamage + UnityEngine.Random.Range(0f, playerInfo.playerSecondaryEquipment4.equipmentInfo.equipmentDamage) + playerDamageDelta;
    public float playerMainEquipmentCurrentDurability => playerInfo.playerMainEquipment.equipmentInfo.equipmentCurrentDurability;
    public float playerMainEquipmentCorruptingDurability => playerInfo.playerMainEquipment.equipmentInfo.equipmentCorruptingDurability;
    public AllItem playerMainEquipmentRepairItem => playerInfo.playerMainEquipment.equipmentInfo.equipmentRepairItem;
    public int playerMainEquipmentRepairItemAmount => playerInfo.playerMainEquipment.equipmentInfo.equipmentRepairItemAmount;
    #region 玩家是否有衝擊能力
    /// <summary>
    /// 玩家裝備是否磨損
    /// </summary>
    public bool playerMainEquipmentInWellCondition => playerInfo.playerMainEquipment.equipmentInfo.equipmentCurrentDurability > playerInfo.playerMainEquipment.equipmentInfo.equipmentCorruptingDurability;
    public bool playerMainEquipmentHasInputTimeImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentInputTimeDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentInputTimeBiasImpact != 0;
    public bool playerMainEquipmentHasPromptTypeNumberImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptTypeNumberImpact != 0;
    public bool playerMainEquipmentHasMinPromptOddsImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptOddsDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptOddsBiasImpact != 0;
    public bool playerMainEquipmentHasMaxPromptNumberImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentMaxPromptNumberDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentMaxPromptNumberBiasImpact != 0;
    public bool playerMainEquipmentHasMinPromptNumberImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptNumberDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptNumberBiasImpact != 0;
    public bool playerMainEquipmentHasPatienceMinusImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentPatienceMinusDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentPatienceMinusBiasImpact != 0;
    public bool playerMainEquipmentHasTotalStageImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentTotalStageDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentTotalStageBiasImpact != 0;
    public bool playerMainEquipmentHasWrongInputPatincePenaltyImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentWrongInputPatincePenaltyDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentWrongInputPatincePenaltyBiasImpact != 0;
    public bool playerMainEquipmentHasDestroyAllOrNotImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentDestroyAllImpact == true || playerInfo.playerMainEquipment.equipmentInfo.equipmentNotDestroyAllImpact == true;
    public bool playerMainEquipmentHasPromptEveryPopTimeImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptEveryPopTimeDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptEveryPopTimeBiasImpact != 0;
    public bool playerMainEquipmentHasPromptRespawnTimeImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptRespawnTimeDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptRespawnTimeBiasImpact != 0;
    //public bool playerMainEquipmentHasBossSceneOrNotImpactOrNot => playerAllInfo.playerMainEquipment.equipmentInfo.equipmentBossSceneImpact == true || playerAllInfo.playerMainEquipment.equipmentInfo.equipmentNotBossSceneImpact == true;
    public bool playerMainEquipmentHasBossRushOddsImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushOddsDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushOddsBiasImpact != 0;
    public bool playerMainEquipmentHasBossRushTimeImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushTimeDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushTimeBiasImpact != 0;
    public bool playerMainEquipmentHasBossHealthImpactOrNot => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossHealthDeltaImpact != 0 || playerInfo.playerMainEquipment.equipmentInfo.equipmentBossHealthBiasImpact != 0;
    #endregion 玩家是否有衝擊能力

    #region 衝擊能力
    public float playerMainEquipmentImpact_InputTimeBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentInputTimeBiasImpact;
    public float playerMainEquipmentImpact_InputTimeDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentInputTimeDeltaImpact;
    //分隔行
    public int playerMainEquipmentImpact_PromptTypeNumber => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptTypeNumberImpact;
    //分隔行
    public float playerMainEquipmentImpact_MinPromptOddsBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptOddsBiasImpact;
    public float playerMainEquipmentImpact_MinPromptOddsDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptOddsDeltaImpact;
    //分隔行
    public int playerMainEquipmentImpact_MaxPromptNumberBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentMaxPromptNumberBiasImpact;
    public int playerMainEquipmentImpact_MaxPromptNumberDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentMaxPromptNumberDeltaImpact;
    //分隔行
    public int playerMainEquipmentImpact_MinPromptNumberBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptNumberBiasImpact;
    public int playerMainEquipmentImpact_MinPromptNumberDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentMinPromptNumberDeltaImpact;
    //分隔行
    public float playerMainEquipmentImpact_PatienceMinusBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentPatienceMinusBiasImpact;
    public float playerMainEquipmentImpact_PatienceMinusDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentPatienceMinusDeltaImpact;
    //分隔行
    public int playerMainEquipmentImpact_TotalStageBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentTotalStageBiasImpact;
    public int playerMainEquipmentImpact_TotalStageDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentTotalStageDeltaImpact;
    //分隔行
    public float playerMainEquipmentImpact_WrongInputPatincePenaltyBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentWrongInputPatincePenaltyBiasImpact;
    public float playerMainEquipmentImpact_WrongInputPatincePenaltyDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentWrongInputPatincePenaltyDeltaImpact;
    //分隔行
    public bool playerMainEquipmentImpact_DestroyAll => playerInfo.playerMainEquipment.equipmentInfo.equipmentDestroyAllImpact;
    public bool playerMainEquipmentImpact_NotDestroyAll => playerInfo.playerMainEquipment.equipmentInfo.equipmentNotDestroyAllImpact;
    //分隔行
    public float playerMainEquipmentImpact_PromptEveryPopTimeBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptEveryPopTimeBiasImpact;
    public float playerMainEquipmentImpact_PromptEveryPopTimeDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptEveryPopTimeDeltaImpact;
    //分隔行
    public float playerMainEquipmentImpact_PromptRespawnTimeBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptRespawnTimeBiasImpact;
    public float playerMainEquipmentImpact_PromptRespawnTimeDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentPromptRespawnTimeDeltaImpact;
    //分隔行
    //public bool playerMainEquipmentImpact_BossScene => playerAllInfo.playerMainEquipment.equipmentInfo.equipmentBossSceneImpact;
    //public bool playerMainEquipmentImpact_NotBossScene => playerAllInfo.playerMainEquipment.equipmentInfo.equipmentNotBossSceneImpact;
    //分隔行
    public float playerMainEquipmentImpact_BossRushOddsBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushOddsBiasImpact;
    public float playerMainEquipmentImpact_BossRushOddsDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushOddsDeltaImpact;
    //分隔行
    public float playerMainEquipmentImpact_BossRushTimeBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushTimeBiasImpact;
    public float playerMainEquipmentImpact_BossRushTimeDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossRushTimeDeltaImpact;
    //分隔行
    public float playerMainEquipmentImpact_BossHealthBias => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossHealthBiasImpact;
    public float playerMainEquipmentImpact_BossHealthDelta => playerInfo.playerMainEquipment.equipmentInfo.equipmentBossHealthDeltaImpact;
    #endregion
    private List<InventoryItem_SO> equipmentList;
    protected override void Awake()
    {
        base.Awake();
        equipmentList = new List<InventoryItem_SO>();
    }
    public void Equipment()
    {
        equipmentList.Clear();
        equipmentList = playerBag.itemList.FindAll(n => n.itemIsEquipmentOrNot == true);
        playerInfo.playerMainEquipment = equipmentList.Find(n => n.equipmentInfo.equipmentIsEquippedAsMainOrNot);
        playerInfo.playerSecondaryEquipment1 = equipmentList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary1OrNot == true);
        playerInfo.playerSecondaryEquipment2 = equipmentList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary2OrNot == true);
        playerInfo.playerSecondaryEquipment3 = equipmentList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary3OrNot == true);
        playerInfo.playerSecondaryEquipment4 = equipmentList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary4OrNot == true);
    }
    public void EquipmentRepair(int repairedValue)
    {
        playerBag.itemList.Find(n => n.equipmentInfo.equipmentIsEquippedAsMainOrNot == true).equipmentInfo.equipmentCurrentDurability += repairedValue;
    }
    public void EquipmentWearDown(int wearValue, InventoryItem_SO equipment)
    {
        equipment.equipmentInfo.equipmentCurrentDurability -= wearValue;
        if (equipment.equipmentInfo.equipmentCurrentDurability <= 0)
        {
            SoundManager.instance.PlaySound(InventoryManager.instance.equipmentBreakSound, true);
            playerBag.itemList.Find(n => n == equipment).Initialize();
        }
    }
    protected override void Update()
    {
        base.Update();
        Equipment();
    }
}