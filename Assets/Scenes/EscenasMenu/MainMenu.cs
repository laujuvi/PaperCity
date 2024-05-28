using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string Scenename;
    [SerializeField] private string Scenename2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(Scenename);
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene(Scenename2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
