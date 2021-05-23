using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;

public class CharacterSelect : NetworkBehaviour
{
    GameObject UI;
    
    [SerializeField]
    GameObject character;

    GameObject _character;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }


  
    [ServerRpc]
    private void SelectCharacterServerRpc(ulong netID)
    {
        GameObject go = Instantiate(character);
        go.GetComponent<NetworkObject>().SpawnWithOwnership(netID);

        
        
        ulong itemNetID = go.GetComponent<NetworkObject>().NetworkObjectId;

        SelectCharacterClientRpc(itemNetID);
    }

    [ClientRpc]
    private void SelectCharacterClientRpc(ulong itemNetID)
    {
        NetworkObject netObj = NetworkSpawnManager.SpawnedObjects[itemNetID];

        _character = netObj.gameObject;
        
       

    }



    public void StartGame(){

        if(IsLocalPlayer)
        {
          
            SelectCharacterServerRpc(NetworkManager.Singleton.LocalClientId);
            
        }
    }

}
