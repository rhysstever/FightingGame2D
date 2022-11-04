using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private GameObject player1Panel, player2Panel;

    private List<GameObject> playerPanels;

    // Start is called before the first frame update
    void Start()
    {
        playerPanels = new List<GameObject>();
        playerPanels.Add(player1Panel);
        playerPanels.Add(player2Panel);

        for(int i = 0; i < playerPanels.Count; i++)
            playerPanels[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "Player " + (i + 1) + " (" + transform.GetChild(i).GetComponent<PlayerInfo>().GetPlayerCharacter + ")";
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < playerPanels.Count; i++)
            playerPanels[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "Health: " + (int)transform.GetChild(i).GetComponent<PlayerCombat>().GetPlayerHealth();
    }
}
