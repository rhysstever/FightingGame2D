using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    MainMenu,
    Select,
    Game,
    Pause,
    PostGame
}

public class UIManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static UIManager instance = null;

    // Awake is called even before start 
    // (I think its at the very beginning of runtime)
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    #region UI Elements
    [SerializeField]
    private Canvas canvas;

	[SerializeField]    // Empty gameobj parents 
    private GameObject mainMenuParent, selectParent, gameParent, pauseParent, postGameParent;

    [SerializeField]    // Main Menu buttons
    private GameObject playButton, quitButton;

    [SerializeField]    // Select text
    private GameObject p1ReadyText, p2ReadyText;

    [SerializeField]    // Pause buttons
    private GameObject pauseToMainMenuButton;

    [SerializeField]    // Post Game buttons
    private GameObject postGameToMainMenuButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GetCurrentMenuState() == MenuState.Select)
		{
            // Update text based on player readiness 
            p1ReadyText.GetComponent<TMP_Text>().text 
                = GameManager.instance.GetPlayer(true)
                .GetComponent<Character2DController>()
                .IsReady() ? "Ready" : "Not Ready";
            p2ReadyText.GetComponent<TMP_Text>().text 
                = GameManager.instance.GetPlayer(false)
                .GetComponent<Character2DController>()
                .IsReady() ? "Ready" : "Not Ready";
        }
    }

    private void SetupUI()
	{
        // Main Menu buttons
        playButton.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.Select));
        quitButton.GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        // Pause buttons
        pauseToMainMenuButton.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.MainMenu));
        // Post Game buttons
        postGameToMainMenuButton.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.MainMenu));
    }

    public void ChangeUIState(MenuState currentMenuState)
	{
        // Deactivate all menu state parents
        foreach(Transform child in canvas.transform)
            child.gameObject.SetActive(false);

        // Do menu-specific logic
        switch(currentMenuState)
        {
            case MenuState.MainMenu:
                mainMenuParent.SetActive(true);
                break;
            case MenuState.Select:
                selectParent.SetActive(true);
                break;
            case MenuState.Game:
                gameParent.SetActive(true);
                break;
            case MenuState.Pause:
                pauseParent.SetActive(true);
                break;
            case MenuState.PostGame:
                postGameParent.SetActive(true);
                break;
        }
    }
}
