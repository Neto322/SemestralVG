using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using UnityEngine.UI;
using TMPro;
using MLAPI.NetworkVariable;

public class Spawner : NetworkBehaviour
{

    [SerializeField]
    GameObject character;

    GameObject _character;

    InputActions inputActions;






    void Awake()
    {
        
        inputActions = new InputActions();
  
      
    }

    private void Start() {
        if(IsLocalPlayer)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


    private void OnEnable() {
      
       inputActions.Enable();
       
        
    }



    private void OnDisable() {
      
       inputActions.Disable();
       
    }



    [ServerRpc]    
    private void SpawnCharacterServerRpc(ulong netID){

        
        Debug.Log("Cliente quiere llamar");
        GameObject go = Instantiate(character);
        

        go.GetComponent<NetworkObject>().SpawnWithOwnership(netID);

        

        ulong itemNetID = go.GetComponent<NetworkObject>().NetworkObjectId;

        SpawnCharacterClientRpc(itemNetID);

    }


    [ClientRpc]
    private void SpawnCharacterClientRpc(ulong itemNetID)
    {
        Debug.Log("Client is equipping");

        
        Destroy(this.gameObject);
        // NetworkObject netObj = NetworkSpawnManager.SpawnedObjects[itemNetID];



       


    }



 


    public void StartGame(){
        Debug.Log("Lets go");
        if(IsLocalPlayer)
        {
            Debug.Log("Lets gogggggg");
            SpawnCharacterServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }
    
}
