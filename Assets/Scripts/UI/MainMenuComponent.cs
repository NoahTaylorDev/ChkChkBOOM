using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;

public class MainMenuComponent : MonoBehaviour
{
    private UIDocument uiDocument;

    private ShopUIController shopController;
    private Button startButton;
    private Button exitButton;
    private Button shopButton;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        shopController = FindFirstObjectByType<ShopUIController>();
        startButton = uiDocument.rootVisualElement.Q("StartButton") as Button;
        exitButton = uiDocument.rootVisualElement.Q("ExitButton") as Button;
        shopButton = uiDocument.rootVisualElement.Q("ShopButton") as Button;

        startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitClick);
        shopButton.RegisterCallback<ClickEvent>(OnShopClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Main");
    }

    private void OnExitClick(ClickEvent evt)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    private void OnShopClick(ClickEvent evt)
    {
        shopController.ShowShop();
    }

    void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        exitButton.UnregisterCallback<ClickEvent>(OnExitClick);
        shopButton.UnregisterCallback<ClickEvent>(OnShopClick);
    }
}
