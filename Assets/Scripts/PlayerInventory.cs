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
        #region �o�̥u�t�d�Z�����}��
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
            #region �D�n�˳�
            if (PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region �p�G�ثe�S���˳ƥD�n�˳�
                    if (playerInfo.playerMainEquipment == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion �p�G�ثe�S���˳ƥD�n�˳�
                    #region �p�G�ثe���˳ƥD�n�˳�
                    else
                    {
                        #region �p�G�ثe�D�n�˳Ƥ��O�ۤv
                        if (playerInfo.playerMainEquipment.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerMainEquipment.equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion �p�G�ثe�D�n�˳Ƥ��O�ۤv
                        #region �p�G�ثe�D�n�˳ƬO�ۤv
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot;
                        }
                        #endregion �p�G�ثe�D�n�˳ƬO�ۤv
                    }
                    #endregion �p�G�ثe���˳ƥD�n�˳�
                }
            }
            #endregion �D�n�˳�
            #region ���n�˳�1
            if (PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region �p�G�ثe�S���˳Ʀ��n�˳�1
                    if (playerInfo.playerSecondaryEquipment1 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion �p�G�ثe�S���˳Ʀ��n�˳�1
                    #region �p�G�ثe���˳Ʀ��n�˳�1
                    else
                    {
                        #region �p�G�ثe���n�˳�1���O�ۤv
                        if (playerInfo.playerSecondaryEquipment1.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment1.equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion �p�G�ثe���n�˳�1���O�ۤv
                        #region �p�G�ثe���n�˳�1�O�ۤv
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot;
                        }
                        #endregion �p�G�ثe���n�˳�1�O�ۤv
                    }
                    #endregion �p�G�ثe���˳Ʀ��n�˳�1
                }
            }
            #endregion ���n�˳�1
            #region ���n�˳�2
            if (PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region �p�G�ثe�S���˳Ʀ��n�˳�2
                    if (playerInfo.playerSecondaryEquipment2 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                    }
                    #endregion �p�G�ثe�S���˳Ʀ��n�˳�2
                    #region �p�G�ثe���˳Ʀ��n�˳�2
                    else
                    {
                        #region �p�G�ثe���n�˳�2���O�ۤv
                        if (playerInfo.playerSecondaryEquipment2.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment2.equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion �p�G�ثe���n�˳�2���O�ۤv
                        #region �p�G�ثe���n�˳�2�O�ۤv
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot;
                        }
                        #endregion �p�G�ثe���n�˳�2�O�ۤv
                    }
                    #endregion �p�G�ثe���˳Ʀ��n�˳�2
                }
            }
            #endregion ���n�˳�2
            #region ���n�˳�3
            if (PlayerInput.secondaryEquipmentInput2 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput3)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region �p�G�ثe�S���˳Ʀ��n�˳�3
                    if (playerInfo.playerSecondaryEquipment3 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;

                    }
                    #endregion �p�G�ثe�S���˳Ʀ��n�˳�3
                    #region �p�G�ثe���˳Ʀ��n�˳�3
                    else
                    {
                        #region �p�G�ثe���n�˳�3���O�ۤv
                        if (playerInfo.playerSecondaryEquipment3.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment3.equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                        }
                        #endregion �p�G�ثe���n�˳�3���O�ۤv
                        #region �p�G�ثe���n�˳�3�O�ۤv
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot;
                        }
                        #endregion �p�G�ثe���n�˳�3�O�ۤv
                    }
                    #endregion �p�G�ثe���˳Ʀ��n�˳�3
                }
            }
            #endregion ���n�˳�3
            #region ���n�˳�4
            if (PlayerInput.secondaryEquipmentInput3 &&
                !PlayerInput.mainEquipmentInput &&
                !PlayerInput.secondaryEquipmentInput0 &&
                !PlayerInput.secondaryEquipmentInput1 &&
                !PlayerInput.secondaryEquipmentInput2)
            {
                if (Input.GetMouseButtonDown(0) && playerBag.itemList[itemIndex].itemIsEquipmentOrNot)
                {
                    #region �p�G�ثe�S���˳Ʀ��n�˳�4
                    if (playerInfo.playerSecondaryEquipment4 == null)
                    {
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = true;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                        playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                    }
                    #endregion �p�G�ثe�S���˳Ʀ��n�˳�4
                    #region �p�G�ثe���˳Ʀ��n�˳�4
                    else
                    {
                        #region �p�G�ثe���n�˳�4���O�ۤv
                        if (playerInfo.playerSecondaryEquipment4.itemName != playerBag.itemList[itemIndex].itemName)
                        {
                            playerInfo.playerSecondaryEquipment4.equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = true;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmentIsEquippedAsMainOrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary1OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary2OrNot = false;
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary3OrNot = false;
                        }
                        #endregion �p�G�ثe���n�˳�4���O�ۤv
                        #region �p�G�ثe���n�˳�4�O�ۤv
                        else
                        {
                            playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot = !playerBag.itemList[itemIndex].equipmentInfo.equipmantIsEquippedAsSecondary4OrNot;
                        }
                        #endregion �p�G�ثe���n�˳�4�O�ۤv
                    }
                    #endregion �p�G�ثe���˳Ʀ��n�˳�4
                }
            }
            #endregion ���n�˳�4
        }
        #endregion �o�̥u�t�d�Z�����}�� 
    }
}