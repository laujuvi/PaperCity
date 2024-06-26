using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    [SerializeField] private GameObject objectToManage;
    public void ActivateObject()
    {
        if (objectToManage != null)
        {
            objectToManage.SetActive(true);
        }
    }
    public void DeactivateObject()
    {
        if (objectToManage != null)
        {
            objectToManage.SetActive(false);
        }
    }
}