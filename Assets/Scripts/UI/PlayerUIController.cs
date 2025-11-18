using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject player;

    void Start()
    {
        uiPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartScene);
        returnButton.onClick.AddListener(ReturnToMainMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void OnPlayerDeath()
    {
        uiPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}