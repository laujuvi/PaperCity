using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MesaCulpable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private GameObject _uiGuiltySelector;
    private bool _isActive = false;

    public void Interact()
    {
          if(_isActive == false)
          {
              _uiGuiltySelector.SetActive(true);
              _isActive = true;
          }
          else
          {
              _uiGuiltySelector.SetActive(false);
              _isActive = false;
          }
        
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
