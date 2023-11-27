using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Player player;
    [Header("�}�������O")]
    [Tooltip("�������O")] public GameObject locationInfoPanel;
    [Tooltip("���ȿ��")] public GameObject missionListPanel;
    [Tooltip("�@�ɿ��")] public GameObject worldMapPanel;
    [Tooltip("�q�����O")] public GameObject notifyPanel;
    [Tooltip("������T���O")] public GameObject locationUIInfo;
    [Tooltip("�ѼƸ�T���O")] public GameObject locationParameterInfo;
    [Tooltip("Save & Menu")] public GameObject leaveGamePanel;
    [Header("���Ȭ���")]
    [Tooltip("���ȦW��")] public TextMeshProUGUI missionName;
    [Tooltip("���ȸԱ�")] public TextMeshProUGUI missionInfo;
    [Tooltip("���ȸg���")] public TextMeshProUGUI missionEXP;
    [Tooltip("���ȼ��y�Ϥ�")] public Image missionRewardImage;
    [Tooltip("���ȼ��y�W��")] public TextMeshProUGUI missionRewardName;
    [Tooltip("���ȼ��y�ƶq")] public TextMeshProUGUI missionRewardAmount;
    [Tooltip("���ȶi�ת�")] public List<GameObject> missionProgressList;
    [Tooltip("���ȼ��y������s")] public GameObject missionClaimRewardButton;
    [Tooltip("�ثe���Ȫ�Index")] private int currentMissionIndex;
    [Header("�@�ɦa��")]
    [Tooltip("�@�ɨ��bContent")] public GameObject worldContent;
    [Tooltip("��JContent����Prefab")] public GameObject contentMemberPrefab;
    [Tooltip("�Ҧ��a�Ϫ��Ϫ�List")] public List<Sprite> allMapImage;
    [Tooltip("�Ҧ��a�Ϫ�GameObject��List")] public List<GameObject> allMapObject;
    [Tooltip("�ثe��ܪ��a��")] private int currentMapIndex;
    [Header("�a�I����")]
    [Tooltip("�a�I�W��")] public TextMeshProUGUI locationName;
    [Tooltip("�a�I���굥�ŻݨD")] public List<LocationUnlock_SO> allLocationLevelInfo = new List<LocationUnlock_SO>();
    [Header("�ΨӶ}���ƹ��I��")]
    [Tooltip("�s�a�I�n�[�J")] public GameObject[] locations;
    [Header("�n������")]
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