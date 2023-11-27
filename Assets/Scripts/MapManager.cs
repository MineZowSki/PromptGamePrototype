using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Player player;
    [Header("開關的面板")]
    [Tooltip("場景面板")] public GameObject locationInfoPanel;
    [Tooltip("任務選單")] public GameObject missionListPanel;
    [Tooltip("世界選單")] public GameObject worldMapPanel;
    [Tooltip("通知面板")] public GameObject notifyPanel;
    [Tooltip("場景資訊面板")] public GameObject locationUIInfo;
    [Tooltip("參數資訊面板")] public GameObject locationParameterInfo;
    [Tooltip("Save & Menu")] public GameObject leaveGamePanel;
    [Header("任務相關")]
    [Tooltip("任務名稱")] public TextMeshProUGUI missionName;
    [Tooltip("任務詳情")] public TextMeshProUGUI missionInfo;
    [Tooltip("任務經驗值")] public TextMeshProUGUI missionEXP;
    [Tooltip("任務獎勵圖片")] public Image missionRewardImage;
    [Tooltip("任務獎勵名稱")] public TextMeshProUGUI missionRewardName;
    [Tooltip("任務獎勵數量")] public TextMeshProUGUI missionRewardAmount;
    [Tooltip("任務進度表")] public List<GameObject> missionProgressList;
    [Tooltip("任務獎勵獲取按鈕")] public GameObject missionClaimRewardButton;
    [Tooltip("目前任務的Index")] private int currentMissionIndex;
    [Header("世界地圖")]
    [Tooltip("世界卷軸Content")] public GameObject worldContent;
    [Tooltip("放入Content內的Prefab")] public GameObject contentMemberPrefab;
    [Tooltip("所有地圖的圖的List")] public List<Sprite> allMapImage;
    [Tooltip("所有地圖的GameObject的List")] public List<GameObject> allMapObject;
    [Tooltip("目前選擇的地圖")] private int currentMapIndex;
    [Header("地點相關")]
    [Tooltip("地點名稱")] public TextMeshProUGUI locationName;
    [Tooltip("地點解鎖等級需求")] public List<LocationUnlock_SO> allLocationLevelInfo = new List<LocationUnlock_SO>();
    [Header("用來開關滑鼠碰撞")]
    [Tooltip("新地點要加入")] public GameObject[] locations;
    [Header("聲音相關")]
    public AudioClip locationSound;
    private void Start()
    {
        worldMapPanel.SetActive(false);
        locationInfoPanel.SetActive(false);
        missionListPanel.SetActive(false);
        notifyPanel.SetActive(false);
        leaveGamePanel.SetActive(false);
        leaveGamePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(GameManager.instance.SavePlayer);
        for(int i = 0; i < allMapImage.Count; i++)
        {
            int index = i;
            var temp = Instantiate(contentMemberPrefab, worldContent.transform);
            temp.GetComponent<Image>().sprite = allMapImage[i];
            temp.GetComponent<Button>().onClick.AddListener(() => GoToMap(index));
        }
    }
    public void MapHelper()
    {
        Notification($"Press {InputsManager.instance.defaultKeyBinding[4]} to open Inventory\n" +
            "To equip a equipment, hold the input showing upon your equipment slot and click on the equipment you want to equip");
    }
    public void MissionSlot(int missionIndex)
    {
        currentMissionIndex = missionIndex;
        missionRewardImage.enabled = true;
        missionClaimRewardButton.SetActive(MissionList.missionList[missionIndex].missionCompletedOrNot);
        missionName.text = MissionList.missionList[missionIndex].missionName;
        missionInfo.text = MissionList.missionList[missionIndex].missionInfo;
        missionEXP.text = MissionList.missionList[missionIndex].missionEXP.ToString();
        missionRewardImage.enabled = true;
        if (MissionList.missionList[missionIndex].missionRewardSprite == null) missionRewardImage.enabled = false;
        missionRewardImage.sprite = MissionList.missionList[missionIndex].missionRewardSprite;
        missionRewardName.text = MissionList.missionList[missionIndex].missionRewardName;
        missionRewardAmount.enabled = true;
        if(MissionList.missionList[missionIndex].missionRewardNumber == 0) missionRewardAmount.enabled = false;
        missionRewardAmount.text = MissionList.missionList[missionIndex].missionRewardNumber.ToString();
    }
    public void ClaimReward()
    {
        if (MissionList.missionList[currentMissionIndex].missionRewardEnum == AllItem.None)
        {

        }
        else if (InventoryManager.instance.inventoryFull(MissionList.missionList[currentMissionIndex].missionRewardEnum))
        {
            Notification("Inventory Full");
            return;
        }
        else if (InventoryManager.instance.inventoryNotFull(MissionList.missionList[currentMissionIndex].missionRewardEnum))
        {
            InventoryManager.instance.firstEmptySlot().Initialize
                (
                MissionList.missionList[currentMissionIndex].missionRewardItemSO,
                MissionList.missionList[currentMissionIndex].missionRewardNumber,
                MissionList.missionList[currentMissionIndex].missionRewardEquipmentDurability
                );
        }
        else
        {
            EventHandler.CallAddPlayerItem
                (
                MissionList.missionList[currentMissionIndex].missionRewardEnum,
                MissionList.missionList[currentMissionIndex].missionRewardNumber,
                MissionList.missionList[currentMissionIndex].missionRewardIsEquipmentOrNot,
                MissionList.missionList[currentMissionIndex].missionRewardEquipmentDurability
                );
        }
        player.playerEXP += MissionList.missionList[currentMissionIndex].missionEXP;
        SoundManager.instance.PlaySound(InventoryManager.instance.bagSound, 0.3f);
        MissionList.missionList.RemoveAt(currentMissionIndex);
        MissionListReList();
        if (currentMissionIndex != 0) MissionSlot(currentMissionIndex - 1);
    }
    public void MissionListReList()
    {
        missionName.text = string.Empty;
        missionInfo.text = string.Empty;
        missionEXP.text = string.Empty;
        missionRewardName.text = string.Empty;
        missionRewardImage.enabled = false;
        missionRewardAmount.text = string.Empty;
        missionClaimRewardButton.SetActive(false);
        for (int i = 0; i < missionProgressList.Count; i++)
        {
            missionProgressList[i].SetActive(false);
            if (i > MissionList.missionList.Count - 1) continue;
            missionProgressList[i].SetActive(true);
            missionProgressList[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(MissionList.missionList[i].missionCompletedOrNot);
            missionProgressList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = MissionList.missionList[i].missionName;
        }
    }
    public virtual void Notification(string notificationText)
    {
        notifyPanel.SetActive(true);
        TextMeshProUGUI notification = notifyPanel.GetComponentInChildren<TextMeshProUGUI>();
        notification.text = notificationText;
    }
    public void GoToMap(int index)
    {
        currentMapIndex = index;
        for (int i = 0; i < allMapObject.Count; i++)
        {
            if (i == currentMapIndex)
            {
                allMapObject[i].SetActive(true);
                allMapObject[i].transform.localPosition = Vector3.zero;
                continue;
            }
            allMapObject[i].SetActive(false);
        }
        worldMapPanel.SetActive(false);
        locationName.text = string.Empty;
    }
    public void LocationInfoSwitch()
    {
        bool switchOnOff = locationParameterInfo.activeSelf;
        locationUIInfo.SetActive(switchOnOff);
        locationParameterInfo.SetActive(!switchOnOff);
    }
    public void WorldPanelSwitch()
    {
        bool switchOnOff = worldMapPanel.activeSelf;
        worldMapPanel.SetActive(!switchOnOff);
    }
    public void MissionPanelSwitch()
    {
        bool switchOnOff = missionListPanel.activeSelf;
        missionListPanel.SetActive(!switchOnOff);
        MissionListReList();
    }
    private void Update()
    {
        if (PlayerInput.leaveGameInput)
        {
            leaveGamePanel.SetActive(true);
        }
        if (InventoryManager.instance != null &&
            (InventoryManager.instance.inventoryOnOff.activeSelf ||
            locationInfoPanel.activeSelf ||
            missionListPanel.activeSelf ||
            leaveGamePanel.activeSelf ||
            worldMapPanel.activeSelf))
        {
            foreach (GameObject location in locations)
            {
                location.SetActive(false);
            }
            return;
        }
        else
        {
            foreach (GameObject location in locations)
            {
                location.SetActive(true);
            }
        }
        foreach (var location in allLocationLevelInfo)
        {
            bool playerLevelLacking = player.playerLevel < location.playerLevelRequire;
            locations.ToList().Find(n => n.CompareTag(location.locationName.ToString())).SetActive(!playerLevelLacking);
            if (!location.locationUnlockedOnce && !playerLevelLacking)
            {
                Notification($"You've unlocked {location.locationName}");
                location.locationUnlockedOnce = true;
            }            
        }
    }
}