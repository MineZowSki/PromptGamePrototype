using System.Collections;
public interface IDialog
{
    /// <summary>
    /// 在void裡寫 bool = !bool
    /// 要StartCoroutine(ShowDialog())跟StartCoroutine(ShowDialogSecond())
    /// </summary>
    /// <param name="content">要顯示的文本</param>
    /// <param name="time">文本的速度</param>
    void DialogStart(string content, float time);
    IEnumerator ShowDialog(string dialog, float howFastTheDialogIs);
    IEnumerator ShowDialogSecond(string dialog, float howFastTheDialogIs);
}
public interface IScenePriority
{
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int scenePromptTypeNumberPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneMinPromptOddsPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int sceneMaxPromptsNumberPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int sceneMinPromptsNumberPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePatienceMinusPriority(Player player);
    /// <summary>
    /// 此值只能在初始時改變，遊戲中不可更改
    /// </summary>
    /// <param name="player"></param>
    /// <param name="isInitial"></param>
    /// <returns></returns>
    int sceneTotalStagePriority(Player player, bool isInitial);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneWrongInputPatincePenaltyPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool sceneDestroyAllOrNotPriority(Player player);
    //public SO sceneEffectPriority(Player player, bool isInitial);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePromptEveryPopTimePriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePromptRespawnTimePriority(Player player);
    /// <summary>
    /// 目前只可以有初始值，玩家無法改變
    /// </summary>
    /// <returns></returns>
    bool sceneBossSceneOrNotPriority();
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneBossRushOddsPriority(Player player);
    /// <summary>
    /// 遊戲中可更改
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneBossRushTimePriority(Player player);
    /// <summary>
    /// 此值只能在初始時改變，遊戲中不可更改
    /// </summary>
    /// <param name="player"></param>
    /// <param name="isInitial"></param>
    /// <returns></returns>
    float sceneBossHealthPriority(Player player, bool isInitial);
}
public interface IUpdateImpact
{
    float inputTimePriority(Player player);
}
public interface ISave
{
    void SaveRegister()
    {
        if (SaveLoadManager.instance == null) return;
        SaveLoadManager.instance.Register(this);
    }
    GameData generateData();
    void RestoreData(GameData data);
}