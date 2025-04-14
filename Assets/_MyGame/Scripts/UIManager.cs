using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;
    public GameObject settingsUI;
    public GameObject creditsUI;

    public enum MenuState
    {
        MainMenu,
        LevelSelection,
        Settings,
        Credits
    }

    private MenuState currentState;


    private void Start()
    {
        currentState = MenuState.MainMenu;
        mainMenuUI.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressBack();
            Debug.Log("Escape key pressed.");
        }
    } 

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }

    public void OpenLevelSelection()
    {
        CloseAllMenus();
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(true);
        currentState = MenuState.LevelSelection;
    }

    public void OpenSettings()
    {
        CloseAllMenus();
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
        currentState = MenuState.Settings;
    }

    public void OpenCredits()
    {
        CloseAllMenus();
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        currentState = MenuState.Credits;
    }

    public void CloseAllMenus()
    {
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    private void pressBack()
    {
        switch (currentState)
        {
        case MenuState.MainMenu:
            QuitGame();
            break;
        case MenuState.LevelSelection:
            levelSelectionUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            break;
        case MenuState.Settings:
            settingsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            break;
        case MenuState.Credits:
            creditsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            currentState = MenuState.MainMenu;
            break;
        }
    }
}
