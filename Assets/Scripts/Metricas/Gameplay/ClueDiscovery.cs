using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDiscovery : Unity.Services.Analytics.Event
{
    public ClueDiscovery() : base("clueDiscovered")
    {

    }

    public int clueCount { set { SetParameter("clue_Count", value); } }
    public string clueID { set { SetParameter("clue_ID", value); } }
}
