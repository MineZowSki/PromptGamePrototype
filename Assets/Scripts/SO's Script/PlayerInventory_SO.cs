using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerInventory_", menuName = "Player/Inventory")]
public class PlayerInventory_SO : ScriptableObject
{
    public List<Prompts_SO> promptItemList;
    public List<string> byproductList;    
}