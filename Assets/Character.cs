using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class Character : NetworkBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLocalPlayer)
        transform.position += new Vector3(Input.GetAxis("Horizontal") * 3 * Time.deltaTime,0,Input.GetAxis("Vertical") * 3 * Time.deltaTime);    
    }
}
