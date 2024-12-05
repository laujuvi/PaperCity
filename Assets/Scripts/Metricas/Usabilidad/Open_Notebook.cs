using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Notebook : Unity.Services.Analytics.Event
{
    public Open_Notebook() : base("OpenNoteBook")
    {

    }

    public int open_NoteBook { set { SetParameter("open_NoteBook", value); } }
}
