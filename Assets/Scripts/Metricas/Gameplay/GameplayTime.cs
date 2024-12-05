using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTime : Unity.Services.Analytics.Event
{
    public GameplayTime() : base("gameplayTime")
    {

    }

    public int gameplay_Time { set { SetParameter("time_to_complete_the_game", value); } }
}
