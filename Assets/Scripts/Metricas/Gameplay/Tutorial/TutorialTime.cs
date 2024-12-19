using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTime : Unity.Services.Analytics.Event
{ 
    // Start is called before the first frame update
    public TutorialTime() : base("tutorialTime")
    {

    }

    public float tutorial_Time { set { SetParameter("time_In_The_Tutorial", value); } }

}
