using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuiltySelector : MonoBehaviour
{
    [SerializeField] List<Suspect> _suspectList = new List<Suspect>();
    [SerializeField] List<Suspect> _GuiltyList = new List<Suspect>();
    private int _reputacion = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _suspectList.Count; i++)
        {
            if (_suspectList[i].isGuilty)
            {
                _GuiltyList.Add(_suspectList[i]);
                
            }       
        }


    }

    public void SelectSuspect(string Suspectname)
    {
        Suspect suspect = null;
        // Cambiar el estado isSelected del sospechoso en la lista principal
        for (int i = 0; i < _suspectList.Count; i ++)
        {
            Suspect s = _suspectList[i];
            _suspectList[i] = s;
            if (s.SuspectName == Suspectname)
            {
                _suspectList[i].isSelected = true;
                _suspectList[i].buttonIsPress = true;
                suspect = s;
                break;
            }
            
        }

        for (int i = 0; i < _GuiltyList.Count; i++)
        {
            Suspect s = _GuiltyList[i];
            _GuiltyList[i] = s;
            if (s.SuspectName == Suspectname)
            {
                _GuiltyList[i].isSelected = true;
                _GuiltyList[i].buttonIsPress = true;
                suspect = s;
                break;
            }

        }

        // Cambiar el estado isSelected del sospechoso
        suspect.isSelected = true;

        // Verificar si el botón está presionado
        if (suspect.buttonIsPress) 
        {

            // Cambiar el color del botón
            Image buttonImage = suspect.SuspectButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = Color.red;
            }
        }
    }

    public void SubmitGuess()
    {

        for (int i = 0; i < _suspectList.Count; i++)
        {
            if (_suspectList[i].isGuilty  && _suspectList[i].isSelected)
            {
                _reputacion += 3;
                Debug.Log(_reputacion);
            }
            else if (_suspectList[i].isGuilty && !_suspectList[i].isSelected)
            {
                _reputacion -= 2;
                Debug.Log(_reputacion);
            }
            else if (!_suspectList[i].isGuilty && _suspectList[i].isSelected)
            {
                _reputacion -= 2;
                Debug.Log(_reputacion);
            }
        }
      
    }
}
