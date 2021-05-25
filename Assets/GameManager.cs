using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using MLAPI;
using MLAPI.Messaging;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    int PlayerList;

    int ReadyList;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ServerRpc]
    public void GameStartServerRpc ()
    {
        PlayerList = 0;
        
        ReadyList = 0;

        foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
        {
            if(obj.PlayerObject.GetComponent<Playerr>().gameStates == Playerr.GameStates.Ready)
            {
                ReadyList++;
            }
        }

         foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
        {
            PlayerList++;
        }

        
    }

    [ServerRpc]
    public void PositionPlayers()
    {
        
    }

  


}
