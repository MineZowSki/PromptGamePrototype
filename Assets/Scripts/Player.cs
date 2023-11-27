using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public sealed class Player : PlayerInventory, ISave
{
    public Sprite playerMainWeaponSprite => playerInfo.playerMainEquipment.itemIcon;
    private void OnEnable()
    {
        EventHandler.startNewGame += OnStartNewGame;
        EventHandler.playerCanOpenInventoryOrNot += OnPlayerCanOpenInventoryOrNot;
    }
    private void OnDisable()
    {
        EventHandler.startNewGame -= OnStartNewGame;
        EventHandler.playerCanOpenInventoryOrNot -= OnPlayerCanOpenInventoryOrNot;
    }
    private void Start()
    {
        ISave iSave = this;
        iSave.SaveRegister();
    }
    public GameData generateData()
    {
        GameData data = new GameData();
        data.playerLevel = playerInfo.playerLevel;
        data.playerMaxHealth = playerInfo.playerMaxHealth;
        data.playerCurrentHealth = playerInfo.playerCurrentHealth;
        data.playerEXP = playerInfo.playerEXP;
        data.playerDamage = playerInfo.playerDamage;
        data.playerDefense = playerInfo.playerDefense;

        data.wrongInput = playerInfo.playerTotalWrongInput;

        for (int i = 0; i < PlayerInventoryCapacity; i++)
        {
            data.playerBagInfo.Add(new ItemData(playerBag.itemList[i]));
        }
        for (int i = 0; i < MissionList.missionList.Count; i++)
        {
            data.missionList.Add(new MissionData(MissionList.missionList[i]));
        }
        return data;
    }
    public void RestoreData(GameData data)
    {
        playerInfo.playerLevel = data.playerLevel;
        playerInfo.playerMaxHealth = data.playerMaxHealth;
        playerInfo.playerCurrentHealth = data.playerCurrentHealth;
        playerInfo.playerEXP = data.playerEXP;
        playerInfo.playerDamage = data.playerDamage;
        playerInfo.playerDefense = data.playerDefense;

        playerInfo.playerTotalWrongInput = data.wrongInput;

        for (int i = 0; i < PlayerInventoryCapacity; i++)
        {
            playerBag.itemList[i].Initialize(data.playerBagInfo[i]);
        }
        MissionList.missionList.Clear();
        List<Mission_SO> missionAll = Resources.LoadAll<Mission_SO>("SO/Mission").ToList();
        foreach (var mission in data.missionList)
        {
            MissionList.missionList.Add(missionAll.Find(n => n.missionName == mission.missionName));
        }
        for (int i = 0; i < data.missionList.Count; i++)
        {
            MissionList.missionList[i].Initialize(data.missionList[i]);
        }
    }
}