using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static PlayerManager instance = null;

    // Awake is called even before start 
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

    private PlayerInfo[] playerInfos;

    // Start is called before the first frame update
    void Start()
    {
        playerInfos = GetComponentsInChildren<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerInfo GetPlayerInfoByPlayerNum(int playerNum)
	{
        return playerInfos[--playerNum];
	}

    public PlayerInfo GetPlayerInfoByIndex(int index)
    {
        return playerInfos[index];
    }

    public void FreezePlayers(bool isFrozen)
    {
        foreach(PlayerInfo playerInfo in playerInfos)
		{
            if(isFrozen)
                playerInfo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            else
                playerInfo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
