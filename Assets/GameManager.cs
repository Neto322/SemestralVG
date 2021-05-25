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

    [ServerRpc(RequireOwnership = false)]
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

        if(ReadyList == PlayerList)
        {
            foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
            {
                obj.PlayerObject.transform.position = new Vector3(-31,16.72f,-191);
                obj.PlayerObject.GetComponent<Playerr>().cam.transform.position = new Vector3(-28.94f,36.7f,-170.5f);
                obj.PlayerObject.GetComponent<Playerr>().cam.transform.eulerAngles = new Vector3(45f,180f,0f);
            }
        }
        

        
    }

   

  


}
