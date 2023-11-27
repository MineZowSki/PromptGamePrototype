using System.Collections;
public interface IDialog
{
    /// <summary>
    /// �bvoid�̼g bool = !bool
    /// �nStartCoroutine(ShowDialog())��StartCoroutine(ShowDialogSecond())
    /// </summary>
    /// <param name="content">�n��ܪ��奻</param>
    /// <param name="time">�奻���t��</param>
    void DialogStart(string content, float time);
    IEnumerator ShowDialog(string dialog, float howFastTheDialogIs);
    IEnumerator ShowDialogSecond(string dialog, float howFastTheDialogIs);
}
public interface IScenePriority
{
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int scenePromptTypeNumberPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneMinPromptOddsPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int sceneMaxPromptsNumberPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int sceneMinPromptsNumberPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePatienceMinusPriority(Player player);
    /// <summary>
    /// ���ȥu��b��l�ɧ��ܡA�C�������i���
    /// </summary>
    /// <param name="player"></param>
    /// <param name="isInitial"></param>
    /// <returns></returns>
    int sceneTotalStagePriority(Player player, bool isInitial);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneWrongInputPatincePenaltyPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool sceneDestroyAllOrNotPriority(Player player);
    //public SO sceneEffectPriority(Player player, bool isInitial);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePromptEveryPopTimePriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float scenePromptRespawnTimePriority(Player player);
    /// <summary>
    /// �ثe�u�i�H����l�ȡA���a�L�k����
    /// </summary>
    /// <returns></returns>
    bool sceneBossSceneOrNotPriority();
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneBossRushOddsPriority(Player player);
    /// <summary>
    /// �C�����i���
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    float sceneBossRushTimePriority(Player player);
    /// <summary>
    /// ���ȥu��b��l�ɧ��ܡA�C�������i���
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