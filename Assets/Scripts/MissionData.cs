public class MissionData
{
    public MissionData() { }
    public MissionData(Mission_SO mission)
    {
        missionCompletedOrNot = mission.missionCompletedOrNot;
        missionName = mission.missionName;
    }
    public bool missionCompletedOrNot;
    public string missionName;
}