using UnityEngine;
[CreateAssetMenu(fileName = "Merchandise_", menuName = "Market/Merchandise")]
public class TradeRequire_SO : ScriptableObject
{
    public InventoryItem_SO merchandiseItemSO;
    public AllItem merchandiseRequireItem;
    public string merchandiseRequireItemName;
    public int requireItemAmount;
    public bool isMerchandiseMissionOrNot;
    public Mission_SO mission;
}