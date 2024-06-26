using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentChildSetter : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject childObject;
    private void Start()
    {
        SetParent();
    }
    public void SetParent()
    {
        if(parentObject != null && childObject != null)
        {
            childObject.transform.SetParent(parentObject.transform);
        }
        else
        {
            Debug.LogWarning("ParentObject o ChildObject no estan asignados");
        }
    }
}