using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IInteractable
{
    [SerializeField] private SceneLoadManager sceneLoadManager;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string interactText;
    [SerializeField] private bool canChangeScene;

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
            interactText = "No puedo irme sin mi libreta detectivesca.";
        }
        else
        {
            interactText = "Comenzar a investigar.";
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
            //SceneManager.LoadScene(sceneToLoad);
            sceneLoadManager.LoadNextScene();
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            changeSceneTime -= Time.deltaTime;
            if (changeSceneTime <= 0)
            {
                sceneLoadManager.LoadNextScene();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                sceneLoadManager.LoadNextScene();
            }
        }
        
    }
}
