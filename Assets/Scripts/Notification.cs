using UnityEngine;
public class Notification : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if(Input.anyKeyDown) gameObject.SetActive(false);
        }
    }
}