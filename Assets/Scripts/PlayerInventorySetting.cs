using UnityEngine;
public abstract class PlayerInventorySetting : PlayerCharacter
{
    [Header("���a�ݩ�")]
    [SerializeField] protected ItemListSorted_SO playerBag;
    public const int PlayerInventoryCapacity = 21;
}