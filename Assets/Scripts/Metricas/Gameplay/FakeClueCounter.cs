using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeClueCounter : Unity.Services.Analytics.Event
{

    public FakeClueCounter() : base("fakeClueCounter")
    {

    }

    public int fake_Clue_Count { set { SetParameter("fakeClueCount", value); } }
    public bool is_First_Clue { set { SetParameter("firstTimeInteract", value); } }
}
