using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MarketManager : Singleton<MarketManager>, IDialog
{
    public const int currentPanelAmount = 5;
    [Tooltip("���a")] public Player player;
    [Space(10)]
    [Header("����")]
    [Tooltip("�Ҧ�����������")]public Button[] ribbonPage;
    [Tooltip("�Ҧ�������")]public GameObject[] allPanelPage;
    [Tooltip("�ثe������")] private MarketPage currentPage;
    [Tooltip("���ȭ���")][SerializeField] private QuestPanel questPanel;
    [Space(10)]
    [Header("��������")]
    [Tooltip("�ثe����Title")] public TextMeshProUGUI menuPage;
    [Space(10)]
    [Header("Repair����")]
    [Tooltip("��ܥD�n�˳ƪ�Image")] public Image repairSlotImage;
    [Tooltip("�D�n�˳ƭ״_���ŭ��O�A�]�AHelper")] public GameObject fixing;
    [Tooltip("��ܥD�n�˳ƬO�_��Corrupted���O")] public GameObject corruptedIndicator;
    [Tooltip("�״_�D�n�˳ƻݭn�����~����r")] public TextMeshProUGUI repairItem;
    [Tooltip("�״_�D�n�˳ƻݭn�����~�ƶq����r")] public TextMeshProUGUI repairItemAmount;
    [Space(10)]
    [Header("�ӤH�ݨD & ���a�ҫ�")]
    [Tooltip("��ܰӤH���ݨD����r")] public TextMeshProUGUI merchantOffer;
    [Tooltip("��ܪ��a�ҫ�����r")] public TextMeshProUGUI playerProperty;
    [Space(10)]
    [Header("���")]
    [Tooltip("��ܱ�1")] public TextMeshProUGUI merchantDialog;
    [Tooltip("��ܱ�2")] public TextMeshProUGUI merchantDialogSecond;
    [Space(10)]
    [Header("��L")]
    [Tooltip("�o�O���a���I�]")] public ItemListSorted_SO playerBag;
    [Tooltip("����������T�{���s")] public Button confirmButton;
    [Tooltip("�o�O�ثe���a�ҿ�ܪ��ӫ~")] private OfferSlot currentMerchandiseInfo;
    [Tooltip("�o�O�}����ܱ�1&2�Ϊ�")]private bool dialogSwitch;
    [Tooltip("���ȭ�����Slot��index")] private int currentSlotIndex;
    [Tooltip("�Ψ��˴����a�O�_���U�״_Button�Ϊ�")]private bool repairReady = false;
    [Tooltip("���a���U���ӭ״_���ťΪ�")]private int repairLevel;
    private string TestText = "Your Bag Is Full";
    private string TestText3 = "Don't have enough items";
    private string TestText4 = "Thank You";
    private string TestText5 = "Equipment repaired";
    private string TestText6 = "You've unlocked new thing!";
    private bool level10Reached = false;
    private bool level20Reached = false;
    private void Start()
    {
        ChangePage(0);
        if (player.playerLevel >= 10 && !level10Reached)
        {
            DialogStart(TestText6, 0.05f);
            level10Reached = true;
        }
        if (player.playerLevel >= 20 && !level20Reached)
        {
            DialogStart(TestText6, 0.05f);
            level20Reached = true;
        }
    }
    /// <summary>
    /// �o�ӬO�����ӤH�����A��Button OnClick()�Ϊ�
    /// </summary>
    /// <param name="text">�o�ӬO�bbutton�̳]�m��</param>
    public void ChangePage(int index)
    {
        playerProperty.text = string.Empty;
        merchantOffer.text = string.Empty;
        repairItem.text = string.Empty;
        repairItemAmount.text = string.Empty;
        for (int i = 0; i < currentPanelAmount; i++)
        {
            if (i == index)
            {
                ribbonPage[i].interactable = false;
                allPanelPage[i].SetActive(true);
            }
            else
            {
                ribbonPage[i].interactable = true;
                allPanelPage[i].SetActive(false);
            }
        }
        confirmButton.onClick.RemoveAllListeners();
        switch (index)
        {
            case 0:
                currentPage = MarketPage.TRADE;
                confirmButton.onClick.AddListener(TradeConfirm_Trade);
                break;
            case 1:
                currentPage = MarketPage.WEAPON;
                break;
            case 2:
                currentPage = MarketPage.SPELL;
                break;
            case 3:
                currentPage = MarketPage.REPAIR;
                confirmButton.onClick.AddListener(TradeConfirm_Repair);
                break;
            case 4:
                currentPage = MarketPage.QUEST;
                confirmButton.onClick.AddListener(TradeConfirm_Quest);
                break;
            default:
                break;
        }
        menuPage.text = currentPage.ToString();
    }
    private void ShowTradeInfo()
    {
        merchantOffer.text = $"You need {currentMerchandiseInfo.tradeRequire.requireItemAmount} {currentMerchandiseInfo.tradeRequire.merchandiseRequireItemName}";
        playerProperty.text = $"You have {FindItemAmountInPlayerBag(currentMerchandiseInfo.tradeRequire.merchandiseRequireItem)} {currentMerchandiseInfo.tradeRequire.merchandiseRequireItemName}";

    }
    public void ShowTradeInfo(OfferSlot offer)
    {
        currentMerchandiseInfo = offer;
        currentSlotIndex = offer.transform.GetSiblingIndex();
        if (offer.tradeRequire.isMerchandiseMissionOrNot)
        {
            merchantOffer.text = $"{currentMerchandiseInfo.tradeRequire.mission.missionName}";
            playerProperty.text = string.Empty;
            return;
        }
        merchantOffer.text = $"You need {offer.tradeRequire.requireItemAmount} {offer.tradeRequire.merchandiseRequireItemName}";
        playerProperty.text = $"You have {FindItemAmountInPlayerBag(offer.tradeRequire.merchandiseRequireItem)} {offer.tradeRequire.merchandiseRequireItemName}";
    }
    /// <summary>
    /// �j�M���~�ƶq��
    /// </summary>
    private int FindItemAmountInPlayerBag(AllItem item)
    {
        if (playerBag.itemList.Find(n => n.itemEnum == item) == null) return 0;
        return playerBag.itemList.Find(n => n.itemEnum == item).itemAmount;
    }
    public void TradeConfirm_Trade()
    {
        InventoryItem_SO tradingItemFromBag = playerBag.itemList.Find(n => n.itemEnum == currentMerchandiseInfo.tradeRequire.merchandiseItemSO.itemEnum);
        InventoryItem_SO requiringItemFromBag = playerBag.itemList.Find(n => n.itemEnum == currentMerchandiseInfo.tradeRequire.merchandiseRequireItem);
        //���~����
        if (requiringItemFromBag == null || currentMerchandiseInfo.tradeRequire.requireItemAmount > requiringItemFromBag.itemAmount)
        {
            DialogStart(TestText3, 0.03f);
            return;
        }
        //�p�G�]�]�S���Ӫ��~
        if (tradingItemFromBag == null)
        {
            if (InventoryManager.instance.firstEmptySlot() != null)
            {
                InventoryManager.instance.firstEmptySlot().Initialize(currentMerchandiseInfo.tradeRequire.merchandiseItemSO, false);
            }
            else
            {
                //�]�]�w��
                DialogStart(TestText, 0.03f);
                return;
            }
        }
        //�]�]���Ӫ��~
        else
        {
            if (currentMerchandiseInfo.tradeRequire.merchandiseItemSO.itemIsEquipmentOrNot)
            {
                InventoryManager.instance.findItem(currentMerchandiseInfo.tradeRequire.merchandiseItemSO.itemEnum).equipmentInfo.equipmentCurrentDurability +=
                    currentMerchandiseInfo.tradeRequire.merchandiseItemSO.equipmentInfo.equipmentCurrentDurability;
                return;
            }
            InventoryManager.instance.findItem(currentMerchandiseInfo.tradeRequire.merchandiseItemSO.itemEnum).itemAmount++;
        }
        requiringItemFromBag.itemAmount -= currentMerchandiseInfo.tradeRequire.requireItemAmount;
        DialogStart(TestText4, 0.03f);
        ShowTradeInfo();
    }
    public void TradeConfirm_Repair()
    {
        if (!player.isPlayerMainEquipmentEquipped || !repairReady) return;
        InventoryItem_SO repairItem = playerBag.itemList.Find(n => n.itemEnum == player.playerMainEquipmentRepairItem);
        if (repairItem == null || repairItem.itemAmount < player.playerMainEquipmentRepairItemAmount * repairLevel)
        {
            DialogStart(TestText3, 0.03f);
            return;
        }
        player.EquipmentRepair((int)player.playerMainEquipmentCorruptingDurability * repairLevel);
        InventoryManager.instance.findItem(player.playerMainEquipmentRepairItem).itemAmount -= player.playerMainEquipmentRepairItemAmount;
        DialogStart(TestText5, 0.03f);
        repairReady = false;
    }
    public void TradeConfirm_Quest()
    {
        if (currentMerchandiseInfo == null) return;
        if (currentMerchandiseInfo.tradeRequire.isMerchandiseMissionOrNot)
        {
            MissionList.missionList.Add(currentMerchandiseInfo.tradeRequire.mission);
            questPanel.missions.tradeOffer.RemoveAt(currentSlotIndex);
            Destroy(currentMerchandiseInfo.gameObject);
            merchantOffer.text = string.Empty;
            return;
        }
    }
    public void LeavingMarket()
    {
        GameManager.instance.LoadMapScene();
    }
    #region ��ܨt��
    public void DialogStart(string content, float time)
    {
        dialogSwitch = !dialogSwitch;
        if (!dialogSwitch) StartCoroutine(ShowDialog(content, time));
        if (dialogSwitch) StartCoroutine(ShowDialogSecond(content, time));
    }
    public IEnumerator ShowDialog(string dialog, float howFastTheDialogIs)
    {
        merchantDialog.gameObject.SetActive(true);
        merchantDialogSecond.gameObject.SetActive(false);
        merchantDialog.text = string.Empty;
        int letter = 0;
        while (letter < dialog.Length && !dialogSwitch)
        {
            merchantDialog.text += dialog[letter];
            letter++;
            yield return new WaitForSeconds(howFastTheDialogIs);
        }
    }
    public IEnumerator ShowDialogSecond(string dialog, float howFastTheDialogIs)
    {
        merchantDialog.gameObject.SetActive(false);
        merchantDialogSecond.gameObject.SetActive(true);
        merchantDialogSecond.text = "";
        int letter = 0;
        while (letter < dialog.Length && dialogSwitch)
        {
            merchantDialogSecond.text += dialog[letter];
            letter++;
            yield return new WaitForSeconds(howFastTheDialogIs);
        }
    }
    #endregion ��ܨt��
    public void EquipmentRepair(int level)
    {
        repairLevel = level;
        repairReady = true;
        repairItem.enabled = true;
        repairItemAmount.enabled = true;
        repairItem.text = player.playerMainEquipmentRepairItem.ToString();
        repairItemAmount.text = (player.playerMainEquipmentRepairItemAmount * level).ToString();
    }
    private void Update()
    {
        if (currentPage == MarketPage.REPAIR)
        {
            if (!player.isPlayerMainEquipmentEquipped)
            {
                repairSlotImage.enabled = false;
                fixing.SetActive(false);
                corruptedIndicator.SetActive(false);
                repairItem.enabled = false;
                repairItemAmount.enabled = false;
                return;
            }
            repairSlotImage.enabled = true;
            repairSlotImage.sprite = player.playerMainWeaponSprite;
            if (player.playerMainEquipmentCurrentDurability < player.playerMainEquipmentCorruptingDurability)
            {
                fixing.SetActive(true);
                corruptedIndicator.SetActive(true);
            }
            else
            {
                fixing.SetActive(false);
                corruptedIndicator.SetActive(false);
            }
            return;
        }
        repairReady = false;
    }
}