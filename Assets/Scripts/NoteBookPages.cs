using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class NoteBookPages : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textoPista;
    public List<string> textList = new List<string>();
    public int clueCount;
    public bool isFull = false;


    private void Update()
    {
        if (clueCount >= 4)
        {
            isFull = true;
        }
    }
}
