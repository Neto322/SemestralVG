using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;


public class ConnectionManager : MonoBehaviour
{   
    Vector3 Position;

    [SerializeField]
    GameObject UI;




    //Pasa del lado del server.
    public void Host()
    {
        UI.SetActive(false);
      

        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(randomPos(),Quaternion.identity);

        
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "Password1234";
        callback(true,null,approve,  randomPos(),Quaternion.identity);
    }

    public void Join()
    {
        UI.SetActive(false);

        
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Password1234");
        NetworkManager.Singleton.StartClient();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 randomPos()
    {
        float x = Random.Range(0,40);

        float y = 1.3f;

        float z = Random.Range(0,40);

        return new Vector3(x,y,z);
    }

  
}
