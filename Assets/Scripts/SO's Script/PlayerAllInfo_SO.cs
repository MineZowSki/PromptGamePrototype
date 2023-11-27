using UnityEngine;
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player/AllInfo")]
public class PlayerAllInfo_SO : ScriptableObject
{
    public int playerLevel = 0;
    public float playerMaxHealth = 100f;
    public float playerCurrentHealth = 100f;
    public float playerEXP = 0f;
    public float playerDamage = 5f;
    public float playerDefense = 10f;
    public int playerTotalWrongInput = 0;
    public InventoryItem_SO playerMainEquipment;
    public InventoryItem_SO playerSecondaryEquipment1;
    public InventoryItem_SO playerSecondaryEquipment2;
    public InventoryItem_SO playerSecondaryEquipment3;
    public InventoryItem_SO playerSecondaryEquipment4;
}