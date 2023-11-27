using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;
    private List<ISave> saveDataList = new List<ISave>();
    private Dictionary<string, GameData> saveDataDict = new Dictionary<string, GameData>();
    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/DATA/";
    }
    private void OnEnable()
    {
        EventHandler.startNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.startNewGame -= OnStartNewGame;
    }
    private void OnStartNewGame()
    {
        string resultPath = jsonFolder + "data.sav";
        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }
    public void SaveGame()
    {
        saveDataDict.Clear();
        foreach (var data in saveDataList)
        {
            saveDataDict.Add(data.GetType().Name, data.generateData());
        }
        string resultPath = jsonFolder + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }
        File.WriteAllText(resultPath, jsonData);
    }
    public void LoadGame()
    {
        string resultPath = jsonFolder + "data.sav";
        if (!File.Exists(resultPath)) return;
        var stringData = File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameData>>(stringData);
        foreach (var data in saveDataList)
        {
            data.RestoreData(jsonData[data.GetType().Name]);
        }
        GameManager.instance.LoadSceneByID(1);
    }
    public void Register(ISave save)
    {
        saveDataList.Add(save);
    }
    private void Update()
    {
        Debug.Log(saveDataList.Count);
    }
}