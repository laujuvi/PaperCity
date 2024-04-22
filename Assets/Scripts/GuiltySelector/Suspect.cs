using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Suspect  
{
    public string SuspectName;
    public Image MiniImage;
    public Image SuiluetImage;
    public bool isGuilty;
    public bool isSelected;
    public Button SuspectButton;
    public bool buttonIsPress;
}
