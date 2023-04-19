using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject titleUIParent, characterSelectUIParent, gameUIParent, pauseUIParent, gameEndUIParent;
    [SerializeField]
    private GameObject playButton, pauseToTitleButton, gameEndToTitleButton;
    [SerializeField]
    private List<GameObject> playerPanels;

    // Start is called before the first frame update
    void Start()
    {
        SetupUI();
    }

    private void SetupUI()
	{
        // Setup onClicks
        playButton.GetComponent<Button>().onClick.AddListener(() => MenuManager.instance.ChangeMenuState(MenuState.CharacterSelect));
        pauseToTitleButton.GetComponent<Button>().onClick.AddListener(() => MenuManager.instance.ChangeMenuState(MenuState.Title));
        gameEndToTitleButton.GetComponent<Button>().onClick.AddListener(() => MenuManager.instance.ChangeMenuState(MenuState.Title));
    }

    public void UpdateMenuStateUI(MenuState menuState)
    {
        // Deactivate all empty gameObject parents
        foreach(Transform childTrans in canvas.transform)
            childTrans.gameObject.SetActive(false);

        // Activate the right empty parent gameObject
        switch(menuState)
        {
            case MenuState.Title:
                titleUIParent.SetActive(true);
                break;
            case MenuState.CharacterSelect:
                characterSelectUIParent.SetActive(true);
                break;
            case MenuState.Game:
                gameUIParent.SetActive(true);

                for(int i = 0; i < playerPanels.Count; i++)
                {
                    Character character = PlayerManager.instance.GetPlayerInfoByIndex(i).GetPlayerCharacter;
                    playerPanels[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "Player " + (i + 1) + " (" + character + ")";
                }
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.GameEnd:
                gameEndUIParent.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < playerPanels.Count; i++)
		{
            int playerHealth = (int)PlayerManager.instance.GetPlayerInfoByIndex(i).GetCurrentHealth;
            playerPanels[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "Health: " + playerHealth;
		}
    }

    private void SetupCharacterUI()
	{
        
    }
}
