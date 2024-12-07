
public class Guilty_Room_Time : Unity.Services.Analytics.Event
{
    public Guilty_Room_Time() : base("GuiltyRoomTime")
    {

    }
    public float guilty_Room_Time { set { SetParameter("guilty_Room_Time", value); } }
}