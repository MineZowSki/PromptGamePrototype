using UnityEngine;
[CreateAssetMenu(fileName = "Mission_", menuName = "Mission/Mission")]
public class Mission_SO : ScriptableObject
{
    public bool missionCompletedOrNot;
    public Sprite missionSprite;
    public string missionName;
    [TextArea]public string missionInfo;
    public float missionEXP;
    [Header("任務需求&進度")]
    [Tooltip("需求物品")] public AllItem missionRequestItem;
    [Tooltip("需求地點")] public SceneName missionRequestScene;
    [Tooltip("需求數量")] public int missionProgress;
    [Header("任務獎勵相關")]
    public Sprite missionRewardSprite;
    public AllItem missionRewardEnum;
    public string missionRewardName;
    public int missionRewardNumber;
    public InventoryItem_SO missionRewardItemSO;
    public bool missionRewardIsEquipmentOrNot;
    public int missionRewardEquipmentDurability;
    public void Initialize(MissionData data)
    {
        this.missionCompletedOrNot = data.missionCompletedOrNot;
    }
}