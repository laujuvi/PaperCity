using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSuspectRate : Unity.Services.Analytics.Event
{
    public CorrectSuspectRate() : base("endLevel")
    {

    }

    public bool correct_Suspect_Rate {  set { SetParameter("right_Suspect", value); } }

    public string suspect_ID { set { SetParameter("suspect_ID", value ); } }
}
