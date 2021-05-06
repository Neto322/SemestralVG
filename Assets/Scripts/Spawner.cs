using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
public class Spawner : NetworkBehaviour
{
    InputActions inputActions;
    
 
    private void OnEnable() {
        inputActions.Enable();    
    }
    private void OnDisable() {
        inputActions.Disable();    
    }

    // Update is called once per frame
   
}
