using UnityEngine;

public class JamesMoriartyKeyClue : MonoBehaviour
{
    public GameObjectControllerv2 goc2;
    public GameObject jamesMoriartyKey;
    bool spawned;
   
    private void Start()
    {
        goc2.AddObject("JamesMoriartyKey", jamesMoriartyKey);
        goc2.DeactivateObject("JamesMoriartyKey");
        spawned = false;
    }
    
    public void CheckCountDialog(int countDialog)
    {
        if (countDialog >= 3 && !spawned)
        {
            goc2.ActivateObject("JamesMoriartyKey");
            spawned = true;
        }
    }
}