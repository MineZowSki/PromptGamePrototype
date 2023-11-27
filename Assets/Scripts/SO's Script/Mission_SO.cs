using UnityEngine;
[CreateAssetMenu(fileName = "Mission_", menuName = "Mission/Mission")]
public class Mission_SO : ScriptableObject
{
    public bool missionCompletedOrNot;
    public Sprite missionSprite;
    public string missionName;
    [TextArea]public string missionInfo;
    public float missionEXP;
    [Header("���ȻݨD&�i��")]
    [Tooltip("�ݨD���~")] public AllItem missionRequestItem;
    [Tooltip("�ݨD�a�I")] public SceneName missionRequestScene;
    [Tooltip("�ݨD�ƶq")] public int missionProgress;
    [Header("���ȼ��y����")]
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