using UnityEngine;
public abstract class PlayerInventorySetting : PlayerCharacter
{
    [Header("ª±®aÄÝ©Ê")]
    [SerializeField] protected ItemListSorted_SO playerBag;
    public const int PlayerInventoryCapacity = 21;
}