using Newtonsoft.Json;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "_Item_", menuName = "Item/Item")]
public class InventoryItem_SO : ScriptableObject
{
    public bool itemIsEmptySlotOrNot;
    [JsonIgnore]
    public Sprite itemIcon;
    public string itemIconPath => $"UI/ForScript/{itemResourcesFolderName}";
    [JsonIgnore]
    public Color itemColor = new Color(255f, 255f, 255f, 255f);
    public float colorR => itemColor.r;
    public float colorG => itemColor.g;
    public float colorB => itemColor.b;
    public float colorA => itemColor.a;
    public bool itemIsCurrentlyObatainableOrNot = true;
    public AllItem itemEnum;
    public string itemName;
    [Tooltip("要根據Resources/ForScript/UI裡的名稱填入")]public string itemResourcesFolderName;
    public int itemAmount = 0;
    public string itemDescription;
    public bool itemIsEquipmentOrNot;
    public Equipment_SO equipmentInfo;
    /// <summary>
    /// 完全初始化aka使背包空格變空白
    /// </summary>
    public void Initialize()
    {
        this.itemIsEmptySlotOrNot = true;
        this.itemIcon = InventoryManager.instance.slotSprite;
        this.itemColor = new Color(255f, 255f, 255f, 255f);
        this.itemIsCurrentlyObatainableOrNot = true;
        this.itemEnum = AllItem.None;
        this.itemName = string.Empty;
        this.itemAmount = 0;
        this.itemDescription = string.Empty;
        this.itemIsEquipmentOrNot = false;
        if (equipmentInfo != null) this.equipmentInfo.Initialize();
    }
    /// <summary>
    /// DATA使用
    /// </summary>
    /// <param name="itemData"></param>
    public void Initialize(ItemData itemData)
    {
        this.itemIsEmptySlotOrNot = itemData.itemIsEmptySlotOrNot;
        if (itemData.itemIsEmptySlotOrNot) this.itemIcon = InventoryManager.instance.slotSprite;
        else
        {
            if (Resources.LoadAll<Sprite>(itemData.itemIconPath) == null) this.itemIcon = InventoryManager.instance.slotMissingSprite;
            else if (Resources.LoadAll<Sprite>(itemData.itemIconPath).Length == 1) this.itemIcon = Resources.Load<Sprite>(itemData.itemIconPath);
            else if (Resources.LoadAll<Sprite>(itemData.itemIconPath).ToList().Find(n => n.name == itemData.itemName) == null) this.itemIcon = InventoryManager.instance.slotMissingSprite;
            else this.itemIcon = Resources.LoadAll<Sprite>(itemData.itemIconPath).ToList().Find(n => n.name == itemData.itemName);
        }
        this.itemColor = new Color(itemData.itemColorR, itemData.itemColorG, itemData.itemColorB, itemData.itemColorA);
        this.itemIsCurrentlyObatainableOrNot = itemData.itemIsCurrentlyObatainableOrNot;
        this.itemEnum = itemData.itemEnum;
        this.itemName = itemData.itemName;
        this.itemAmount = itemData.itemNumber;
        this.itemDescription = itemData.itemDescription;
        this.itemIsEquipmentOrNot = itemData.itemIsEquipmentOrNot;
        if (!itemData.itemIsEquipmentOrNot) return;
        this.equipmentInfo.Initialize(itemData);
    }
    /// <summary>
    /// 用SO初始SO,不包括裝備
    /// </summary>
    /// <param name="newItem">新物品</param>
    public void Initialize(InventoryItem_SO newItem)
    {
        if (newItem.itemIsEquipmentOrNot) return;
        this.itemIsEmptySlotOrNot = false;
        this.itemIcon = newItem.itemIcon;
        this.itemColor = newItem.itemColor;
        this.itemIsCurrentlyObatainableOrNot = newItem.itemIsCurrentlyObatainableOrNot;
        this.itemEnum = newItem.itemEnum;
        this.itemName = newItem.itemName;
        this.itemAmount = newItem.itemAmount;
        this.itemDescription = newItem.itemDescription;
        this.itemIsEquipmentOrNot = false;
    }
    /// <summary>
    /// 用SO初始SO,包括裝備
    /// </summary>
    /// <param name="newItem">交易物品</param>
    /// <param name="isDev">皆為false,除了開發者使用</param>
    public void Initialize(InventoryItem_SO newItem, bool isDev)
    {
        if (isDev)
        {
            Debug.Log($"開發者已將{newItem.itemName}加入玩家包包");
            this.itemIsEmptySlotOrNot = false;
            this.itemIcon = newItem.itemIcon;
            this.itemColor = newItem.itemColor;
            this.itemIsCurrentlyObatainableOrNot = newItem.itemIsCurrentlyObatainableOrNot;
            this.itemEnum = newItem.itemEnum;
            this.itemName = newItem.itemName;
            this.itemAmount = newItem.itemAmount;
            this.itemDescription = newItem.itemDescription;
            this.itemIsEquipmentOrNot = newItem.itemIsEquipmentOrNot;
            if (!this.itemIsEquipmentOrNot) return;
            this.equipmentInfo.Initialize(newItem.equipmentInfo);
            return;
        }
        this.itemIsEmptySlotOrNot = false;
        this.itemIcon = newItem.itemIcon;
        this.itemColor = newItem.itemColor;
        this.itemIsCurrentlyObatainableOrNot = newItem.itemIsCurrentlyObatainableOrNot;
        this.itemEnum = newItem.itemEnum;
        this.itemName = newItem.itemName;
        this.itemAmount = 1;
        this.itemDescription = newItem.itemDescription;
        this.itemIsEquipmentOrNot = newItem.itemIsEquipmentOrNot;
        if (!this.itemIsEquipmentOrNot) return;
        this.equipmentInfo.Initialize(newItem.equipmentInfo);
    }
    /// <summary>
    /// 取得任務獎勵用
    /// </summary>
    /// <param name="newItem">獎勵物品</param>
    /// <param name="itemAmount">物品數量</param>
    public void Initialize(InventoryItem_SO newItem, int itemAmount, int equipmentDurability)
    {
        this.itemIsEmptySlotOrNot = false;
        this.itemIcon = newItem.itemIcon;
        this.itemColor = newItem.itemColor;
        this.itemIsCurrentlyObatainableOrNot = newItem.itemIsCurrentlyObatainableOrNot;
        this.itemEnum = newItem.itemEnum;
        this.itemName = newItem.itemName;
        this.itemAmount = itemAmount;
        this.itemDescription = newItem.itemDescription;
        this.itemIsEquipmentOrNot = newItem.itemIsEquipmentOrNot;
        if (!this.itemIsEquipmentOrNot) return;
        this.equipmentInfo.Initialize(newItem.equipmentInfo);
        this.equipmentInfo.equipmentCurrentDurability = equipmentDurability;
    }
    /// <summary>
    /// 從Prompt加入的Item
    /// </summary>
    public void Initialize(Prompts_SO promptItem)
    {
        this.itemIsEmptySlotOrNot = false;
        this.itemIcon = promptItem.promptSprite;
        this.itemColor = promptItem.promptTransferToItem.itemColor;
        this.itemEnum = promptItem.promptTransferToItem.itemEnum;
        this.itemName = promptItem.promptTransferToItem.itemName;
        this.itemAmount = 1;
        this.itemDescription = promptItem.promptTransferToItem.itemDescription;
        this.itemIsEquipmentOrNot = promptItem.promptTransferToItem.itemIsEquipmentOrNot;
        if (!this.itemIsEquipmentOrNot) return;
        this.equipmentInfo.Initialize(promptItem.promptTransferToItem.equipmentInfo);
    }
}