using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;


public class ConnectionManager : MonoBehaviour
{   
    Vector3 Position;

    //Pasa del lado del server.
    public void Host()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(RandomPosition(),Quaternion.identity);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "Password1234";
        callback(true,null,approve,  RandomPosition() ,Quaternion.identity);
    }

    public void Join()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Password1234");
        NetworkManager.Singleton.StartClient();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomPosition()
    {

        float X = Random.Range(-5.0f, 5.0f);
        float Y = 3;

        return new Vector3(X,Y,0);

    }
}
