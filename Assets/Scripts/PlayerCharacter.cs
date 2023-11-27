using UnityEngine;
public abstract class PlayerCharacter : MonoBehaviour
{
    protected virtual void Awake()
    {
    }
    [Header("ぃ笆计沮")]
    [Tooltip("產﹍て把计")] public InitialPlayer_SO playerInitial;
    //[Tooltip("產à︹")] public CharacterInfo_SO character;
    //[Tooltip("產﹍把计")] public PlayerAllInfo_SO playerDefaultInfo;
    [Header("穦笆计沮")]
    [Tooltip("產Runtime计沮")] public PlayerAllInfo_SO playerInfo;
}