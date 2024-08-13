using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallCheck : MonoBehaviour
{
    GameManager gameManager;
    BoxMessageManager boxMessageManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        boxMessageManager = FindObjectOfType<BoxMessageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        wallChecker();
    }

    private void wallChecker()
    {
        if(gameManager.npcInteracted.Count >= 3)
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
    //    {
    //        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    //        if (gameManager.npcInteracted.Count < 3)
    //        {
    //            boxMessageManager.SendMessage("", Color.white, "I should talk to the suspects first", Emotions.None);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (gameManager.npcInteracted.Count < 3)
            {
                boxMessageManager.SendMessage("", Color.white, "I should talk to the suspects first", Emotions.None);
            }
        }
    }

}
