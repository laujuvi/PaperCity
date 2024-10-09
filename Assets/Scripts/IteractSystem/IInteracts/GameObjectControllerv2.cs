using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectControllerv2 : MonoBehaviour
{
    private Dictionary<string, GameObject> objectsToManage = new Dictionary<string, GameObject>();

    public void AddObject(string identifier, GameObject obj)
    {
        if (!objectsToManage.ContainsKey(identifier))
        {
            
            objectsToManage.Add(identifier, obj);
        }
    }
    public void ActivateObject(string identifier)
    {
        if (objectsToManage.ContainsKey(identifier))
        {
            print("a ver");
            objectsToManage[identifier].SetActive(true);
        }
    }

    public void DeactivateObject(string identifier)
    {
        if (objectsToManage.ContainsKey(identifier))
        {
            objectsToManage[identifier].SetActive(false);
        }
    }
    public void RemoveObject(string identifier)
    {
        if (objectsToManage.ContainsKey(identifier))
        {
            objectsToManage.Remove(identifier);
        }
    }
}