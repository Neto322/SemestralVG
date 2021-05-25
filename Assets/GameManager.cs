using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using MLAPI;
using MLAPI.Messaging;

public class GameManager : MonoBehaviour
{   
    public NetworkVariable<int> clientsList = new NetworkVariable<int>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, 100);

    public static GameManager instance = null;

  

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

   
  
    private void OnGUI() {
        
       
    
    }

    [ServerRpc]
    public void GameStartServerRpc ()
    {
        

        
            foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
            {   
                Debug.Log("Hay aqui " + obj.PlayerObject.name);

               obj.PlayerObject.GetComponent<Playerr>().gameStates = Playerr.GameStates.GameStart;
               
                
            }
        


     
        }
        

      
    }

   

  



