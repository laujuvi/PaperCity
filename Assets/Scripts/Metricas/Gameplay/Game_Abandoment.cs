using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Abandoment : Unity.Services.Analytics.Event
{
    public Game_Abandoment() : base("gameAbandoned")
    {

    }

    public int gameplay_Time { set { SetParameter("time_to_complete_the_game", value); } }

    public bool Leave_Game { set { SetParameter("leave_game", value); } }
}
