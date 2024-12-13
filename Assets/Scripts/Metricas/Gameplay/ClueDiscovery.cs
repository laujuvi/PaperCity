using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDiscovery : Unity.Services.Analytics.Event
{
    public ClueDiscovery() : base("clueDiscovered")
    {

    }

    public int clue_Count { set { SetParameter("clue_Count", value); } }
}
