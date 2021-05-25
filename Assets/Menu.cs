using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;

public class Menu : MonoBehaviour
{
  [SerializeField]
  GameObject UI;
      void OnGUI()
    {
            if(NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsHost)
            {
                if (GUI.Button(new Rect(Screen.width * 0.05f,Screen.height * 0.05f, 100, 40), "Leave Match"))
            {
                if (NetworkManager.Singleton.IsHost)
                {
                                     UI.SetActive(true);
                    NetworkManager.Singleton.StopHost();
                }

                else if (NetworkManager.Singleton.IsClient)
                {
                                      UI.SetActive(true);

                   NetworkManager.Singleton.StopClient();
                }
            }
            }
            else
            {
              if (GUI.Button(new Rect(Screen.width * 0.1f,Screen.height * 0.9f, 100, 50),"Create Game" ))
              {
                  UI.SetActive(false);
                  NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
                  NetworkManager.Singleton.StartHost(new Vector3(0,1.3f,0),Quaternion.identity);

              }

              if (GUI.Button(new Rect(Screen.width * 0.4f,Screen.height * 0.9f, 100, 50),"Join Game" ))
              {
                  UI.SetActive(false);
                  NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Password1234");
                  
                  NetworkManager.Singleton.StartClient();

              }

              if (GUI.Button(new Rect(Screen.width * 0.7f,Screen.height * 0.9f, 100, 50),"Quit Game" ))
              {
                  
                  Application.Quit();

              }

            }
             

    }

     private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "Password1234";
        callback(true,null,approve,  new Vector3(0,1.3f,0) ,Quaternion.identity);
    }

}
