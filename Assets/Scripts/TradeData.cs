public class TradeData
{
    public TradeData(){}
    public TradeData(TradeRequire_SO tradeOffer)
    {
        tradeIsMissionOrNot = tradeOffer.isMerchandiseMissionOrNot;
        tradeMissionData = new MissionData(tradeOffer.mission);
    }
    public bool tradeIsMissionOrNot;
    public MissionData tradeMissionData;
}