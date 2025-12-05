using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMenuComponent : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button restartButton;
    private Button mainMenuButton;
    private Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPlayerMenu()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void HidePlayerMenu()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnRestartClick(ClickEvent clickEvent)
    {
        SceneManager.LoadScene("Main");
    }

    private void OnMainMenuClick(ClickEvent clickEvent)
    {
        SceneManager.LoadScene("MainMenu");        
    }

    private void OnExitClick(ClickEvent clickEvent)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();        
    }

    private void OnDisable()
    {
        restartButton.UnregisterCallback<ClickEvent>(OnRestartClick);
        mainMenuButton.UnregisterCallback<ClickEvent>(OnMainMenuClick);
        exitButton.UnregisterCallback<ClickEvent>(OnExitClick);
    }
}
