using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : PersistentSingleton<InventoryManager>
{
    [Tooltip("Prompt�h�d�����Ĥ@�˪��~")]private Prompts_SO firstPrompt => playerInventoryForTransfer.promptItemList.First();
    [Header("�]�]����")]
    [Tooltip("���a�I�]")] public GameObject inventoryOnOff;
    [Tooltip("���a���~����")]public GameObject inventoryPage;
    [Tooltip("���a��T����")] public GameObject playerInfoPage;
    [Tooltip("���a�˱󪫫~����")] public GameObject itemDiscardPanel;
    [Tooltip("���a�I�]��l�����~")]public GameObject[] inventorySlotsItem;
    [Space(10)]
    [Header("�]�]���~�Ա����")]
    [Tooltip("���~�W��")]public TextMeshProUGUI itemName;
    [Tooltip("���~��T")] public TextMeshProUGUI itemDescription;
    [Tooltip("���~�ƶq")] public TextMeshProUGUI itemNumber;
    [Tooltip("���~�˱��")] public Slider itemDiscardSlider;
    [Tooltip("���~�˱�ƶq")] public TextMeshProUGUI itemDiscardAmount;
    [Space(10)]
    [Header("�]�]���a��T���")]
    [Tooltip("���a����")] public TextMeshProUGUI playerLevel;
    [Tooltip("���a�g���")] public TextMeshProUGUI playerEXP;
    [Tooltip("���a�`�����ƶq")] public TextMeshProUGUI playerMistake;
    [Space(10)]
    [Header("�I�]�˳����")]
    [Tooltip("�D�n�˳�")] public Image mainEquipmentImage;
    [Tooltip("���n�˳�1")] public Image secondaryEquipment1Image;
    [Tooltip("���n�˳�2")] public Image secondaryEquipment2Image;
    [Tooltip("���n�˳�3")] public Image secondaryEquipment3Image;
    [Tooltip("���n�˳�4")] public Image secondaryEquipment4Image;
    [Tooltip("���n�˳�1���ֱ��䪺Text")] public TextMeshProUGUI secondaryEquipment1InputText;
    [Tooltip("���n�˳�2���ֱ��䪺Text")] public TextMeshProUGUI secondaryEquipment2InputText;
    [Tooltip("���n�˳�3���ֱ��䪺Text")] public TextMeshProUGUI secondaryEquipment3InputText;
    [Tooltip("���n�˳�4���ֱ��䪺Text")] public TextMeshProUGUI secondaryEquipment4InputText;
    [Space(10)]
    [Header("SO����")]
    [Tooltip("�o�̬OPrompt���h�d�Ȧs��")]public PlayerInventory_SO playerInventoryForTransfer;
    [Tooltip("�o�ӬO���a�]�]")]public ItemListSorted_SO playerBag;
    [Space(10)]
    [Header("�]�]���~��l�ƥ�")]
    [Tooltip("�I�]��l��Sprite")]public Sprite slotSprite;
    [Tooltip("�p�G�SŪ��Resources�̪�Sprite�h�Φ����")]public Sprite slotMissingSprite;
    [Space(10)]
    [Header("���Ĭ���")]
    [Tooltip("���}�I�]����")]public AudioClip bagSound;
    [Tooltip("�I�]��l����")] public AudioClip slotSound;
    [Tooltip("�˳Ư}�a����")] public AudioClip equipmentBreakSound;
    [Space(10)]
    [Header("�}�o�̨ϥΡA�@�묰��")]
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
    /// ���a�I��I�]���~�ɡA���~���O�n��ܸԱ�
    /// </summary>
    /// <param name="index">�]�]���ĴX�˪��~</param>
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
        #region �]�]�S���Ӫ��~
        if (firstEmptySlot() == null && playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum) == null)
        {
            Debug.Log("InventoryFull");
        }
        #endregion �]�]�S���Ӫ��~
        #region �]�]�w�̦��Ӫ��~
        else if (playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum) != null)
        {
            playerBag.itemList.Find(n => n.itemEnum == firstPrompt.promptTransferToItem.itemEnum).itemAmount++;
        }
        #endregion �]�]�w�̦��Ӫ��~
        #region �O�s���~
        else firstEmptySlot().Initialize(firstPrompt);
        #endregion �O�s���~
        playerInventoryForTransfer.promptItemList.Remove(firstPrompt);
    }
    private void DeveloperAddItemToPlayerBag(InventoryItem_SO developerOnly)
    {
        if (developerOnly == null)
        {
            Debug.Log("�ж}�o�̦bInventoryManager��J���~");
            return;
        }
        if (playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum) != null)
        {
            if (developerOnly.itemIsEquipmentOrNot)
            {
                playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum).equipmentInfo.equipmentCurrentDurability += developerOnly.equipmentInfo.equipmentCurrentDurability;
                Debug.Log($"�}�o�̤w�N�]�]�����~{developerOnly.itemName}���@�[�ȴ����F{developerOnly.equipmentInfo.equipmentCurrentDurability}");
            }
            else
            {
                playerBag.itemList.Find(n => n.itemEnum == developerOnly.itemEnum).itemAmount += developerOnly.itemAmount;
                Debug.Log($"�}�o�̤w�N�]�]�����~{developerOnly.itemName}���ƶq�s�W�F{developerOnly.itemAmount}");
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
    /// �˴����~���b�]�]�̥B�]�]�w��
    /// </summary>
    /// <param name="itemEnum">�n�˴����~</param>
    /// <returns>���~�����i�]�]�h�Ǧ^true</returns>
    public bool inventoryFull(AllItem itemEnum)
    {
        if (playerBag.itemList.Find(n => n.itemEnum == itemEnum) == null && firstEmptySlot() == null) return true;
        return false;
    }
    /// <summary>
    /// �˴����~���b�]�]�̦��]�]����
    /// </summary>
    /// <param name="itemEnum">�n�˴����~</param>
    /// <returns>���~���b�]�]�̥B�]�]�����h�Ǧ^true</returns>
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
        #region �}�o��
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
            Debug.Log("�}�o�̤w�N�I�]�M��");
#endif
        }
        #endregion �}�o��
        ShowEquipmentOnInventory();
        ManagementPlayerBag();
        AddItemToPlayerBag();
        OnOffInventorySlots();
        if (itemDiscardPanel.activeSelf) itemDiscardAmount.text = itemDiscardSlider.value.ToString();
    }
}