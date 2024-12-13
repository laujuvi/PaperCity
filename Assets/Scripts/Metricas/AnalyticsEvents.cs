public class AccusationDecisionTime : Unity.Services.Analytics.Event
{
    public AccusationDecisionTime() : base("accusationDecisionTime")
    {

    }
    public float decisionTime { set { SetParameter("accusationTime", value); } }
}
public class AccusationRoomClues : Unity.Services.Analytics.Event
{
    public AccusationRoomClues() : base("accusationRoomClues")
    {

    }
    public int accusationRoomClues { set { SetParameter("accusationRoomClues", value); } }
}
public class ActionUsed : Unity.Services.Analytics.Event
{
    public ActionUsed() : base("actionUsed")
    {

    }
    public string actionName { set { SetParameter("actionName", value); } }
    public int actionUsedTimes { set { SetParameter("actionUsedTimes", value); } }
    public string clue_ID { set { SetParameter("clue_ID", value); } }
}
