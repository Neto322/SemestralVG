using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using System;

public class ConnectionManager : MonoBehaviour
{

    //Pasa del lado del server.
    public void Host()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "Password1234";
        callback(true,null,approve,Vector3.zero,Quaternion.identity);
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
}
