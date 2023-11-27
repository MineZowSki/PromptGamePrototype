using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : Singleton<GameManager>, ISave
{
    //public SceneParameter_SO sceneParameterTransfer;
    [Header("DATA")]
    public OfferList_SO tradeList;
    public List<InventoryItem_SO> craftDeposit = new List<InventoryItem_SO>();
    private void OnEnable()
    {
        EventHandler.startNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.startNewGame -= OnStartNewGame;
    }
    private void Start()
    {
        ISave iSave = this;
        iSave.SaveRegister();
    }
    private void OnStartNewGame()
    {
        tradeList.tradeOffer.Clear();
        foreach(var item in craftDeposit)
        {
            item.Initialize();
        }
    }
    public void SavePlayer()
    {
#if UNITY_EDITOR
        AssetDatabase.StartAssetEditing();
        EditorUtility.SetDirty(InventoryManager.instance.playerBag);
        EditorUtility.SetDirty(tradeList);
        foreach (var item in InventoryManager.instance.playerBag.itemList)
        {
            EditorUtility.SetDirty(item);
        }
        AssetDatabase.StopAssetEditing();
        AssetDatabase.SaveAssets();
#endif
        SaveLoadManager.instance.SaveGame();
        LoadSceneByID(0);
        Debug.Log("PlayerSaved");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public virtual void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
    public virtual void LoadSceneByID(int sceneId)
    {
        SceneManager.LoadSceneAsync(sceneId);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
    #region 這些是給AddListener用的
    public virtual void LoadMarketScene()
    {
        SceneManager.LoadSceneAsync("MarketScene");
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
    public virtual void LoadPromptScene()
    {
        SceneManager.LoadSceneAsync("PromptScene");
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
    public virtual void LoadMapScene()
    {
        SceneManager.LoadSceneAsync("MapScene");
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
    #endregion 這些是給AddListener用的
    public GameData generateData()
    {
        GameData data = new GameData();
        for (int i = 0; i < CraftManager.maxDepositSlots; i++)
        {
            data.craftDepositList.Add(new ItemData(craftDeposit[i]));
        }
        return data;
    }
    public void RestoreData(GameData data)
    {
        for (int i = 0; i < CraftManager.maxDepositSlots; i++)
        {
            craftDeposit[i].Initialize(data.craftDepositList[i]);
        }
    }
}