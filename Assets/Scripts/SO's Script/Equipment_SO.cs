using UnityEngine;
[CreateAssetMenu(fileName = "Equipment_", menuName = "Equipment")]
public class Equipment_SO : ScriptableObject
{
    public EquipmentType equipmentType;
    public bool equipmentIsEquippedAsMainOrNot;
    public bool equipmantIsEquippedAsSecondary1OrNot;
    public bool equipmantIsEquippedAsSecondary2OrNot;
    public bool equipmantIsEquippedAsSecondary3OrNot;
    public bool equipmantIsEquippedAsSecondary4OrNot;
    [Tooltip("�˳ƶˮ`")] public float equipmentDamage;
    [Tooltip("�˳ƨ��m")] public float equipmentDefense;
    [Tooltip("�˳ƥثe�@�[��")] public int equipmentCurrentDurability;
    [Tooltip("��ثe�@�[�קC�󦹭ȡA�����N�L��")] public int equipmentCorruptingDurability;
    [Tooltip("�˳ƭ״_�ݭn�����~")] public AllItem equipmentRepairItem;
    [Tooltip("�˳ƭ״_���~�ƶq")] public int equipmentRepairItemAmount;
    [Space(10)]
    [Header("�����]�w(���ȿ�J�ɡA�����|�bPlayerAbility�ͮ�)")]
    [Header("�W�Ƴ��OBias, Bias���u������")]
    [Header("Bias�N���J�h�ָӳ����ƭȧY���h��, Bias���u������")]
    [Header("Delta�N��ӳ����w�]�ȥ[�W��ƭ�(Delta)")]
    [Space(10)]
    [Header("�ɶ�����")]
    public float equipmentInputTimeBiasImpact;
    public float equipmentInputTimeDeltaImpact;
    [Space(10)]
    [Header("PromptTypeNumber����")]
    [Range(0, 4)] public int equipmentPromptTypeNumberImpact;
    [Space(10)]
    [Header("MinPromptOdds����")]
    public float equipmentMinPromptOddsBiasImpact;
    public float equipmentMinPromptOddsDeltaImpact;
    [Space(10)]
    [Header("MaxPromptNumber����")]
    public int equipmentMaxPromptNumberBiasImpact;
    public int equipmentMaxPromptNumberDeltaImpact;
    [Space(10)]
    [Header("MinPromptNumber����")]
    public int equipmentMinPromptNumberBiasImpact;
    public int equipmentMinPromptNumberDeltaImpact;
    [Space(10)]
    [Header("PatienceMinus����")]
    public float equipmentPatienceMinusBiasImpact;
    public float equipmentPatienceMinusDeltaImpact;
    [Space(10)]
    [Header("TotalStage����")]
    public int equipmentTotalStageBiasImpact;
    public int equipmentTotalStageDeltaImpact;
    [Space(10)]
    [Header("WrongInputPatincePenalty����")]
    public float equipmentWrongInputPatincePenaltyBiasImpact;
    public float equipmentWrongInputPatincePenaltyDeltaImpact;
    [Space(10)]
    [Header("DestroyAllOrNot����")]
    public bool equipmentDestroyAllImpact;
    public bool equipmentNotDestroyAllImpact;
    [Space(10)]
    [Header("PromptEveryPopTime����")]
    public float equipmentPromptEveryPopTimeBiasImpact;
    public float equipmentPromptEveryPopTimeDeltaImpact;
    [Space(10)]
    [Header("PromptRespawnTime����")]
    public float equipmentPromptRespawnTimeBiasImpact;
    public float equipmentPromptRespawnTimeDeltaImpact;
    //�ثe�����
    //[Space(10)]
    //[Header("BossSceneOrNot����")]
    //public bool equipmentBossSceneImpact;
    //public bool equipmentNotBossSceneImpact;
    [Space(10)]
    [Header("BossRushOdds����")]
    public float equipmentBossRushOddsBiasImpact;
    public float equipmentBossRushOddsDeltaImpact;
    [Space(10)]
    [Header("BossRushTime����")]
    public float equipmentBossRushTimeBiasImpact;
    public float equipmentBossRushTimeDeltaImpact;
    [Space(10)]
    [Header("BossHealth����")]
    public float equipmentBossHealthBiasImpact;
    public float equipmentBossHealthDeltaImpact;
    public void Initialize()
    {
        this.equipmentType = EquipmentType.NOT_RESTRICTED;
        this.equipmentIsEquippedAsMainOrNot = false;
        this.equipmantIsEquippedAsSecondary1OrNot = false;
        this.equipmantIsEquippedAsSecondary2OrNot = false;
        this.equipmantIsEquippedAsSecondary3OrNot = false;
        this.equipmantIsEquippedAsSecondary4OrNot = false;
        this.equipmentDamage = 0f;
        this.equipmentDefense = 0f;
        this.equipmentCurrentDurability = 0;
        this.equipmentCorruptingDurability = 0;
        this.equipmentRepairItem = AllItem.None;
        this.equipmentRepairItemAmount = 0;
        this.equipmentPromptTypeNumberImpact = 0;
        this.equipmentInputTimeBiasImpact = 0f;
        this.equipmentInputTimeDeltaImpact = 0f;
        this.equipmentMinPromptOddsBiasImpact = 0f;
        this.equipmentMinPromptOddsDeltaImpact = 0f;
        this.equipmentMaxPromptNumberBiasImpact = 0;
        this.equipmentMaxPromptNumberDeltaImpact = 0;
        this.equipmentMinPromptNumberBiasImpact = 0;
        this.equipmentMinPromptNumberDeltaImpact = 0;
        this.equipmentPatienceMinusBiasImpact = 0f;
        this.equipmentPatienceMinusDeltaImpact = 0f;
        this.equipmentTotalStageBiasImpact = 0;
        this.equipmentTotalStageDeltaImpact = 0;
        this.equipmentWrongInputPatincePenaltyBiasImpact = 0f;
        this.equipmentWrongInputPatincePenaltyDeltaImpact = 0f;
        this.equipmentDestroyAllImpact = false;
        this.equipmentNotDestroyAllImpact = false;
        this.equipmentPromptEveryPopTimeBiasImpact = 0f;
        this.equipmentPromptEveryPopTimeDeltaImpact = 0f;
        this.equipmentPromptRespawnTimeBiasImpact = 0f;
        this.equipmentPromptRespawnTimeDeltaImpact = 0f;
        //�ثe�����
        //this.equipmentBossSceneImpact = false;
        //this.equipmentNotBossSceneImpact = false;
        this.equipmentBossRushOddsBiasImpact = 0f;
        this.equipmentBossRushOddsDeltaImpact = 0f;
        this.equipmentBossRushTimeBiasImpact = 0f;
        this.equipmentBossRushTimeDeltaImpact = 0f;
        this.equipmentBossHealthBiasImpact = 0f;
        this.equipmentBossHealthDeltaImpact = 0f;
    }
    public void Initialize(Equipment_SO newEquipment)
    {
        this.equipmentType = EquipmentType.NOT_RESTRICTED;
        this.equipmentIsEquippedAsMainOrNot = false;
        this.equipmantIsEquippedAsSecondary1OrNot = false;
        this.equipmantIsEquippedAsSecondary2OrNot = false;
        this.equipmantIsEquippedAsSecondary3OrNot = false;
        this.equipmantIsEquippedAsSecondary4OrNot = false;
        this.equipmentDamage = newEquipment.equipmentDamage;
        this.equipmentDefense = newEquipment.equipmentDefense;
        this.equipmentCurrentDurability = newEquipment.equipmentCurrentDurability;
        this.equipmentCorruptingDurability = newEquipment.equipmentCorruptingDurability;
        this.equipmentRepairItem = newEquipment.equipmentRepairItem;
        this.equipmentRepairItemAmount = newEquipment.equipmentRepairItemAmount;
        this.equipmentPromptTypeNumberImpact = newEquipment.equipmentPromptTypeNumberImpact;
        this.equipmentInputTimeBiasImpact = newEquipment.equipmentInputTimeBiasImpact;
        this.equipmentInputTimeDeltaImpact = newEquipment.equipmentInputTimeDeltaImpact;
        this.equipmentMinPromptOddsBiasImpact = newEquipment.equipmentMinPromptOddsBiasImpact;
        this.equipmentMinPromptOddsDeltaImpact = newEquipment.equipmentMinPromptOddsDeltaImpact;
        this.equipmentMaxPromptNumberBiasImpact = newEquipment.equipmentMaxPromptNumberBiasImpact;
        this.equipmentMaxPromptNumberDeltaImpact = newEquipment.equipmentMaxPromptNumberDeltaImpact;
        this.equipmentMinPromptNumberBiasImpact = newEquipment.equipmentMinPromptNumberBiasImpact;
        this.equipmentMinPromptNumberDeltaImpact = newEquipment.equipmentMinPromptNumberDeltaImpact;
        this.equipmentPatienceMinusBiasImpact = newEquipment.equipmentPatienceMinusBiasImpact;
        this.equipmentPatienceMinusDeltaImpact = newEquipment.equipmentPatienceMinusDeltaImpact;
        this.equipmentTotalStageBiasImpact = newEquipment.equipmentTotalStageBiasImpact;
        this.equipmentTotalStageDeltaImpact = newEquipment.equipmentTotalStageDeltaImpact;
        this.equipmentWrongInputPatincePenaltyBiasImpact = newEquipment.equipmentWrongInputPatincePenaltyBiasImpact;
        this.equipmentWrongInputPatincePenaltyDeltaImpact = newEquipment.equipmentWrongInputPatincePenaltyDeltaImpact;
        this.equipmentDestroyAllImpact = newEquipment.equipmentDestroyAllImpact;
        this.equipmentNotDestroyAllImpact = newEquipment.equipmentNotDestroyAllImpact;
        this.equipmentPromptEveryPopTimeBiasImpact = newEquipment.equipmentPromptEveryPopTimeBiasImpact;
        this.equipmentPromptEveryPopTimeDeltaImpact = newEquipment.equipmentPromptEveryPopTimeDeltaImpact;
        this.equipmentPromptRespawnTimeBiasImpact = newEquipment.equipmentPromptRespawnTimeBiasImpact;
        this.equipmentPromptRespawnTimeDeltaImpact = newEquipment.equipmentPromptRespawnTimeDeltaImpact;
        //�ثe�����
        //this.equipmentBossSceneImpact = newEquipment.equipmentBossSceneImpact;
        //this.equipmentNotBossSceneImpact = newEquipment.equipmentNotBossSceneImpact;
        this.equipmentBossRushOddsBiasImpact = newEquipment.equipmentBossRushOddsBiasImpact;
        this.equipmentBossRushOddsDeltaImpact = newEquipment.equipmentBossRushOddsDeltaImpact;
        this.equipmentBossRushTimeBiasImpact = newEquipment.equipmentBossRushTimeBiasImpact;
        this.equipmentBossRushTimeDeltaImpact = newEquipment.equipmentBossRushTimeDeltaImpact;
        this.equipmentBossHealthBiasImpact = newEquipment.equipmentBossHealthBiasImpact;
        this.equipmentBossHealthDeltaImpact = newEquipment.equipmentBossHealthDeltaImpact;
    }
    public void Initialize(ItemData itemData)
    {
        this.equipmentType = itemData.equipmentType;
        this.equipmentIsEquippedAsMainOrNot = itemData.equipmentIsEquippedAsMainOrNot;
        this.equipmantIsEquippedAsSecondary1OrNot = itemData.equipmantIsEquippedAsSecondary1OrNot;
        this.equipmantIsEquippedAsSecondary2OrNot = itemData.equipmantIsEquippedAsSecondary2OrNot;
        this.equipmantIsEquippedAsSecondary3OrNot = itemData.equipmantIsEquippedAsSecondary3OrNot;
        this.equipmantIsEquippedAsSecondary4OrNot = itemData.equipmantIsEquippedAsSecondary4OrNot;
        this.equipmentDamage = itemData.equipmentDamage;
        this.equipmentDefense = itemData.equipmentDefense;
        this.equipmentCurrentDurability = itemData.equipmentCurrentDurability;
        this.equipmentCorruptingDurability = itemData.equipmentCorruptingDurability;
        this.equipmentRepairItem = itemData.equipmentRepairItem;
        this.equipmentRepairItemAmount = itemData.equipmentRepairItemAmount;
        this.equipmentPromptTypeNumberImpact = itemData.equipmentPromptTypeNumberImpact;
        this.equipmentInputTimeBiasImpact = itemData.equipmentInputTimeBiasImpact;
        this.equipmentInputTimeDeltaImpact = itemData.equipmentInputTimeDeltaImpact;
        this.equipmentMinPromptOddsBiasImpact = itemData.equipmentMinPromptOddsBiasImpact;
        this.equipmentMinPromptOddsDeltaImpact = itemData.equipmentMinPromptOddsDeltaImpact;
        this.equipmentMaxPromptNumberBiasImpact = itemData.equipmentMaxPromptNumberBiasImpact;
        this.equipmentMaxPromptNumberDeltaImpact = itemData.equipmentMaxPromptNumberDeltaImpact;
        this.equipmentMinPromptNumberBiasImpact = itemData.equipmentMinPromptNumberBiasImpact;
        this.equipmentMinPromptNumberDeltaImpact = itemData.equipmentMinPromptNumberDeltaImpact;
        this.equipmentPatienceMinusBiasImpact = itemData.equipmentPatienceMinusBiasImpact;
        this.equipmentPatienceMinusDeltaImpact = itemData.equipmentPatienceMinusDeltaImpact;
        this.equipmentTotalStageBiasImpact = itemData.equipmentTotalStageBiasImpact;
        this.equipmentTotalStageDeltaImpact = itemData.equipmentTotalStageDeltaImpact;
        this.equipmentWrongInputPatincePenaltyBiasImpact = itemData.equipmentWrongInputPatincePenaltyBiasImpact;
        this.equipmentWrongInputPatincePenaltyDeltaImpact = itemData.equipmentWrongInputPatincePenaltyDeltaImpact;
        this.equipmentDestroyAllImpact = itemData.equipmentDestroyAllImpact;
        this.equipmentNotDestroyAllImpact = itemData.equipmentNotDestroyAllImpact;
        this.equipmentPromptEveryPopTimeBiasImpact = itemData.equipmentPromptEveryPopTimeBiasImpact;
        this.equipmentPromptEveryPopTimeDeltaImpact = itemData.equipmentPromptEveryPopTimeDeltaImpact;
        this.equipmentPromptRespawnTimeBiasImpact = itemData.equipmentPromptRespawnTimeBiasImpact;
        this.equipmentPromptRespawnTimeDeltaImpact = itemData.equipmentPromptRespawnTimeDeltaImpact;
        //�ثe�����
        //this.equipmentBossSceneImpact = newEquipment.equipmentBossSceneImpact;
        //this.equipmentNotBossSceneImpact = newEquipment.equipmentNotBossSceneImpact;
        this.equipmentBossRushOddsBiasImpact = itemData.equipmentBossRushOddsBiasImpact;
        this.equipmentBossRushOddsDeltaImpact = itemData.equipmentBossRushOddsDeltaImpact;
        this.equipmentBossRushTimeBiasImpact = itemData.equipmentBossRushTimeBiasImpact;
        this.equipmentBossRushTimeDeltaImpact = itemData.equipmentBossRushTimeDeltaImpact;
        this.equipmentBossHealthBiasImpact = itemData.equipmentBossHealthBiasImpact;
        this.equipmentBossHealthDeltaImpact = itemData.equipmentBossHealthDeltaImpact;
    }
}