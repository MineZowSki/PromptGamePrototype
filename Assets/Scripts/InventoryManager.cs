using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : PersistentSingleton<InventoryManager>
{
    [Tooltip("Prompt去留站的第一樣物品")]private Prompts_SO firstPrompt => playerInventoryForTransfer.promptItemList.First();
    [Header("包包介面")]
    [Tooltip("玩家背包")] public GameObject inventoryOnOff;
    [Tooltip("玩家物品頁面")]public GameObject inventoryPage;
    [Tooltip("玩家資訊頁面")] public GameObject playerInfoPage;
    [Tooltip("玩家捨棄物品頁面")] public GameObject itemDiscardPanel;
    [Tooltip("玩家背包格子內物品")]public GameObject[] inventorySlotsItem;
    [Space(10)]
    [Header("包包物品詳情顯示")]
    [Tooltip("物品名稱")]public TextMeshProUGUI itemName;
    [Tooltip("物品資訊")] public TextMeshProUGUI itemDescription;
    [Tooltip("物品數量")] public TextMeshProUGUI itemNumber;
    [Tooltip("物品捨棄條")] public Slider itemDiscardSlider;
    [Tooltip("物品捨棄數量")] public TextMeshProUGUI itemDiscardAmount;
    [Space(10)]
    [Header("包包玩家資訊顯示")]
    [Tooltip("玩家等級")] public TextMeshProUGUI playerLevel;
    [Tooltip("玩家經驗值")] public TextMeshProUGUI playerEXP;
    [Tooltip("玩家總按錯數量")] public TextMeshProUGUI playerMistake;
    [Space(10)]
    [Header("背包裝備顯示")]
    [Tooltip("主要裝備")] public Image mainEquipmentImage;
    [Tooltip("次要裝備1")] public Image secondaryEquipment1Image;
    [Tooltip("次要裝備2")] public Image secondaryEquipment2Image;
    [Tooltip("次要裝備3")] public Image secondaryEquipment3Image;
    [Tooltip("次要裝備4")] public Image secondaryEquipment4Image;
    [Tooltip("次要裝備1的快捷鍵的Text")] public TextMeshProUGUI secondaryEquipment1InputText;
    [Tooltip("次要裝備2的快捷鍵的Text")] public TextMeshProUGUI secondaryEquipment2InputText;
    [Tooltip("次要裝備3的快捷鍵的Text")] public TextMeshProUGUI secondaryEquipment3InputText;
    [Tooltip("次要裝備4的快捷鍵的Text")] public TextMeshProUGUI secondaryEquipment4InputText;
    [Space(10)]
    [Header("SO類型")]
    [Tooltip("這裡是Prompt的去留暫存站")]public PlayerInventory_SO playerInventoryForTransfer;
    [Tooltip("這個是玩家包包")]public ItemListSorted_SO playerBag;
    [Space(10)]
    [Header("包包物品初始化用")]
    [Tooltip("背包格子的Sprite")]public Sprite slotSprite;
    [Tooltip("如果沒讀到Resources裡的Sprite則用此表示")]public Sprite slotMissingSprite;
    [Space(10)]
    [Header("音效相關")]
    [Tooltip("打開背包音效")]public AudioClip bagSound;
    [Tooltip("背包格子音效")] public AudioClip slotSound;
    [Tooltip("裝備破壞音效")] public AudioClip equipmentBreakSound;
    [Space(10)]
    [Header("開發者使用，一般為空")]
    [SerializeField] private InventoryItem_SO developUse;
    private int currentSlotIndex;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        EventHandler.playerInfo += OnPlayerInfo;
        EventHandler.startNewGame += OnStartNewGame;
        EventHandler.addPlayerItem += OnAddPlayerItem;
    }
    private void OnDisable()
    {
        EventHandler.playerInfo -= OnPlayerInfo;
        EventHandler.startNewGame -= OnStartNewGame;
        EventHandler.addPlayerItem -= OnAddPlayerItem;
    }
    private void Start()
    {
        inventoryOnOff.SetActive(false);
        itemDiscardPanel.SetActive(false);
    }
    private void OnPlayerInfo(int level, float exp, int mistake)
    {
        playerLevel.text = level.ToString();
        playerEXP.text = exp.ToString();
        playerMistake.text = mistake.ToString();
    }
    private void OnStartNewGame()
    {
        foreach (var item in playerBag.itemList)
        {
            item.Initialize();
        }
    }
    private void OnAddPlayerItem(AllItem itemEnum, int itemNumber, bool itemIsEquipmentOrNot, int equipmentDurability)
    {
        if (itemIsEquipmentOrNot)
        {
            playerBag.itemList.Find(n => n.itemEnum == itemEnum).equipmentInfo.equipmentCurrentDurability += equipmentDurability;
            return;
        }
        playerBag.itemList.Find(n => n.itemEnum == itemEnum).itemAmount += itemNumber;
    }
    public void SwitchPlayerPage()
    {
        SoundManager.instance.PlaySound(slotSound);
        bool switchOnOff = inventoryPage.activeSelf;
        inventoryPage.SetActive(!switchOnOff);
        playerInfoPage.SetActive(switchOnOff);        
    }
    /// <summary>
    /// 當玩家點選背包物品時，物品面板要顯示詳情
    /// </summary>
    /// <param name="index">包包的第幾樣物品</param>
    public void ShowItemInfo(int index)
    {
        currentSlotIndex = index;
        if (playerBag.itemList[index].itemIsEmptySlotOrNot)
        {
            itemDiscardPanel.SetActive(false);
            itemName.text = string.Empty;
            itemNumber.text = string.Empty;
            itemDescription.text = string.Empty;
            return;
        }
        itemDiscardPanel.SetActive(true);
        itemDiscardSlider.value = 0;
        itemDiscardSlider.minValue = 0;
        itemDiscardSlider.maxValue = playerBag.itemList[index].itemAmount;
        SoundManager.instance.PlaySound(slotSound);
        itemName.text = playerBag.itemList[index].itemName;
        itemNumber.text = playerBag.itemList[index].itemIsEquipmentOrNot
            ? "Durability: " + playerBag.itemList[index].equipmentInfo.equipmentCurrentDurability.ToString() 
            : "Amount: " + playerBag.itemList[index].itemAmount.ToString();
        itemDescription.text = playerBag.itemList[index].itemDescription;
    }
    public void DiscardItem()
    {
        playerBag.itemList[currentSlotIndex].itemAmount -= (int)itemDiscardSlider.value;
        ShowItemInfo(currentSlotIndex);
        if (!playerBag.itemList[currentSlotIndex].itemIsEquipmentOrNot) return;
        playerBag.itemList[currentSlotIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
        playerBag.itemList[currentSlotIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
    }
    private void AddItemToPlayerBag()
    {
        if (playerInventoryForTransfer.promptItemList.Count <= 0) return;
        #region 包包沒有該物品
        if (firstEmptySlot() == null && playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum) == null)
        {
            Debug.Log("InventoryFull");
        }
        #endregion 包包沒有該物品
        #region 包包已裡有該物品
        else if (playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum) != null)
        {
            playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum).itemAmount++;
        }
        #endregion 包包已裡有該物品
        #region 是新物品
        else firstEmptySlot().Initialize(firstPrompt);
        #endregion 是新物品
        playerInventoryForTransfer.promptItemList.Remove(firstPrompt);
    }
    private void DeveloperAddItemToPlayerBag(InventoryItem_SO developerOnly)
    {
        if (developerOnly == null)
        {
            Debug.Log("請開發者在InventoryManager放入物品");
            return;
        }
        if (playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum) != null)
        {
            if (developerOnly.itemIsEquipmentOrNot)
            {
                playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum).equipmentInfo.equipmentCurrentDurability += developerOnly.equipmentInfo.equipmentCurrentDurability;
                Debug.Log($"開發者已將包包內物品{developerOnly.itemName}的耐久值提高了{developerOnly.equipmentInfo.equipmentCurrentDurability}");
            }
            else
            {
                playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum).itemAmount += developerOnly.itemAmount;
                Debug.Log($"開發者已將包包內物品{developerOnly.itemName}的數量新增了{developerOnly.itemAmount}");
            }
            return;
        }
        firstEmptySlot().Initialize(developerOnly, true);
    }
    private void ManagementPlayerBag()
    {
        foreach (var item in playerBag.itemList)
        {
            if (item.itemAmount == 0 && !item.itemIsEmptySlotOrNot)
            {
                item.Initialize();
            }
        }
        while(playerBag.itemList.Count > PlayerInventorySetting.PlayerInventoryCapacity)
        {
            playerBag.itemList.RemoveAt(PlayerInventorySetting.PlayerInventoryCapacity);
        }
    }
    private void ShowEquipmentOnInventory()
    {
        secondaryEquipment1InputText.text = InputsManager.instance.defaultKeyBinding[5];
        secondaryEquipment2InputText.text = InputsManager.instance.defaultKeyBinding[6];
        secondaryEquipment3InputText.text = InputsManager.instance.defaultKeyBinding[7];
        secondaryEquipment4InputText.text = InputsManager.instance.defaultKeyBinding[8];
        InventoryItem_SO mainEquipment = playerBag.itemList.Find(n => n.equipmentInfo.equipmentIsEquippedAsMainOrNot == true);
        InventoryItem_SO secondaryEquipment1 = playerBag.itemList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary1OrNot == true);
        InventoryItem_SO secondaryEquipment2 = playerBag.itemList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary2OrNot == true);
        InventoryItem_SO secondaryEquipment3 = playerBag.itemList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary3OrNot == true);
        InventoryItem_SO secondaryEquipment4 = playerBag.itemList.Find(n => n.equipmentInfo.equipmantIsEquippedAsSecondary4OrNot == true);
        if (mainEquipment != null)
        {
            this.mainEquipmentImage.sprite = mainEquipment.itemIcon;
            this.mainEquipmentImage.enabled = true;
        }
        else this.mainEquipmentImage.enabled = false;

        if (secondaryEquipment1 != null)
        {
            secondaryEquipment1Image.sprite = secondaryEquipment1.itemIcon;
            secondaryEquipment1Image.enabled = true;
        }
        else this.secondaryEquipment1Image.enabled = false;

        if (secondaryEquipment2 != null)
        {
            secondaryEquipment2Image.sprite = secondaryEquipment2.itemIcon;
            secondaryEquipment2Image.enabled = true;
        }
        else this.secondaryEquipment2Image.enabled = false;

        if (secondaryEquipment3 != null)
        {
            secondaryEquipment3Image.sprite = secondaryEquipment3.itemIcon;
            secondaryEquipment3Image.enabled = true;
        }
        else this.secondaryEquipment3Image.enabled = false;

        if (secondaryEquipment4 != null)
        {
            secondaryEquipment4Image.sprite = secondaryEquipment4.itemIcon;
            secondaryEquipment4Image.enabled = true;
        }
        else this.secondaryEquipment4Image.enabled = false;
    }
    private void OnOffInventorySlots()
    {
        for (int i = 0; i < PlayerInventorySetting.PlayerInventoryCapacity; i++)
        {
            if (i < playerBag.itemList.Count)
            {
                inventorySlotsItem[i].GetComponent<Image>().color = playerBag.itemList[i].itemColor;
                inventorySlotsItem[i].GetComponent<Image>().sprite = playerBag.itemList[i].itemIcon;
                inventorySlotsItem[i].GetComponent<Image>().enabled = true;
                inventorySlotsItem[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                inventorySlotsItem[i].GetComponent<Image>().enabled = false;
                inventorySlotsItem[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    /// <summary>
    /// 檢測物品不在包包裡且包包已滿
    /// </summary>
    /// <param name="itemEnum">要檢測物品</param>
    /// <returns>物品不能放進包包則傳回true</returns>
    public bool inventoryFull(AllItem itemEnum)
    {
        if (playerBag.itemList.Find(n => n.itemEnum == itemEnum) == null && firstEmptySlot() == null) return true;
        return false;
    }
    /// <summary>
    /// 檢測物品不在包包裡但包包未滿
    /// </summary>
    /// <param name="itemEnum">要檢測物品</param>
    /// <returns>物品不在包包裡且包包未滿則傳回true</returns>
    public bool inventoryNotFull(AllItem itemEnum)
    {
        if (playerBag.itemList.Find(n => n.itemEnum == itemEnum) == null && firstEmptySlot() != null) return true;
        return false;
    }
    public InventoryItem_SO firstEmptySlot()
    {
        return playerBag.itemList.Find(n => n.itemIsEmptySlotOrNot == true);
    }
    public InventoryItem_SO findItem(AllItem item)
    {
        return playerBag.itemList.Find(n => n.itemEnum == item);
    }
    private void Update()
    {
        #region 開發者
        if (firstEmptySlot() != null && PlayerInput.mainEquipmentInput && Input.GetKeyDown(KeyCode.D)) DeveloperAddItemToPlayerBag(developUse);
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.Delete))
        {
#if UNITY_EDITOR
            AssetDatabase.StartAssetEditing();
            EditorUtility.SetDirty(playerBag);
            foreach (var item in playerBag.itemList)
            {
                item.Initialize();
                EditorUtility.SetDirty(item);
            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
            Debug.Log("開發者已將背包清除");
#endif
        }
        #endregion 開發者
        ShowEquipmentOnInventory();
        ManagementPlayerBag();
        AddItemToPlayerBag();
        OnOffInventorySlots();
        if (itemDiscardPanel.activeSelf) itemDiscardAmount.text = itemDiscardSlider.value.ToString();
    }
}