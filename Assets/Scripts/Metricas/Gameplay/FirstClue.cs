using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstClue : Unity.Services.Analytics.Event
{

    public FirstClue() : base("timeToFindFirstClue")
    {

    }

    public int gameplay_Time { set { SetParameter("time_to_complete_the_game", value); } }
    public string clue_ID { set { SetParameter("clue_ID", value); } }

}
