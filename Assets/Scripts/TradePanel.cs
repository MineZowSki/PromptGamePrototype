using UnityEngine;
public class TradePanel : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnEnable()
    {
        if (player.playerLevel < 10)
        {
            for (int i = 4; i < 12; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (player.playerLevel >= 10 && player.playerLevel < 20)
        {
            for (int i = 4; i < 8; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 8; i < 12; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 4; i < 12; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}