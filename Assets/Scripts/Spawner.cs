using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using UnityEngine.UI;
using TMPro;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
public class Spawner : NetworkBehaviour
{

    [SerializeField]
    GameObject character;

    GameObject _character;

    InputActions inputActions;

    [SerializeField]
    Sprite[] monos;

    [SerializeField]
    Image imagen;

    int i = 0;

    void Awake()
    {        

        inputActions = new InputActions();

    }

    private void Start() {

        imagen.sprite = monos[i];
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
        

        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(netID);

        
        Debug.Log("Spawner " + i);
      
        Debug.Log("ID de mono " + go.GetComponent<Playerr>().characterID.Value );
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
    
    public void Right()
    {
        if(i == 7)
        {
            i = 0;
            
            imagen.sprite = monos[i];

            return;
        }
        else
        {
            i++;

            imagen.sprite = monos[i];

        }
    }

      public void Left()
    {
        if(i == 0)
        {
            i = 7;
            imagen.sprite = monos[i];
            return;
        }
        else
        {
            i--;

            imagen.sprite = monos[i];
            
        }
    }
}
