using UnityEngine;
[CreateAssetMenu(fileName = "Initial_", menuName = "Initial")]
public class InitialPlayer_SO : ScriptableObject
{
    public string initialName;
    public Sprite initialSprite;
    public PlayerAllInfo_SO initialInfo;
    public CharacterInfo_SO initialCharacterInfo;
}