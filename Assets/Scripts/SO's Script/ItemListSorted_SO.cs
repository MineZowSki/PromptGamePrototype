using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemList_", menuName = "Item/ItemList")]
public class ItemListSorted_SO : ScriptableObject
{
    public List<InventoryItem_SO> itemList;
}