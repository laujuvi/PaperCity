using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject panelScore;

    [Header("Imagenes")]
    [SerializeField] Texture scoreStarImage; 
    [SerializeField] int totalStars;
    private int totalClues;


    private void Start()
    {
        UpdateScorePanel();
    }
    public void UpdateScorePanel()
    {
        for (int i = 0; i < totalStars; i++)
        {
            GameObject newStarObj = new GameObject($"Star_{i + 1}");
            newStarObj.transform.SetParent(panelScore.transform, false); 

            RawImage newStar = newStarObj.AddComponent<RawImage>();
            newStar.texture = scoreStarImage;
        }
    }
}
