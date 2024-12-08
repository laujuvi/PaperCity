using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoPistas;
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
        if (GameManager.Instance.uIManager.CheckNotebookStatus())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if(NoteBookPagesList[currentPage].textList.Count >= maxCluesPerPage)
                {
                    playerController.PlayAudio(changePage);

                    NoteBookPagesList[currentPage].gameObject.SetActive(false);

                    currentPage++;
                    if (currentPage >= NoteBookPagesList.Count)
                    {
                        currentPage = 0;
                    }

                    NoteBookPagesList[currentPage].gameObject.SetActive(true);
                } else if (NoteBookPagesList[currentPage] != NoteBookPagesList[0])
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    currentPage = 0;
                    NoteBookPagesList[currentPage].gameObject.SetActive(true);
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {

                if (NoteBookPagesList[currentPage].textList.Count >= maxCluesPerPage)
                {
                    playerController.PlayAudio(changePage);

                    NoteBookPagesList[currentPage].gameObject.SetActive(false);

                    currentPage--;
                    if (currentPage < 0)
                    {

                        currentPage = NoteBookPagesList.Count - 1;

                        // si la pagina esta vacia paso a otra pagina
                        if (NoteBookPagesList[currentPage].textList.Count == 0)
                        {
                            currentPage -= 1; 
                        }
                    }

                    NoteBookPagesList[currentPage].gameObject.SetActive(true);
                } else if (NoteBookPagesList[currentPage] != NoteBookPagesList[0])
                {
                    NoteBookPagesList[currentPage].gameObject.SetActive(false);
                    currentPage -= 1;
                    NoteBookPagesList[currentPage].gameObject.SetActive(true);
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
