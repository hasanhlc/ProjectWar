using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelSelectionUI;
    public GameObject settingsUI;
    public GameObject creditsUI;
    public GameObject introLevelOneUI;
    public RectTransform textTransform;
    public RectTransform textTransformCredits;
    private float scrollSpeed = 100f;
    public float endY = 1700f; // Distance to scroll the text
    // starty = -1000f; // Starting position of the text
    private float scrollSpeedCredits = 150f;
    public float endYCredits = 1700f;

    public Coroutine creditsCoroutine;
    public Coroutine IntroCoroutine;

    [Header("Ekranlar")]
    public GameObject MainMenuBackground;
    public GameObject CreditsBackground;
    public GameObject LevelSelectBackground;
    public GameObject SettingsBackground;

    [Header("Ekran Görselleri")]

    public Sprite MainMenuWar;
    public Sprite MainMenuPeace;
    public Sprite CreditsWar;
    public Sprite CreditsPeace;
    public Sprite LevelSelectWar;
    public Sprite LevelSelectPeace;
    public Sprite SettingsWar;
    public Sprite SettingsPeace;

    [Header("Button Parentları")]

    public GameObject MainMenuButtons;

    [Header("Button Görselleri")]

    public Sprite WarButtonSprite;
    public Sprite PeaceButtonSprite;

    public RectTransform LevelSelectButtonsParent;
    public GameObject LevelSelectionHeader;




    private bool isWar;

    public enum MenuState
    {
        MainMenu,
        LevelSelection,
        Settings,
        Credits,
        IntroLevel
    }

    private MenuState currentState;


    void Awake()
    {
        if (Random.value < 0.5f)
        {
            isWar = true;
        }
        else
        {
            isWar = false;
        }

        ChangeButtonSprites();
        SetLevelSelectionHeader();
    }

    public void ChangeButtonSprites()
    {
        if (isWar)
        {
            foreach (Transform button in MainMenuButtons.transform)
            {
                button.gameObject.GetComponent<Image>().sprite = WarButtonSprite;
            }
        }
        else
        {
            foreach (Transform button in MainMenuButtons.transform)
            {
                button.gameObject.GetComponent<Image>().sprite = PeaceButtonSprite;
            }
        }
    }

    public void SetLevelSelectionHeader()
    {
        if (isWar)
        {
            LevelSelectionHeader.GetComponent<TextMeshProUGUI>().fontSize = 65f;
            LevelSelectionHeader.GetComponent<TextMeshProUGUI>().color = new Color(0f, 173f, 0f);
            LevelSelectionHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(-154f ,256f);


        }
        else // peace
        {
            LevelSelectionHeader.GetComponent<TextMeshProUGUI>().fontSize = 85.3f;
            LevelSelectionHeader.GetComponent<TextMeshProUGUI>().color = new Color(255f, 255f, 255);
            LevelSelectionHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(-338f ,348f);
        }
    }
    private void Start()
    {
        OpenMainMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressBack();
            Debug.Log("Escape key pressed.");
        }
        if (Input.GetKeyDown(KeyCode.E) && currentState == MenuState.IntroLevel)
        {
            Debug.Log("E ye Basıldı");
            StopCoroutine(IntroCoroutine);
            LoadScene("SampleScene");
        }
    }


    public void IntroLevelOne()
    {
        CloseAllMenus();
        introLevelOneUI.SetActive(true);
        currentState = MenuState.IntroLevel;
        textTransform.anchoredPosition = new Vector2(0, -1000f); // Starting position of the text
        IntroCoroutine = StartCoroutine(ScrollUp());
    }

    IEnumerator ScrollUp()
    {
        while (textTransform.anchoredPosition.y < endY)
        {
            Vector2 pos = textTransform.anchoredPosition;
            pos.y += scrollSpeed * Time.deltaTime;
            textTransform.anchoredPosition = pos;

            yield return null; // Bir frame bekle
        }
        // Scroll işlemi tamamlandığında yapılacak işlemler
        // burada fade out eklenecek
        LoadScene("SampleScene");
    }
    IEnumerator ScrollUpCredits()
    {
        while (textTransformCredits.anchoredPosition.y < endYCredits)
        {
            Vector2 pos = textTransformCredits.anchoredPosition;
            pos.y += scrollSpeedCredits * Time.deltaTime;
            textTransformCredits.anchoredPosition = pos;

            yield return null; // Bir frame bekle
        }
        OpenMainMenu();

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
        levelSelectionUI.SetActive(true);
        currentState = MenuState.LevelSelection;

        if (isWar)
        {
            LevelSelectBackground.GetComponent<Image>().sprite = LevelSelectWar;
            LevelSelectButtonsParent.anchoredPosition = new Vector2(0f ,-40f);
        }
        else
        {
            LevelSelectBackground.GetComponent<Image>().sprite = LevelSelectPeace;
            LevelSelectButtonsParent.anchoredPosition = new Vector2(0f, -118f);
        }

    }

    public void OpenMainMenu()
    {
        CloseAllMenus();
        currentState = MenuState.MainMenu;
        mainMenuUI.SetActive(true);

        if (isWar)
        {
            MainMenuBackground.GetComponent<Image>().sprite = MainMenuWar;
        }
        else
        {
            MainMenuBackground.GetComponent<Image>().sprite = MainMenuPeace;
        }
    }

    public void OpenSettings()
    {
        CloseAllMenus();
        settingsUI.SetActive(true);
        currentState = MenuState.Settings;
        if (isWar)
        {
            SettingsBackground.GetComponent<Image>().sprite = SettingsWar;
        }
        else
        {
            SettingsBackground.GetComponent<Image>().sprite = SettingsPeace;
        }
    }

    public void OpenCredits()
    {
        CloseAllMenus();
        creditsUI.SetActive(true);
        currentState = MenuState.Credits;
        textTransformCredits.anchoredPosition = new Vector2(0, -1350f); // Starting position of the text
        creditsCoroutine = StartCoroutine(ScrollUpCredits());
        if (isWar)
        {
            CreditsBackground.GetComponent<Image>().sprite = CreditsWar;
        }
        else
        {
            CreditsBackground.GetComponent<Image>().sprite = CreditsPeace;
        }
    }

    public void CloseAllMenus()
    {
        mainMenuUI.SetActive(false);
        levelSelectionUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        introLevelOneUI.SetActive(false);
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
                StopCoroutine(creditsCoroutine);
                break;
        }
    }
}
