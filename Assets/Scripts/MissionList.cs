using System.Collections.Generic;
using UnityEngine;
public class MissionList : MonoBehaviour
{
    public static List<Mission_SO> missionList = new List<Mission_SO>();
    public static Dictionary<SceneName, bool> sceneCompleteDict = new Dictionary<SceneName, bool>();
    public static Dictionary<AllItem, int> itemCompleteDict = new Dictionary<AllItem, int>();
    public Mission_SO testMission;
    private void OnEnable()
    {
        EventHandler.startNewGame += OnStartNewGame;
        if (sceneCompleteDict.Count != 0 || itemCompleteDict.Count != 0)
        {
            foreach (var mission in sceneCompleteDict)
            {
                if (missionList.Find(n => n.missionRequestScene == mission.Key) != null)
                {
                    missionList.Find(n => n.missionRequestScene == mission.Key).missionCompletedOrNot = mission.Value;
                }
            }
            foreach (var mission in itemCompleteDict)
            {
                if (missionList.Find(n => n.missionRequestItem == mission.Key) != null)
                {
                    missionList.Find(n => n.missionRequestItem == mission.Key).missionProgress -= mission.Value;
                }
            }
            sceneCompleteDict.Clear();
            itemCompleteDict.Clear();
        }
        foreach (var mission in missionList)
        {
            if (mission.missionRequestItem != AllItem.None && mission.missionProgress <= 0)
            {
                mission.missionCompletedOrNot = true;
                Debug.Log($"Quest {mission.missionName} has Completed: {mission.missionCompletedOrNot}");
            }
        }
    }
    private void OnDisable()
    {
        EventHandler.startNewGame -= OnStartNewGame;
    }
    private void OnStartNewGame()
    {
        missionList.Clear();
    }
    private void Update()
    {
        if (testMission != null)
        {
            missionList.Add(testMission);
            testMission = null;
        }
    }
}