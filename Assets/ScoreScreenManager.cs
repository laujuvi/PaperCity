using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ProfileType { Entusiasta, Osado, Meticuloso, Psicológico, Implacable };

[System.Serializable]
public class ScoreData
{
    public string profileTitle;
    public string profileText;
    public string profileComment;
    public Texture image;
}

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject panelScore;

    [Header("Imagenes")]
    [SerializeField] RawImage imagePersonaje;

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI profileTitleText;
    [SerializeField] TextMeshProUGUI profileText;
    [SerializeField] TextMeshProUGUI profileCommentText;

    [Header("Perfiles")]
    [SerializeField] List<ScoreData> profileList;

    private int cluesObtained;
    private int minClues;
    private int maxClues;
    private bool hasTalkedAllNPCs;

    private string profileType;


    private void UpdateProfileText(string title, string content , string comment )
    {
        profileTitleText.SetText(title);
        profileText.SetText(content);
        profileCommentText.SetText(comment);
    }

    private void UpdateProfileImage(Texture texture)
    {
        imagePersonaje.texture = texture;
    }

    public void UpdateScorePanel()
    {
        CheckMessageShowing();
        SelectProfileToShow();
    }

    public void SetScoreValues(DataCollected dataCollected)
    {
        maxClues = dataCollected.maxClues;
        minClues = dataCollected.minClues;
        cluesObtained = dataCollected.cluesObtained;
        hasTalkedAllNPCs = dataCollected.hasTalkedAllNPCs;
    }

    private void SelectProfileToShow()
    {
        foreach (var profile in profileList)
        {
            if (profile.profileTitle == profileType)
            {
                UpdateProfileText(profile.profileTitle, profile.profileText, profile.profileComment);
                UpdateProfileImage(profile.image);
            }
        }
    }

    private string CheckMessageShowing() {

        if (hasTalkedAllNPCs)
        {
            // Este caso no fue contemplado
            //if (cluesObtained < maxClues && cluesObtained > minClues) return;

            // IMPLACABLE
            if (cluesObtained == maxClues) return profileType = ProfileType.Implacable.ToString();

            //PSICOLOGICO
            if (cluesObtained == minClues) return profileType = ProfileType.Psicológico.ToString();

        }

        // METICULOSO
        if (cluesObtained == maxClues) return profileType  = ProfileType.Meticuloso.ToString();

        //OSADO
        if (cluesObtained == minClues) return profileType  = ProfileType.Osado.ToString();

        return profileType = ProfileType.Entusiasta.ToString();
    }
}
