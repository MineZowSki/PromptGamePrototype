using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CraftManager : MonoBehaviour, IDialog
{
    [Tooltip("目前CraftPlace有的頁數")] private const int currentPanelAmount = 2;
    [Tooltip("庫存的數量")] public const int maxDepositSlots = 5;
    [Header("主要介面")]
    [Tooltip("目前的頁面的Enum")] private CraftPlacePage currentPage;
    [Tooltip("目前介面Title"), SerializeField] private TextMeshProUGUI menuPage;
    [Tooltip("玩家持有物"), SerializeField] private GameObject playerItem;
    [Tooltip("玩家持有物的捲動框"), SerializeField] private GameObject contentHolder;
    [Tooltip("玩家持有物的List")] private List<CraftItem> allItemInScrollList = new List<CraftItem>();
    [Tooltip("所有的頁面標籤"), SerializeField] private Button[] ribbonPage;
    [Tooltip("所有的頁面的Panel"), SerializeField] private GameObject[] allPanelPage;
    [Tooltip("市場的交易確認按鈕"), SerializeField] private Button confirmButton;
    [Space(10), Header("合成介面")]
    [Tooltip("合成台的空位"), SerializeField] private GameObject[] craftSlots;
    [Tooltip("合成時的跑條"), SerializeField] private Image craftProgressBar;
    [Tooltip("合成出來的物品圖片"), SerializeField] private Image craftCompleteSlot;
    [Tooltip("目前在合成台的第幾空位")] private int currentCraftSlotIndex;
    [Tooltip("玩家選擇的被合成物")] private InventoryItem_SO currentCraftItem;
    [Tooltip("合成台中的物品的List"), SerializeField] private List<AllItem> allItemInCraftSlot = new List<AllItem>();
    [Space(10), Header("Deposit介面")]
    [Tooltip("庫存的空位"), SerializeField] private Image[] depositSlots;
    [Tooltip("庫存的List"), SerializeField] private List<InventoryItem_SO> depositList = new List<InventoryItem_SO>();
    [Tooltip("物品顯示的文字"), SerializeField] private TextMeshProUGUI depositItem;
    [Tooltip("物品數量顯示的文字"), SerializeField] private TextMeshProUGUI depositItemAmount;
    [Tooltip("玩家選擇了庫存的第幾空位")] private int currentDepositIndex;
    [Space(10), Header("對話系統")]
    [Tooltip("對話條1")] public TextMeshProUGUI crafterDialog;
    [Tooltip("對話條2")] public TextMeshProUGUI crafterDialogSecond;
    [Tooltip("這是開關對話條1&2用的")] private bool dialogSwitch;
    [Space(10), Header("音效相關")]
    [Tooltip("合成時的音效"), SerializeField] private AudioClip progressSound;
    [Space(10), Header("所有物品")]
    [Tooltip("所有能被合成出來的物品的List"), SerializeField] private List<InventoryItem_SO> allCraftableItem = new List<InventoryItem_SO>();
    private float duration = 2f;
    private bool isCrafting;
    private void Start()
    {
        ChangePage(0);
        currentCraftSlotIndex = 0;
    }
    public void ChangePage(int index)
    {
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
                currentPage = CraftPlacePage.CRAFT;
                depositItem.enabled = false;
                depositItemAmount.enabled = false;
                SpawnPlayerItems();
                confirmButton.onClick.AddListener(ConfirmCraft);
                break;
            case 1:
                currentPage = CraftPlacePage.DEPOSIT;
                StartCoroutine(RemoveSpawnedItem());
                currentDepositIndex = -1;
                confirmButton.onClick.AddListener(ConfirmDeposit);
                break;
            default:
                break;
        }
        menuPage.text = currentPage.ToString();
    }
    private void SpawnPlayerItems()
    {
        contentHolder.SetActive(true);
        foreach (var item in InventoryManager.instance.playerBag.itemList)
        {
            if (item.itemIsEmptySlotOrNot) continue;
            var spawnItem = Instantiate(playerItem, contentHolder.transform);
            allItemInScrollList.Add(spawnItem.GetComponent<CraftItem>());
            spawnItem.GetComponent<CraftItem>().craftItem = item;
            spawnItem.GetComponent<Image>().sprite = item.itemIcon;
            spawnItem.GetComponent<Button>().onClick.AddListener(() => AddItemToCraftSlot(spawnItem.GetComponent<CraftItem>()));
        }
    }
    private IEnumerator RemoveSpawnedItem()
    {
        allItemInScrollList.Clear();
        contentHolder.SetActive(false);
        while (contentHolder.transform.childCount != 0)
        {
            Destroy(contentHolder.transform.GetChild(0).gameObject);
            yield return null;
        }
    }
    private void AddItemToCraftSlot(CraftItem craftItem)
    {
        if (currentCraftSlotIndex > 4)
        {
            DialogStart("Craft Slots are full", 0.03f);
            return;
        }
        currentCraftItem = craftItem.craftItem;
        allItemInCraftSlot.Add(craftItem.craftItem.itemEnum);
        if (craftItem.craftItem.itemAmount - allItemInCraftSlot.FindAll(n => n == craftItem.craftItem.itemEnum).ToList().Count == 0)
        {
            allItemInScrollList.Find(n => n.craftItem.itemEnum == currentCraftItem.itemEnum).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            allItemInScrollList.Find(n => n.craftItem.itemEnum == currentCraftItem.itemEnum).GetComponent<Button>().interactable = false;
        }
        craftSlots[currentCraftSlotIndex].transform.GetChild(0).GetComponent<Image>().sprite = craftItem.craftItem.itemIcon;
        craftSlots[currentCraftSlotIndex].transform.GetChild(0).GetComponent<Image>().enabled = true;
        craftSlots[currentCraftSlotIndex].GetComponent<CraftSlot>().craftElement = craftItem.craftItem.itemEnum;
        currentCraftSlotIndex++;
    }
    public void CancelChoice()
    {
        if (currentCraftSlotIndex == 0) return;
        if (allItemInCraftSlot.Count != 0)
        {
            allItemInScrollList.Find(n => n.craftItem.itemEnum == allItemInCraftSlot[allItemInCraftSlot.Count - 1]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            allItemInScrollList.Find(n => n.craftItem.itemEnum == allItemInCraftSlot[allItemInCraftSlot.Count - 1]).GetComponent<Button>().interactable = true;
            allItemInCraftSlot.RemoveAt(allItemInCraftSlot.Count - 1);
        }
        craftSlots[currentCraftSlotIndex - 1].transform.GetChild(0).GetComponent<Image>().enabled = false;
        craftSlots[currentCraftSlotIndex - 1].GetComponent<CraftSlot>().craftElement = AllItem.None;
        currentCraftSlotIndex--;
    }
    public void ConfirmCraft()
    {
        if (allItemInCraftSlot.Count == 0) return;
        else if (findEmptyDeposit() == null)
        {
            DialogStart("Your Deposit is full", 0.03f);
            return;
        }
        StartCoroutine(CraftProgressBar());
    }
    public void ConfirmDeposit()
    {
        if (depositList.Count == 0 || currentDepositIndex == -1) return;
        else if (InventoryManager.instance.firstEmptySlot() == null)
        {
            DialogStart("Inventory Full", 0.05f);
            return;
        }
        depositItem.enabled = false;
        depositItemAmount.enabled = false;
        if (InventoryManager.instance.findItem(depositList[currentDepositIndex].itemEnum) != null)
        {
            InventoryManager.instance.findItem(depositList[currentDepositIndex].itemEnum).itemAmount += depositList[currentDepositIndex].itemAmount;
        }
        else InventoryManager.instance.firstEmptySlot().Initialize(depositList[currentDepositIndex]);
        depositList[currentDepositIndex].Initialize();
    }
    private IEnumerator CraftProgressBar()
    {
        isCrafting = true;
        SoundManager.instance.PlaySound(progressSound, 0.3f);
        float elapsedTime = 0f;
        float startFillAmount = 0f;
        float targetFillAmount = 1f;
        while (elapsedTime < duration)
        {
            float currentFillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / duration);
            craftProgressBar.fillAmount = currentFillAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        craftProgressBar.fillAmount = targetFillAmount;
        SoundManager.instance.StopSound();
        CraftComplete();
    }
    //TODO: 合成物品系統
    private InventoryItem_SO craftedItem()
    {
        if (allItemInCraftSlot[0] == AllItem.AquaStone) return allCraftableItem.Find(n => n.itemEnum == AllItem.Mica);
        return allCraftableItem.Find(n => n.itemEnum == AllItem.Mica);
        //return null;
    }
    private void CraftComplete()
    {
        InventoryItem_SO newItem = craftedItem();
        craftCompleteSlot.sprite = newItem.itemIcon;
        if (findDeposit(newItem) != null) findDeposit(newItem).itemAmount += newItem.itemAmount;
        else findEmptyDeposit().Initialize(newItem, false);
        foreach (var item in allItemInCraftSlot)
        {
            InventoryManager.instance.findItem(item).itemAmount--;
        }
        foreach (var member in craftSlots)
        {
            member.transform.GetChild(0).GetComponent<Image>().enabled = false;
            member.GetComponent<CraftSlot>().craftElement = AllItem.None;
        }
        currentCraftSlotIndex = 0;
        DialogStart($"Hooray! We crafted a {newItem.itemName}", 0.05f);
        craftProgressBar.fillAmount = 0f;
        isCrafting = false;
        allItemInCraftSlot.Clear();
    }
    private InventoryItem_SO findDeposit(InventoryItem_SO craftedItem)
    {
        return depositList.Find(n => n.itemEnum == craftedItem.itemEnum);
    }
    private InventoryItem_SO findEmptyDeposit()
    {
        return depositList.Find(n => n.itemIsEmptySlotOrNot == true);
    }
    /// <summary>
    /// 用在Deposit的button
    /// </summary>
    /// <param name="index"></param>
    public void ChooseDeposit(int index)
    {
        currentDepositIndex = index;
        depositItem.enabled = true;
        depositItem.text = depositList[index].itemName;
        depositItemAmount.enabled = true;
        depositItemAmount.text = depositList[index].itemAmount.ToString();
    }
    private void Update()
    {
        if (isCrafting)
        {
            List<GameObject> content = new List<GameObject>();
            for(int i = 0; i < contentHolder.transform.childCount; i++)
            {
                content.Add(contentHolder.transform.GetChild(i).gameObject);
            }
            foreach (var item in content)
            {
                item.GetComponent<Button>().enabled = false;
                item.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        else
        {
            List<GameObject> content = new List<GameObject>();
            for (int i = 0; i < contentHolder.transform.childCount; i++)
            {
                content.Add(contentHolder.transform.GetChild(i).gameObject);
            }
            foreach (var item in content)
            {
                item.GetComponent<Button>().enabled = true;
                item.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        if (currentPage == CraftPlacePage.DEPOSIT)
        {
            for (int i = 0; i < maxDepositSlots; i++)
            {
                if (depositList[i].itemIsEmptySlotOrNot)
                {
                    depositSlots[i].GetComponent<Button>().interactable = false;
                    depositSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    continue;
                }
                depositSlots[i].GetComponent<Button>().interactable = true;
                depositSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                depositSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = depositList[i].itemIcon;
            }
        }
    }
    public void LeaveCraftPlace()
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
        crafterDialog.gameObject.SetActive(true);
        crafterDialogSecond.gameObject.SetActive(false);
        crafterDialog.text = string.Empty;
        int letter = 0;
        while (letter < dialog.Length && !dialogSwitch)
        {
            crafterDialog.text += dialog[letter];
            letter++;
            yield return new WaitForSeconds(howFastTheDialogIs);
        }
    }
    public IEnumerator ShowDialogSecond(string dialog, float howFastTheDialogIs)
    {
        crafterDialog.gameObject.SetActive(false);
        crafterDialogSecond.gameObject.SetActive(true);
        crafterDialogSecond.text = "";
        int letter = 0;
        while (letter < dialog.Length && dialogSwitch)
        {
            crafterDialogSecond.text += dialog[letter];
            letter++;
            yield return new WaitForSeconds(howFastTheDialogIs);
        }
    }
    #endregion 對話系統
}