using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScenePrompts_", menuName = "Scene_SO/ScenePrompt")]
public class ScenePrompts_SO : ScriptableObject
{
    public List<GameObject> promptsItem = new List<GameObject>(4);
}