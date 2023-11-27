using System.Collections.Generic;
public class GameData
{
    public int playerLevel;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float playerEXP;
    public float playerDamage;
    public float playerDefense;
    public int wrongInput;
    public List<ItemData> playerBagInfo = new List<ItemData>();
    public List<MissionData> missionList = new List<MissionData>();
    public List<TradeData> tradeList = new List<TradeData>();
    public List<ItemData> craftDepositList = new List<ItemData>();
}