using UnityEngine;
public abstract class PlayerCharacter : MonoBehaviour
{
    protected virtual void Awake()
    {
    }
    [Header("���ʼƾ�")]
    [Tooltip("���a��l�ưѼ�")] public InitialPlayer_SO playerInitial;
    //[Tooltip("���a����")] public CharacterInfo_SO character;
    //[Tooltip("���a��l�Ѽ�")] public PlayerAllInfo_SO playerDefaultInfo;
    [Header("�|�ʼƾ�")]
    [Tooltip("���a�bRuntime���ƾ�")] public PlayerAllInfo_SO playerInfo;
}