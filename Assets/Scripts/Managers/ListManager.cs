using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoPistas;
    private List<string> textList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIText();
    }

    public void AddText(string newText)
    {
        textList.Add(newText);
        UpdateUIText();
    }

    // Método para actualizar el UI Text con el contenido de la lista
    private void UpdateUIText()
    {
        textoPistas.text = string.Join("\n", textList);
    }
}
