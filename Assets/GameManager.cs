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
        foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
        {
            Debug.Log("Jugador " + obj.PlayerObject.name + " esta " +  obj.PlayerObject.GetComponent<Playerr>().gameStates);
        }
    }

  


}
