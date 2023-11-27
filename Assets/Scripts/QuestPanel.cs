using UnityEngine;
using UnityEngine.UI;
public class QuestPanel : MonoBehaviour
{
    [Tooltip("�n�ͦ���OfferSlot")][SerializeField] private GameObject offerSlot;
    [Tooltip("���a�ثe�i�H��������")] public OfferList_SO missions;
    private void OnEnable()
    {
        EventHandler.startNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.startNewGame -= OnStartNewGame;
    }
    private void OnStartNewGame()
    {
        missions.tradeOffer.Clear();
    }
    private void Update()
    {
        if (missions.tradeOffer.Count == 0 || transform.childCount >= missions.tradeOffer.Count) return;
        for(int i = 0; i < missions.tradeOffer.Count; i++)
        {
            if (transform.childCount > i) continue;
            var slot = Instantiate(offerSlot, transform);
            slot.GetComponent<OfferSlot>().tradeRequire = missions.tradeOffer[i];
            slot.GetComponent<Button>().onClick.AddListener(() => MarketManager.instance.ShowTradeInfo(slot.GetComponent<OfferSlot>()));
        }
    }
}