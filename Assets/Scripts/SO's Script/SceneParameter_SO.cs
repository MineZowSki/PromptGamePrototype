using UnityEngine;
[CreateAssetMenu(fileName = "SceneDefaultParameter_", menuName = "Scene_SO/Parameter")]
public class SceneParameter_SO : ScriptableObject
{
    [Header("Enum相關")]
    [Tooltip("場景的Enum")] public SceneName sceneName;
    [Tooltip("在地圖上的地點")] public LocationName sceneLocationName;
    [Space(10)]
    [Header("場景Prompt")]
    [Tooltip("此場景內有的Prompt")] public ScenePrompts_SO scenePrompts;
    [Tooltip("此場景的Prompt是否為隨機的")] public bool scenePromptsRandomOrNot;
    [Space(10)]
    [Header("場景解鎖")]
    [Tooltip("True則會顯示在面板中，false則不會")] public bool sceneIsUnlockedOrNot = false;
    [Tooltip("玩家要達到該等級才能解鎖場景")] public int sceneRequireLevel;
    [Space(10)]
    [Header("場景參數")]
    [Tooltip("出現最小Prompt數量的機率")][Range(1, 4)] public int scenePromptTypeNumber = 4;
    [Tooltip("出現最小Prompt數量的機率")][Range(0f, 1f)] public float sceneMinPromptOdds = 0.5f;
    [Tooltip("最大Prompt數量")] public int sceneMaxPromptsNumber = 5;
    [Tooltip("最小Prompt數量")] public int sceneMinPromptsNumber = 3;
    [Tooltip("此關卡的扣血程度，越大扣血越慢，越小扣血越快")] public float scenePatienceMinus = 100f;
    [Tooltip("目前場景有幾層")] public int sceneTotalStage = 5;
    [Tooltip("按錯要扣多少耐心")][Range(0f, 1f)] public float sceneWrongInputPatincePenalty = 0.1f;
    [Tooltip("場景是否為按錯一次即所有prompt消失型")] public bool sceneDestroyAllOrNot = false;
    [Tooltip("場景特殊效果")] public string sceneEffect = "目前還沒實裝，需要SO";
    [Space(10)]
    [Header("時間相關")]
    [Tooltip("每次Prompt跳出的時間間隔")] public float scenePromptEveryPopTime = 0.5f;
    [Tooltip("刪除Prompt的時間間隔，只有DestroyAll的場景設置才有效")] public float scenePromptRespawnTime = 0.1f;
    [Space(10)]
    [Header("Boss相關")]
    [Tooltip("此場景是否有Boss")] public bool sceneBossSceneOrNot = false;
    [Tooltip("BossRush的機率")][Range(0f, 1f)] public float sceneBossRushOdds = 0.1f;
    [Tooltip("BossRush的時間")] public float sceneBossRushTime = 3f;
    [Tooltip("Boss的血量")] public float sceneBossHealth = 0f;
    [Space(10)]
    [Header("音效相關")]
    [Tooltip("場景的BGM")] public AudioClip sceneBGM;
    [Tooltip("結算畫面碰撞音效")] public AudioClip[] sceneResultSound;
    [Space(10)]
    [Header("圖片其他")]
    [Tooltip("場景的背景圖")] public Sprite sceneSprite;
    [Tooltip("場景Boss的圖片")] public Sprite sceneBossSprite;
    [Space(10)]
    [Header("Map面板顯示")]
    [Tooltip("是否顯示此場景的Prompt")] public bool sceneShowPrompt = true;
    [Tooltip("是否顯示此場景的Input數量")] public bool sceneShowInputAmount = true;
    public float sceneAbundance => (sceneMinPromptsNumber * sceneMinPromptOdds + sceneMaxPromptsNumber * (1 - sceneMinPromptOdds)) * sceneTotalStage;
    public float scenePatience => scenePatienceMinus / sceneWrongInputPatincePenalty;
    public void Initialize(SceneParameter_SO newSceneParameter)
    {
        this.sceneName = newSceneParameter.sceneName;
        this.sceneLocationName = newSceneParameter.sceneLocationName;
        this.scenePrompts = newSceneParameter.scenePrompts;
        this.scenePromptsRandomOrNot = newSceneParameter.scenePromptsRandomOrNot;
        this.sceneIsUnlockedOrNot = newSceneParameter.sceneIsUnlockedOrNot;
        this.sceneRequireLevel = newSceneParameter.sceneRequireLevel;
        this.scenePromptTypeNumber = newSceneParameter.scenePromptTypeNumber;
        this.sceneMinPromptOdds = newSceneParameter.sceneMinPromptOdds;
        this.sceneMaxPromptsNumber = newSceneParameter.sceneMaxPromptsNumber;
        this.sceneMinPromptsNumber = newSceneParameter.sceneMinPromptsNumber;
        this.scenePatienceMinus = newSceneParameter.scenePatienceMinus;
        this.sceneTotalStage = newSceneParameter.sceneTotalStage;
        this.sceneWrongInputPatincePenalty = newSceneParameter.sceneWrongInputPatincePenalty;
        this.sceneDestroyAllOrNot = newSceneParameter.sceneDestroyAllOrNot;
        //this.sceneEffect = "目前還沒實裝，需要SO";
        this.scenePromptEveryPopTime = newSceneParameter.scenePromptEveryPopTime;
        this.scenePromptRespawnTime = newSceneParameter.scenePromptRespawnTime;
        this.sceneBossSceneOrNot = newSceneParameter.sceneBossSceneOrNot;
        this.sceneBossRushOdds = newSceneParameter.sceneBossRushOdds;
        this.sceneBossRushTime = newSceneParameter.sceneBossRushTime;
        this.sceneBossHealth = newSceneParameter.sceneBossHealth;
        this.sceneBGM = newSceneParameter.sceneBGM;
        this.sceneResultSound = newSceneParameter.sceneResultSound;
        this.sceneSprite = newSceneParameter.sceneSprite;
        this.sceneBossSprite = newSceneParameter.sceneBossSprite;
        this.sceneShowPrompt = newSceneParameter.sceneShowPrompt;
        this.sceneShowInputAmount = newSceneParameter.sceneShowInputAmount;
    }
}