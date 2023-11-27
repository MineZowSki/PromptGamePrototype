using System.Collections.Generic;
using UnityEngine;
public class Utilities : MonoBehaviour
{
    public const int GameMaxPrompts = 4;
    public static Vector2 mousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    public static RaycastHit2D hit => Physics2D.Raycast(mousePosition, Vector2.zero);
    public static int ByproductWeighted(List<float> ListWithSumFloat1)
    {
        float checkForValidChance = 0;
        foreach (float eachChance in ListWithSumFloat1)
        {
            checkForValidChance += eachChance;
        }
        if (checkForValidChance != 1)
        {
            Debug.Log("Prompt的Byproduct機率總和不為1");
            return -1;
        }
        float decider = Random.value;
        int index = -1;
        for (int i = 0; i < ListWithSumFloat1.Count; i++)
        {
            if (decider < ListWithSumFloat1[i])
            {
                index = i;
                break;
            }
            else decider -= ListWithSumFloat1[i];
        }
        return index;
    }
}