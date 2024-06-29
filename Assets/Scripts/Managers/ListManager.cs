using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoPistas;
    [SerializeField] private int characterLimit  = 100;
    [SerializeField] private int maxCluesPerPage = 4;
    [SerializeField] private List<NoteBookPages> NoteBookPagesList = new List<NoteBookPages>();
    [SerializeField] private AudioClip changePage;
    private PlayerController playerController;

    private int currentPage = 0;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        if (playerController.isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                int previousPage = currentPage - 1;
                int nextPage = currentPage + 1;
                playerController.PlayAudio(changePage);

                if (currentPage == NoteBookPagesList.Count - 1)
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    NoteBookPagesList[previousPage].gameObject.SetActive(true);
                    currentPage--;
                }
                else if (currentPage == 0)
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    NoteBookPagesList[nextPage].gameObject.SetActive(true);
                    currentPage++;
                }
                
                else
                {
                    if (NoteBookPagesList[currentPage].isFull)
                    {
                        NoteBookPagesList[currentPage].gameObject.SetActive(false);
                        NoteBookPagesList[nextPage].gameObject.SetActive(true);
                        currentPage++;
                    }
                    else
                    {
                        NoteBookPagesList[currentPage].gameObject.SetActive(false);
                        NoteBookPagesList[previousPage].gameObject.SetActive(true);
                        currentPage--;
                    }
                }
            }
        }
    }

    public void AddText(string newText)
    {
        if (NoteBookPagesList[currentPage].textList.Count >= maxCluesPerPage)
        {
            if (currentPage < NoteBookPagesList.Count - 1)
            {
                currentPage++;
            }
            else
            {
                Debug.LogWarning("No more pages available!");
                return;
            }
        }
        NoteBookPagesList[currentPage].textList.Add(newText);
        UpdateUIText();
    }

    // Método para actualizar el UI Text con el contenido de la lista
    private void UpdateUIText()
    {
        for (int i = 0; i < NoteBookPagesList.Count; i++)
        {
            if (!NoteBookPagesList[i].isFull)
            {
                NoteBookPagesList[i].textoPista.text = string.Join("\n\n", NoteBookPagesList[i].textList);
                NoteBookPagesList[i].clueCount = NoteBookPagesList[i].textList.Count;
                if (NoteBookPagesList[i].clueCount >= maxCluesPerPage)
                {
                    NoteBookPagesList[i].isFull = true;
                }
            }
        }   
    }
}
