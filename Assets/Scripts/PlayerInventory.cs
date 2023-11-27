using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public abstract class PlayerInventory : PlayerProperty
{
    private bool playerCanOpenInventory;
    protected override void Awake()
    {
        base.Awake();
        playerCanOpenInventory = true;
    }
    protected void OnPlayerCanOpenInventoryOrNot(bool check)
    {
        playerCanOpenInventory = check;
    }
    protected override void Update()
    {
        base.Update();
        if (InventoryManager.instance == null) return;
        if (PlayerInput.inventoryInput && playerCanOpenInventory)
        {
            InventoryManager.instance.itemName.text = string.Empty;
            InventoryManager.instance.itemNumber.text = string.Empty;
            InventoryManager.instance.itemDescription.text = string.Empty;
            InventoryManager.instance.inventoryOnOff.SetActive(!InventoryManager.instance.inventoryOnOff.activeSelf);
            InventoryManager.instance.itemDiscardPanel.SetActive(false);
            SoundManager.instance.PlaySound(InventoryManager.instance.bagSound);
            EventHandler.CallPlayerInfo(playerInfo.playerLevel, playerInfo.playerEXP, playerInfo.playerTotalWrongInput);
        }
        #region 這裡只負責武器的開關
        if (InventoryManager.instance.inventoryOnOff.activeSelf)
        {
            int itemIndex = 0;
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            GameObject item = null;
            List<RaycastResult> results = new List<RaycastResult>(1);
            EventSystem.current.RaycastAll(eventData, results);
            if (results.Count > 0)
            {
                item = results[0].gameObject;
                if (item.GetComponent<PlayerInventorySlot>() == null) return;
                itemIndex = item.GetComponent<PlayerInventorySlot>().slotIndex;
            }
            #region 主要裝備
            if (PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region 如果目前沒有裝備主要裝備
                    if (playerInfo.playerMainEquipment == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion 如果目前沒有裝備主要裝備
                    #region 如果目前有裝備主要裝備
                    else
                    {
                        #region 如果目前主要裝備不是自己
                        if (playerInfo.playerMainEquipment.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerMainEquipment.equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion 如果目前主要裝備不是自己
                        #region 如果目前主要裝備是自己
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot;
                        }
                        #endregion 如果目前主要裝備是自己
                    }
                    #endregion 如果目前有裝備主要裝備
                }
            }
            #endregion 主要裝備
            #region 次要裝備1
            if (PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region 如果目前沒有裝備次要裝備1
                    if (playerInfo.playerSecondaryEquipment1 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion 如果目前沒有裝備次要裝備1
                    #region 如果目前有裝備次要裝備1
                    else
                    {
                        #region 如果目前次要裝備1不是自己
                        if (playerInfo.playerSecondaryEquipment1.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment1.equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion 如果目前次要裝備1不是自己
                        #region 如果目前次要裝備1是自己
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot;
                        }
                        #endregion 如果目前次要裝備1是自己
                    }
                    #endregion 如果目前有裝備次要裝備1
                }
            }
            #endregion 次要裝備1
            #region 次要裝備2
            if (PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region 如果目前沒有裝備次要裝備2
                    if (playerInfo.playerSecondaryEquipment2 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion 如果目前沒有裝備次要裝備2
                    #region 如果目前有裝備次要裝備2
                    else
                    {
                        #region 如果目前次要裝備2不是自己
                        if (playerInfo.playerSecondaryEquipment2.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment2.equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion 如果目前次要裝備2不是自己
                        #region 如果目前次要裝備2是自己
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot;
                        }
                        #endregion 如果目前次要裝備2是自己
                    }
                    #endregion 如果目前有裝備次要裝備2
                }
            }
            #endregion 次要裝備2
            #region 次要裝備3
            if (PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region 如果目前沒有裝備次要裝備3
                    if (playerInfo.playerSecondaryEquipment3 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;

                    }
                    #endregion 如果目前沒有裝備次要裝備3
                    #region 如果目前有裝備次要裝備3
                    else
                    {
                        #region 如果目前次要裝備3不是自己
                        if (playerInfo.playerSecondaryEquipment3.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment3.equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion 如果目前次要裝備3不是自己
                        #region 如果目前次要裝備3是自己
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot;
                        }
                        #endregion 如果目前次要裝備3是自己
                    }
                    #endregion 如果目前有裝備次要裝備3
                }
            }
            #endregion 次要裝備3
            #region 次要裝備4
            if (PlayerInput.secondaryEquipmentInput3 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region 如果目前沒有裝備次要裝備4
                    if (playerInfo.playerSecondaryEquipment4 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                    }
                    #endregion 如果目前沒有裝備次要裝備4
                    #region 如果目前有裝備次要裝備4
                    else
                    {
                        #region 如果目前次要裝備4不是自己
                        if (playerInfo.playerSecondaryEquipment4.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment4.equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        }
                        #endregion 如果目前次要裝備4不是自己
                        #region 如果目前次要裝備4是自己
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot;
                        }
                        #endregion 如果目前次要裝備4是自己
                    }
                    #endregion 如果目前有裝備次要裝備4
                }
            }
            #endregion 次要裝備4
        }
        #endregion 這裡只負責武器的開關 
    }
}