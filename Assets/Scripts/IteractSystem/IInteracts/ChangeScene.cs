using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IInteractable
{
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string interactText;
    private bool canChangeScene;

    [Header("Cinematic")]
    [SerializeField] private float changeSceneTime;
    [SerializeField] private string sceneName;

    private void Start()
    {
        canChangeScene = false;
    }
    public string GetInteractText()
    {
        if (!canChangeScene)
        {
            interactText = "I need to grab my book first";
        }
        else
        {
            interactText = "press Left Click to intyeract";
        }
        
        return interactText;
    }
    
    public void SetActiveScript(bool active)
    {
        canChangeScene = active;
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if (canChangeScene)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }

    private void Update()
    {
        //changeSceneTime -= Time.deltaTime;
        //if (changeSceneTime < 0 ) 
        //{
        //    SceneManager.LoadScene(sceneName);
        //}
    }

}
