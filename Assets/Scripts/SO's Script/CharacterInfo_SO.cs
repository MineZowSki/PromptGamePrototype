using UnityEngine;
[CreateAssetMenu(fileName = "CharacterInfo_", menuName = "Player/CharacterInfo")]
public class CharacterInfo_SO : ScriptableObject
{
    [Header("角色自身參數")]
    public string characterName;
    public float characterLevelEXPRequirement;
    [Space(10)]
    [Header("角色影響場景之參數")]
    public int characterScenePromptTypeNumberParameter = 0;
    public int characterSceneDestroyAllOrNotParameter = 0;
    public float characterSceneMinPromptOddsParameter;
    public float characterSceneMaxPromptsNumberParameter;
    public float characterSceneMinPromptsNumberParameter;
    public float characterScenePatienceMinusParameter;
    public float characterSceneTotalStageParameter;
    public float characterSceneWrongInputPatincePenaltyParameter;
    public float characterScenePromptEveryPopTimeParameter;
    public float characterScenePromptRespawnTimeParameter;
    //public int characterSceneBossSceneOrNot = 0;
    public float characterSceneBossRushOddsParameter;
    public float characterSceneBossRushTimeParameter;
    public float characterSceneBossHealthParameter;
}