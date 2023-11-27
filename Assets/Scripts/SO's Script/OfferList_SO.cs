using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OfferList_", menuName = "Market/OfferList")]
public class OfferList_SO : ScriptableObject
{
    public List<TradeRequire_SO> tradeOffer;
}