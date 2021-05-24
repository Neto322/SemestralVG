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

    Camera cam;


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

 

     enum Estados {Idle,Playing,Death}

        Estados states = Estados.Idle;


    [SerializeField]
    TextMeshPro text;


    Collider[] enemiesHit;

    [SerializeField]
    LayerMask playerlayer;


    [SerializeField]
    float range;

    

    [SerializeField]
    Transform punchposition;

    
    
    float punchrate = 1;

    float netxtpunch = 0;

    void Awake()
    {
        
        inputActions = new InputActions();
        rigidbody = GetComponent<Rigidbody>();
        



        
    }

    private void OnEnable() {
        inputActions.Enable();
      
            
     }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void Start() {
        
        cam = gameObject.GetComponentInChildren<Camera>();



    }
    
  
    // Update is called once per frame
    void Update()
    {
        if(!IsLocalPlayer)
        {
            cam.gameObject.SetActive(false);
        }
       if(IsLocalPlayer)
       {
           
           if(nameset == true)
           {
                SetNameServerRpc();

           }
           else
           {

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
                                    angle += cam.transform.eulerAngles.y;
                                    targetRotation = Quaternion.Euler(0,angle,0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                                }
                                
                                rigidbody.AddForce(transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime,ForceMode.Impulse);

                                //transform.position += transform.forward * (15 * MovementAxis.magnitude) * Time.deltaTime;

                           
                                if(inputActions.Movement.Action.triggered && Time.time > netxtpunch)
                                {
                                    anim.SetTrigger("Punch");
                                    netxtpunch = Time.time + punchrate;
                                    rigidbody.AddForce(transform.forward * 5500 * Time.deltaTime,ForceMode.Impulse);
                                    AttackPlayerServerRpc();
                                }
                                

                break;
            }    

      



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

                                   
                                        
                                    BlendValue = rigidbody.velocity.magnitude / maxSpeed;

                                    BlendValue = Mathf.Clamp(BlendValue,0,1);
                                    
                                    anim.SetFloat("Blending",BlendValue);
                            }   
                                                    

                break;
            }

               
                  
                 

       }
            
            


            

        
    }



    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(punchposition.position,range);
    }

    [ServerRpc]
    void AttackPlayerServerRpc()
    {
        
        AttackPlayerClientRpc();
    }

    
    
    [ClientRpc]
    void AttackPlayerClientRpc()
    {
        foreach(var obj in NetworkManager.Singleton.ConnectedClientsList)
        {
          
           
            
            enemiesHit = Physics.OverlapSphere(punchposition.position,range,playerlayer);
              
            
           foreach(Collider col in enemiesHit)
           {    
               if(obj.PlayerObject.GetComponent<NetworkObject>().NetworkObjectId == col.GetComponent<NetworkObject>().NetworkObjectId)
                {
                    Debug.Log("Si pega" + obj.PlayerObject.name);
                    GameObject gameObj = obj.PlayerObject.gameObject;
                    Vector3 direction = gameObj.transform.position -  transform.position;

                    gameObj.GetComponent<Playerr>().KnockbackServerRpc(direction);


                }
           }
    
        }
    }


   [ServerRpc(RequireOwnership = false)]
   public void KnockbackServerRpc(Vector3 direction)
   {
        KnockbackClientRpc(direction);
   }



    [ClientRpc]

   public  void KnockbackClientRpc(Vector3 direction)
   {
        rigidbody.AddForce(direction.normalized * 2500 * Time.deltaTime,ForceMode.Impulse);

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

            text.transform.LookAt(cam.transform.position,Vector3.up);

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

        // draw the name with a shadow (colored for buf)	

        


    
       

        

         if(IsLocalPlayer)
       {
            if(nameset == false)
            GUI.Label(new Rect(Screen.width * 0.5f , Screen.height * 0.1f , 100 , 30 ),"Select Fighter", titlestyle);

          


           if(nameset == false)
        nombrenNet.Value = GUI.TextField(new Rect( Screen.width * 0.43f ,Screen.height * 0.6f , 400, 100), nombrenNet.Value, 8,textfieldStyle);


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
                    cam.transform.SetParent(null);
                    cam.transform.position = new Vector3(0,52,62);
                    cam.transform.eulerAngles = new Vector3(45,180,0);
                    states = Estados.Playing;
                    nameset = true;
                    for(int i = 0; i < characters.Length; i++)
                    {
                        characters[i].SetActive(false);
                    }
                    anim = GetComponentInChildren<Animator>();
                    CleanNamesServerRpc(LocalID,NetworkManager.Singleton.LocalClientId);

                }
        }
      
       }
           
       }   
    

    
    Vector2 AxisInput => inputActions.Movement.Axis.ReadValue<Vector2>();

    Vector3 MovementAxis => new Vector3(AxisInput.x, 0f, AxisInput.y);

   

}
