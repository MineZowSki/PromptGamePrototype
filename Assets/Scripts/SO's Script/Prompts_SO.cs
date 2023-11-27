using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Prompt_", menuName = "Prompt/Prompt")]
public class Prompts_SO : ScriptableObject
{
    public PromptType promptType;
    public Sprite promptSprite;
    public string promptName_English;
    public string promptName_TraditionalChinese;
    [Tooltip("��Prompt�O�_��Q���a���o")] public bool promptIsObtainableOrNot = true;
    [Tooltip("�����a���o��Item")] public InventoryItem_SO promptTransferToItem;
    [Tooltip("�����a���g���")] public float promptEXP;
    [Tooltip("�缾�a���ˮ`")] public float promptDamage;
    [Tooltip("��˳ƪ��i�l��")] public int promptWearing = 1;
    public AudioClip promptAudio;
    [Tooltip("��Prompt�O�_���Ʋ���")]
    public bool containsByproduct;
    [Header("(�`�N:ContainsByproduct���Ĥ~�h�դU������)")]
    [Range(0f, 1f)] public float byproductOdds;
    [Header("(�`�N:�H�U�`�M�n��1)")]
    [Range(0f, 1f)] public List<float> eachByproductOdds;
    public List<Byproduct_SO> byproductList;
}