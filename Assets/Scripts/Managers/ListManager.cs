using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoPistas;
    [SerializeField] private int characterLimit  = 100;
    private List<string> textList = new List<string>();
    private List<string> allTexts = new List<string>();
    [SerializeField] private List<NoteBookPages> NoteBookPagesList = new List<NoteBookPages>();
    private PlayerController playerController;

    public int clueCount = 0;
    private int currentPage = 0;

    // Start is called before the first frame update
    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isOpen)
        {
            int previousPage = currentPage - 1;
            int nextPage = currentPage + 1;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(currentPage != 0)
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    NoteBookPagesList[previousPage].gameObject.SetActive(true);
                    currentPage--;
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {

                if (currentPage < NoteBookPagesList.Count - 1)
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    NoteBookPagesList[nextPage].gameObject.SetActive(true);
                    currentPage++;
                }
            }
            //textoPistas.text = NoteBookPagesList[currentPage].textoPista.text;
        }

        //foreach (NoteBookPages noteBookPages in NoteBookPagesList)
        //{
        //    if (clueCount >= 4)
        //    {
        //        noteBookPages.isFull = true;
        //    }
        //}


        
    }

    public void AddText(string newText)
    {
        NoteBookPagesList[currentPage].textList.Add(newText);
        UpdateUIText();
    }

    // Método para actualizar el UI Text con el contenido de la lista
    private void UpdateUIText()
    {
        for (int i = 0; i < NoteBookPagesList.Count; i++)
        {
            if (NoteBookPagesList[i].isFull == false)
            {
                //textoPistas = NoteBookPagesList[i].textoPista;
                NoteBookPagesList[i].textoPista.text = string.Join("\n", NoteBookPagesList[i].textList);
                NoteBookPagesList[currentPage].clueCount++;
            }
            else
            {
                continue;
            }
        }   
    }
}
