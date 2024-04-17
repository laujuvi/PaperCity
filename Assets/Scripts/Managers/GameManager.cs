using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _delay;
    public PlayerInteract _playerInteract;
    public PlayerController _playerController;
    private void Start()
    {
        if (_playerInteract != null)
        {
            _playerInteract.OnInteract += DisablePlayerInputs;
        }
        else
        {
            Debug.LogWarning("PlayerInteract component not found in the scene.");
        }
    }
    private void DisablePlayerInputs()
    {
        StartCoroutine(DisablePlayerInputsCoroutine(_delay));
    }
    private IEnumerator DisablePlayerInputsCoroutine(float delay)
    {
        _playerInteract.enabled = false;
        _playerController.enabled = false;
        yield return new WaitForSeconds(delay);
        _playerInteract.enabled = true;
        _playerController.enabled = true;
    }
}
