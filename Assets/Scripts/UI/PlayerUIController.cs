using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI boomBucksCounter;

    void Start()
    {
        uiPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartScene);
        returnButton.onClick.AddListener(ReturnToMainMenu);
        exitButton.onClick.AddListener(ExitGame);
        
        // Only search if not already assigned in Inspector
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        // Warn if still null
        if (player == null)
        {
            Debug.LogError("Player not found in scene!");
        }

        if (boomBucksCounter == null)
        {
            Debug.LogError("BoomBucks counter not assigned!");
        }
    }

    void Update()
    {
        // Add null check before accessing
        if (player != null && boomBucksCounter != null)
        {
            boomBucksCounter.text = $": {player.Boombucks}";
        }
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