using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Prompt_", menuName = "Prompt/Prompt")]
public class Prompts_SO : ScriptableObject
{
    public PromptType promptType;
    public Sprite promptSprite;
    public string promptName_English;
    public string promptName_TraditionalChinese;
    [Tooltip("此Prompt是否能被玩家取得")] public bool promptIsObtainableOrNot = true;
    [Tooltip("讓玩家取得的Item")] public InventoryItem_SO promptTransferToItem;
    [Tooltip("給玩家的經驗值")] public float promptEXP;
    [Tooltip("對玩家的傷害")] public float promptDamage;
    [Tooltip("對裝備的磨損值")] public int promptWearing = 1;
    public AudioClip promptAudio;
    [Tooltip("此Prompt是否有副產物")]
    public bool containsByproduct;
    [Header("(注意:ContainsByproduct有勾才去調下面的值)")]
    [Range(0f, 1f)] public float byproductOdds;
    [Header("(注意:以下總和要為1)")]
    [Range(0f, 1f)] public List<float> eachByproductOdds;
    public List<Byproduct_SO> byproductList;
}