using UnityEngine;
public class End : MonoBehaviour
{
    [SerializeField] private GameObject notification;
    private bool playerReadyToLeave;
    private void Update()
    {
        if (playerReadyToLeave)
        {
            if (PlayerInput.input0 || PlayerInput.input1 || PlayerInput.input2 || PlayerInput.input3) GameManager.instance.LoadSceneByName("StartScene");
        }
        if (PlayerInput.input0 || PlayerInput.input1 || PlayerInput.input2 || PlayerInput.input3)
        {
            notification.SetActive(true);
            playerReadyToLeave = true;
        }
    }
}