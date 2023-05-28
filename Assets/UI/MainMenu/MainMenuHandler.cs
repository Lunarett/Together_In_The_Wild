using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private int _singleplayerLevelIndex = 1;

    private VisualElement _rootElement;

    private const string SINGLE_PLAYER_BTN_NAME = "SingleplayerButton";
    private const string MULTI_PLAYER_BTN_NAME = "MultiplayerButton";
    private const string SETTINGS_BTN_NAME = "SettingsButton";
    private const string EXIT_BTN_NAME = "ExitButton";

    private void Awake()
    {
        _rootElement = GetComponent<UIDocument>().rootVisualElement;

        // Bind Click Events
        _rootElement.Q<Button>(SINGLE_PLAYER_BTN_NAME).clicked += Singleplayer_OnClicked;
        _rootElement.Q<Button>(MULTI_PLAYER_BTN_NAME).clicked += Multiplayer_OnClicked;
        _rootElement.Q<Button>(SETTINGS_BTN_NAME).clicked += Settings_OnClicked;
        _rootElement.Q<Button>(EXIT_BTN_NAME).clicked += Exit_OnClicked;
    }

    private void Singleplayer_OnClicked()
    {
        Debug.Log("SingleP");
    }

    private void Multiplayer_OnClicked()
    {
        Debug.Log("MultiP");
    }

    private void Settings_OnClicked()
    {
        Debug.Log("Settings");
    }

    private void Exit_OnClicked()
    {
        Debug.Log("Exit");
    }
}
