using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject panelScore;

    [Header("Imagenes")]
    [SerializeField] Texture scoreStarImage; 

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI totalCluesText;
    [SerializeField] TextMeshProUGUI resultMessageText;
    [SerializeField] string[] resultMessages;

    private float totalClues;
    private float cluesObtained;
    private int totalStars;


    public void SetTotalClues (int total)
    {
        totalClues = total;
    }
    public void SetCluesObtained(int obtained)
    {
        cluesObtained = obtained;
    }

    private void SetTotalCluesText()
    {
        totalCluesText.SetText($"Encontraste {cluesObtained} pistas de {totalClues}.");
    }

    private void SetResultMessageText()
    {
        resultMessageText.SetText(resultMessages[totalStars-1]);
    }
    public void UpdateScorePanel()
    {
        CalculateTotalScoreStarImage();
        SetTotalCluesText();
        SetResultMessageText();

        for (int i = 0; i < totalStars; i++)
        {
            GameObject newStarObj = new GameObject($"Star_{i + 1}");
            newStarObj.transform.SetParent(panelScore.transform, false); 

            RawImage newStar = newStarObj.AddComponent<RawImage>();
            newStar.texture = scoreStarImage;
        }
    }
    private void CalculateTotalScoreStarImage()
    {
        float percentage = (cluesObtained / totalClues) * 100f;

        // Calcula las estrellas redondeando hacia abajo y asegurando un rango de 1 a 5
        // Se divide por 20 porque queremos que el total de estrellas no supere las 5, entonces sabiendo que percentage<=100
        // el resultado va a estar siempre dentro del rango de 1 a 5
        totalStars = Mathf.Clamp(Mathf.CeilToInt(percentage / 20f), 1, 5);

    }
}
