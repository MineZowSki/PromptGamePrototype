using System.Collections.Generic;
using UnityEngine;
public class PromptsRegister : Prompts
{
    List<GameObject> tempList = new List<GameObject>();
    protected override void Start()
    {
        if (sceneDefaultParameter == null)
        {
            Debug.Log("No SceneDefaultParameter after build");
            return;
        }
        if (sceneDefaultParameter.scenePromptsRandomOrNot)
        {
            tempList = sceneDefaultParameter.scenePrompts.promptsItem;
            System.Random rng = new System.Random();
            int scenePrompts = tempList.Count;
            while (scenePrompts > 1)
            {
                scenePrompts--;
                int random = rng.Next(scenePrompts + 1);
                GameObject temp = tempList[random];
                tempList[random] = tempList[scenePrompts];
                tempList[scenePrompts] = temp;
            }
            for (int i = 0; i < tempList.Count; i++)
            {
                getGameObjects.Add(tempList[i]);
                if (currentSceneObtainedItem.ContainsKey(tempList[i].GetComponent<FourButton>().promptSO.promptTransferToItem.itemEnum)) continue;
                currentSceneObtainedItem.Add(tempList[i].GetComponent<FourButton>().promptSO.promptTransferToItem.itemEnum, 0);
            }
        }
        else
        {
            for (int i = 0; i < sceneDefaultParameter.scenePrompts.promptsItem.Count; i++)
            {
                getGameObjects.Add(sceneDefaultParameter.scenePrompts.promptsItem[i]);
                if (currentSceneObtainedItem.ContainsKey(sceneDefaultParameter.scenePrompts.promptsItem[i].GetComponent<FourButton>().promptSO.promptTransferToItem.itemEnum)) continue;
                currentSceneObtainedItem.Add(sceneDefaultParameter.scenePrompts.promptsItem[i].GetComponent<FourButton>().promptSO.promptTransferToItem.itemEnum, 0);
            }
        }
        base.Start();
    }
}