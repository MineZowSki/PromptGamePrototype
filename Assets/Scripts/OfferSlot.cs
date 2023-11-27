using UnityEngine;
using UnityEngine.UI;
public class OfferSlot : MonoBehaviour
{
    public TradeRequire_SO tradeRequire;
    private void Start()
    {
        if (tradeRequire.isMerchandiseMissionOrNot)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = tradeRequire.mission.missionSprite;
            return;
        }
        if (tradeRequire.merchandiseItemSO.itemIcon != null) transform.GetChild(0).GetComponent<Image>().sprite = tradeRequire.merchandiseItemSO.itemIcon;
    }
}