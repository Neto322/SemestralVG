using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Exceptions;
using MLAPI.Prototyping;
using TMPro;
using MLAPI.Transports.PhotonRealtime;
using MLAPI.NetworkVariable;
using Photon.Realtime;

public class Playerr : NetworkBehaviour
{

    
  
     InputActions inputActions;
   
    Rigidbody rigidbody;

    float angle;
    [SerializeField]
    float Turnspeed;

    [SerializeField]
    float speed;

    
    Transform cam;


    Quaternion targetRotation;

    public Animator anim;

    [SerializeField]
   float maxSpeed;

   float BlendValue;

   int LocalID = 0;

 

   
    public NetworkVariable<string> nombrenNet = new NetworkVariable<string>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, "Name");

  
     public GUIStyle  textfieldStyle;

     public GUIStyle  titlestyle;





    bool nameset = false;

    public NetworkVariable<int> characterID = new NetworkVariable<int>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, 100);


    [SerializeField]
    GameObject[] characters;

    [SerializeField]
    Camera playercam;


     enum Estados {Idle,Playing,Death}

        Estados states = Estados.Idle;


    [SerializeField]
    TextMeshPro text;


    Collider[] enemiesHit;

    [SerializeField]
    LayerMask playerlayer;


    [SerializeField]
    float range;
    
 

    void Awake()
    {
        
        inputActions = new InputActions();
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;



        
    }

    private void OnEnable() {
        inputActions.Enable();
      
            
     }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void Start() {



    }
    
  
    // Update is called once per frame
    void Update()
    {
       if(IsLocalPlayer)
       {
           
           if(nameset == true)
           {
               Debug.Log("Activo");
                SetNameServerRpc();

           }
           else
           {
                Debug.Log("Inactivo");
           }

     

            
            switch(states)
            {
                case Estados.Idle:
              

                
                break;

                    case Estados.Playing:

                    if(AxisInput != Vector2.zero)
                                {
                                    angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                                    angle = Mathf.Rad2Deg * angle;
                                    angle += cam.eulerAngles.y;
                                    targetRotation = Quaternion.Euler(0,angle,0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                                }
                                
                                rigidbody.AddForce(transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime,ForceMode.Impulse);

                                //transform.position += transform.forward * (15 * MovementAxis.magnitude) * Time.deltaTime;
                    

                                if(inputActions.Movement.Action.triggered)
                                {
                                    Attack();
                                }
                                

                break;
            }    

      



        }
            
            
       
       
    }

  


    void  Attack()
    {

        enemiesHit = Physics.OverlapSphere(transform.position,range,playerlayer);

        foreach(Collider enemy in enemiesHit)
        {
            
        }


    }

 
    private void LateUpdate() {
       if(IsLocalPlayer)
       {
           

            switch(states)
            {
                case Estados.Idle:
              

                
                break;

                    case Estados.Playing:

                       if(anim)
                            {
                                if(AxisInput != Vector2.zero)
                                    {
                                        anim.SetBool("Move",true);

                                    } 

                                    if(inputActions.Movement.Action.triggered)
                                    {
                                        anim.SetTrigger("Punch");
                                    }
                                        
                                    BlendValue = rigidbody.velocity.magnitude / maxSpeed;

                                    BlendValue = Mathf.Clamp(BlendValue,0,1);
                                    
                                    anim.SetFloat("Blending",BlendValue);
                            }   
                                                    

                break;
            }

               
                  
                 

       }
            
            


            

        
    }



    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,range);
    }



  
    [ServerRpc]
    void SetNameServerRpc()
    {
        
        
            SetNameClientRpc();

        
    }


    [ClientRpc]
    void SetNameClientRpc()
    {    
           
        
            text.text = nombrenNet.Value;

            text.transform.LookAt(cam.position,Vector3.up);

            text.transform.eulerAngles = new Vector3(45,180,0);

          
                 
            characters[characterID.Value].SetActive(true);

            
          
            
             

            


     



        

        
         
     

    }
    


    [ServerRpc]
    void CleanNamesServerRpc(int LocalID,ulong netID)
    {
      
        CleanNamesClientRpc(LocalID);
    }

   [ClientRpc]
    void CleanNamesClientRpc(int LocalID)
    {
        Debug.Log("Papito");
        characterID.Value = LocalID;

          for(int i = 0; i < characters.Length; i++)
            {
                if(characters[i].GetComponent<Animator>() != anim)
                {
                    Debug.Log("Este no es el bueno");
                    characters[i].SetActive(false);
                }
                else
                {
                    Debug.Log("Este si >:) que es " + characters[characterID.Value].name);
                }
            }

        
        characters[characterID.Value].SetActive(true);
        anim = GetComponentInChildren<Animator>();




    }



   
  
  
     void OnGUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        // draw the name with a shadow (colored for buf)	

        


    
       

        

         if(IsLocalPlayer)
       {
            if(nameset == false)
            GUI.Label(new Rect(Screen.width * 0.5f , Screen.height * 0.1f , 100 , 30 ),"Select Fighter", titlestyle);

          


           if(nameset == false)
        nombrenNet.Value = GUI.TextField(new Rect( Screen.width * 0.43f ,Screen.height * 0.6f , 400, 160), nombrenNet.Value, 8,textfieldStyle);


        if(nameset == false)
        {
                characters[LocalID].SetActive(true);
               if (GUI.Button(new Rect(Screen.width * 0.3f,Screen.height * 0.5f, 180, 60), "<"))
                {  
                  if(LocalID == 0)
                    {
                        characters[LocalID].SetActive(false);
                        LocalID = 7;
                        characterID.Value = LocalID;
                        characters[LocalID].SetActive(true);


                    }
                    else
                    {
                        characters[LocalID].SetActive(false);
                        LocalID--;
                        characterID.Value = LocalID;
                        characters[LocalID].SetActive(true);



                    }
                    
                    anim = GetComponentInChildren<Animator>();;



                }

                 if (GUI.Button(new Rect(Screen.width * 0.7f,Screen.height * 0.5f, 180, 60), ">"))
                {
                    if(LocalID == 7)
                    {
                        characters[LocalID].SetActive(false);
                        LocalID = 0;
                        characterID.Value = LocalID;
                        characters[LocalID].SetActive(true);


                    }
                    else
                    {
                        characters[LocalID].SetActive(false);
                        LocalID++;
                        characterID.Value = LocalID;
                        characters[LocalID].SetActive(true);



                    }

                   




                }

              if (GUI.Button(new Rect(Screen.width * 0.5f,Screen.height * 0.8f, 180, 60), "Start Game"))
                {
                    states = Estados.Playing;
                    nameset = true;
                    for(int i = 0; i < characters.Length; i++)
                    {
                        characters[i].SetActive(false);
                    }
                    playercam.gameObject.SetActive(false);
                    anim = GetComponentInChildren<Animator>();
                    CleanNamesServerRpc(LocalID,NetworkManager.Singleton.LocalClientId);

                }
        }
      
       }
           
       }   
    

    
    Vector2 AxisInput => inputActions.Movement.Axis.ReadValue<Vector2>();

    Vector3 MovementAxis => new Vector3(AxisInput.x, 0f, AxisInput.y);

   

}
