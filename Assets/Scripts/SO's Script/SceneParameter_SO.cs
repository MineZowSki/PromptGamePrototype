using UnityEngine;
[CreateAssetMenu(fileName = "SceneDefaultParameter_", menuName = "Scene_SO/Parameter")]
public class SceneParameter_SO : ScriptableObject
{
    [Header("Enum����")]
    [Tooltip("������Enum")] public SceneName sceneName;
    [Tooltip("�b�a�ϤW���a�I")] public LocationName sceneLocationName;
    [Space(10)]
    [Header("����Prompt")]
    [Tooltip("������������Prompt")] public ScenePrompts_SO scenePrompts;
    [Tooltip("��������Prompt�O�_���H����")] public bool scenePromptsRandomOrNot;
    [Space(10)]
    [Header("��������")]
    [Tooltip("True�h�|��ܦb���O���Afalse�h���|")] public bool sceneIsUnlockedOrNot = false;
    [Tooltip("���a�n�F��ӵ��Ť~��������")] public int sceneRequireLevel;
    [Space(10)]
    [Header("�����Ѽ�")]
    [Tooltip("�X�{�̤pPrompt�ƶq�����v")][Range(1, 4)] public int scenePromptTypeNumber = 4;
    [Tooltip("�X�{�̤pPrompt�ƶq�����v")][Range(0f, 1f)] public float sceneMinPromptOdds = 0.5f;
    [Tooltip("�̤jPrompt�ƶq")] public int sceneMaxPromptsNumber = 5;
    [Tooltip("�̤pPrompt�ƶq")] public int sceneMinPromptsNumber = 3;
    [Tooltip("�����d������{�סA�V�j����V�C�A�V�p����V��")] public float scenePatienceMinus = 100f;
    [Tooltip("�ثe�������X�h")] public int sceneTotalStage = 5;
    [Tooltip("�����n���h�֭@��")][Range(0f, 1f)] public float sceneWrongInputPatincePenalty = 0.1f;
    [Tooltip("�����O�_�������@���Y�Ҧ�prompt������")] public bool sceneDestroyAllOrNot = false;
    [Tooltip("�����S��ĪG")] public string sceneEffect = "�ثe�٨S��ˡA�ݭnSO";
    [Space(10)]
    [Header("�ɶ�����")]
    [Tooltip("�C��Prompt���X���ɶ����j")] public float scenePromptEveryPopTime = 0.5f;
    [Tooltip("�R��Prompt���ɶ����j�A�u��DestroyAll�������]�m�~����")] public float scenePromptRespawnTime = 0.1f;
    [Space(10)]
    [Header("Boss����")]
    [Tooltip("�������O�_��Boss")] public bool sceneBossSceneOrNot = false;
    [Tooltip("BossRush�����v")][Range(0f, 1f)] public float sceneBossRushOdds = 0.1f;
    [Tooltip("BossRush���ɶ�")] public float sceneBossRushTime = 3f;
    [Tooltip("Boss����q")] public float sceneBossHealth = 0f;
    [Space(10)]
    [Header("���Ĭ���")]
    [Tooltip("������BGM")] public AudioClip sceneBGM;
    [Tooltip("����e���I������")] public AudioClip[] sceneResultSound;
    [Space(10)]
    [Header("�Ϥ���L")]
    [Tooltip("�������I����")] public Sprite sceneSprite;
    [Tooltip("����Boss���Ϥ�")] public Sprite sceneBossSprite;
    [Space(10)]
    [Header("Map���O���")]
    [Tooltip("�O�_��ܦ�������Prompt")] public bool sceneShowPrompt = true;
    [Tooltip("�O�_��ܦ�������Input�ƶq")] public bool sceneShowInputAmount = true;
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
        //this.sceneEffect = "�ثe�٨S��ˡA�ݭnSO";
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