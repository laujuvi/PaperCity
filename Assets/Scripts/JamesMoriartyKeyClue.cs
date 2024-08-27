using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesMoriartyKeyClue : MonoBehaviour
{
    public NPCInteractable npcInteractable;
    public GameObjectControllerv2 goc2;
    public GameObject jamesMoriartyKey;
    private void Start()
    {
        goc2.AddObject("JamesMoriartyKey", jamesMoriartyKey);
    }
    public void CheckCountDialog()
    {
        if (npcInteractable.countDialog >= 3)
        {
            goc2.ActivateObject("JamesMoriartyKey");
        }
    }
}