using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterName : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;



    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        Debug.Log(cam);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);
    }
}
