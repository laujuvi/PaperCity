using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesMoriartyKeyClue : MonoBehaviour
{
    public GameObjectControllerv2 goc2;
    public GameObject jamesMoriartyKey;
   
    private void Start()
    {
        goc2.AddObject("JamesMoriartyKey", jamesMoriartyKey);
    }
    
    public void CheckCountDialog(int countDialog)
    {
        if (countDialog >= 3)
        {
            goc2.ActivateObject("JamesMoriartyKey");
        }
    }
}