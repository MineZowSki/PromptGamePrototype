using UnityEngine;
using System;
public class EventHandler
{
    public static Action<int, KeyCode, string> settingInput;
    public static void CallSettingInput(int index, KeyCode keycode, string input)
    {
        settingInput?.Invoke(index, keycode, input);
    }
    public static Action<bool> playerCanOpenInventoryOrNot;
    public static void CallPlayerCanOpenInventoryOrNot(bool check)
    {
        playerCanOpenInventoryOrNot?.Invoke(check);
    }
    public static Action startNewGame;
    public static void CallStartNewGame()
    {
        startNewGame?.Invoke();
    }
    public static Action<int, float, int> playerInfo;
    public static void CallPlayerInfo(int level, float exp, int mistake)
    {
        playerInfo?.Invoke(level, exp, mistake);
    }
    public static Action<AllItem, int, bool, int> addPlayerItem;
    public static void CallAddPlayerItem(AllItem itemEnum, int itemNumber, bool itemIsEquipmentOrNot, int equipmentDurability)
    {
        addPlayerItem?.Invoke(itemEnum, itemNumber, itemIsEquipmentOrNot, equipmentDurability);
    }
    public static Action<float> playerEXP;
    public static void CallPlayerEXP(float exp)
    {
        playerEXP?.Invoke(exp);
    }
}