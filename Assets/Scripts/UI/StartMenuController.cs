using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject progressionMenu;

    void Start()
    {
        progressionMenu.SetActive(false);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void OnShopClick()
    {
        progressionMenu.SetActive(true);
        
    }
}
