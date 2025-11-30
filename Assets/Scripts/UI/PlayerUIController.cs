using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] public HUDComponent HUD;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI boomBucksCounter;

    [SerializeField] public GlobalData globalData;

    void Start()
    {
        uiPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartScene);
        returnButton.onClick.AddListener(ReturnToMainMenu);
        exitButton.onClick.AddListener(ExitGame);
        SaveSystem.LoadGame();
    }

    void Update()
    {
        if (player != null && boomBucksCounter != null)
        {
            boomBucksCounter.text = $": {globalData.boombucks}";
        }
    }

    public void OnPlayerDeath()
    {
        uiPanel.SetActive(true);
        Time.timeScale = 0f;
        SaveSystem.SaveGame();
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