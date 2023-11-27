using UnityEngine;
public abstract class PlayerMainInfo : PlayerInventorySetting
{
    public int playerLevel => playerInfo.playerLevel;
    public bool playerReachedLevel10 
    { 
        get
        {
            if (playerLevel >= 10) return true;
            return false;
        } 
    }
    public float playerCurrentHealth 
    { 
        get => playerInfo.playerCurrentHealth;          
        set => playerInfo.playerCurrentHealth = value;
    }
    public float playerEXP
    {
        get => playerInfo.playerEXP;
        set => playerInfo.playerEXP = value;
    }
    public int playerTotalWrongInput
    {
        get => playerInfo.playerTotalWrongInput;
        set => playerInfo.playerTotalWrongInput = value;
    }
    public float playerDamageDelta => Random.Range(0f, playerInfo.playerDamage) + playerInfo.playerDamage;
    protected void OnStartNewGame()
    {
        playerInfo.playerLevel = playerInitial.initialInfo.playerLevel;
        playerInfo.playerMaxHealth = playerInitial.initialInfo.playerMaxHealth;
        playerInfo.playerCurrentHealth = playerInitial.initialInfo.playerCurrentHealth;
        playerInfo.playerEXP = playerInitial.initialInfo.playerEXP;
        playerInfo.playerDamage = playerInitial.initialInfo.playerDamage;
        playerInfo.playerDefense = playerInitial.initialInfo.playerDefense;
        playerInfo.playerTotalWrongInput = playerInitial.initialInfo.playerTotalWrongInput;
    }
    private void LevelUp()
    {
        if (playerInitial.initialCharacterInfo == null) return;
        if (playerInfo.playerEXP >= playerInitial.initialCharacterInfo.characterLevelEXPRequirement)
        {
            playerInfo.playerEXP -= playerInitial.initialCharacterInfo.characterLevelEXPRequirement;
            playerInfo.playerLevel += 1;
        }
    }
    private void PlayerHealthAdjustment()
    {
        if (playerInfo.playerCurrentHealth <= 0f)
        {
            playerInfo.playerCurrentHealth = 0f;
        }
    }
    protected virtual void Update()
    {
        LevelUp();
        PlayerHealthAdjustment();
        if (UIManager.instance != null) UIManager.instance.playerHealth.fillAmount = playerInfo.playerCurrentHealth / playerInfo.playerMaxHealth;
    }
}