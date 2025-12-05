using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMenuComponent : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button restartButton;
    private Button mainMenuButton;
    private Button exitButton;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        mainMenuButton = uiDocument.rootVisualElement.Q<Button>("MainMenuButton");
        exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");

        restartButton.RegisterCallback<ClickEvent>(OnRestartClick);
        mainMenuButton.RegisterCallback<ClickEvent>(OnMainMenuClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        
        HidePlayerMenu();
    }

    void Start()
    {
        SaveSystem.LoadGame();
    }

    public void ShowPlayerMenu()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        Time.timeScale = 0f;
    }

    private void HidePlayerMenu()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void OnPlayerDeath()
    {
        ShowPlayerMenu();
        SaveSystem.SaveGame();
    }

    private void OnRestartClick(ClickEvent clickEvent)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnMainMenuClick(ClickEvent clickEvent)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");        
    }

    private void OnExitClick(ClickEvent clickEvent)
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();        
    }

    private void OnDisable()
    {
        restartButton?.UnregisterCallback<ClickEvent>(OnRestartClick);
        mainMenuButton?.UnregisterCallback<ClickEvent>(OnMainMenuClick);
        exitButton?.UnregisterCallback<ClickEvent>(OnExitClick);
    }
}
