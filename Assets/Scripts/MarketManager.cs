using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MarketManager : Singleton<MarketManager>, IDialog
{
    public const int currentPanelAmount = 5;
    [Tooltip("玩家")] public Player player;
    [Space(10)]
    [Header("頁面")]
    [Tooltip("所有的頁面標籤")]public Button[] ribbonPage;
    [Tooltip("所有的頁面")]public GameObject[] allPanelPage;
    [Tooltip("目前的頁面")] private MarketPage currentPage;
    [Tooltip("任務頁面")][SerializeField] private QuestPanel questPanel;
    [Space(10)]
    [Header("市場介面")]
    [Tooltip("目前介面Title")] public TextMeshProUGUI menuPage;
    [Space(10)]
    [Header("Repair頁面")]
    [Tooltip("顯示主要裝備的Image")] public Image repairSlotImage;
    [Tooltip("主要裝備修復等級面板，包括Helper")] public GameObject fixing;
    [Tooltip("顯示主要裝備是否為Corrupted面板")] public GameObject corruptedIndicator;
    [Tooltip("修復主要裝備需要的物品的文字")] public TextMeshProUGUI repairItem;
    [Tooltip("修復主要裝備需要的物品數量的文字")] public TextMeshProUGUI repairItemAmount;
    [Space(10)]
    [Header("商人需求 & 玩家所持")]
    [Tooltip("顯示商人的需求的文字")] public TextMeshProUGUI merchantOffer;
    [Tooltip("顯示玩家所持的文字")] public TextMeshProUGUI playerProperty;
    [Space(10)]
    [Header("對話")]
    [Tooltip("對話條1")] public TextMeshProUGUI merchantDialog;
    [Tooltip("對話條2")] public TextMeshProUGUI merchantDialogSecond;
    [Space(10)]
    [Header("其他")]
    [Tooltip("這是玩家的背包")] public ItemListSorted_SO playerBag;
    [Tooltip("市場的交易確認按鈕")] public Button confirmButton;
    [Tooltip("這是目前玩家所選擇的商品")] private OfferSlot currentMerchandiseInfo;
    [Tooltip("這是開關對話條1&2用的")]private bool dialogSwitch;
    [Tooltip("任務頁面的Slot的index")] private int currentSlotIndex;
    [Tooltip("用來檢測玩家是否按下修復Button用的")]private bool repairReady = false;
    [Tooltip("玩家按下哪個修復等級用的")]private int repairLevel;
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
    /// 這個是切換商人介面，給Button OnClick()用的
    /// </summary>
    /// <param name="text">這個是在button裡設置的</param>
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
    /// 搜尋物品數量用
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
        //物品不夠
        if (requiringItemFromBag == null || currentMerchandiseInfo.tradeRequire.requireItemAmount > requiringItemFromBag.itemAmount)
        {
            DialogStart(TestText3, 0.03f);
            return;
        }
        //如果包包沒有該物品
        if (tradingItemFromBag == null)
        {
            if (InventoryManager.instance.firstEmptySlot() != null)
            {
                InventoryManager.instance.firstEmptySlot().Initialize(currentMerchandiseInfo.tradeRequire.merchandiseItemSO, false);
            }
            else
            {
                //包包已滿
                DialogStart(TestText, 0.03f);
                return;
            }
        }
        //包包有該物品
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
    #region 對話系統
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
    #endregion 對話系統
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