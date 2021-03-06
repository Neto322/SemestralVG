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

    public Camera cam;


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

 

     public enum Estados {Idle,Playing,Death,Charging,Punch}

     public enum GameStates {Waiting,Ready,GameStart,Ended}

     public GameStates gameStates = GameStates.Waiting;

     public   Estados states = Estados.Idle;


    [SerializeField]
    TextMeshPro text;


    Collider[] enemiesHit;

    [SerializeField]
    LayerMask playerlayer;


    [SerializeField]
    float range;

    

    [SerializeField]
    Transform punchposition;

    
    public NetworkVariable<float> vida = new NetworkVariable<float>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, 0);
 

    bool position = false;

   
    public NetworkVariable<float> damage = new NetworkVariable<float>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, 0);


    bool powering = false;

    [SerializeField]
    float punchStrenght = 0;

    float TimeHold = 0;

    [SerializeField]
    float punchDamage;

    float rate;
    
    

    

    int clientList;

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

       
        

        inputActions.Movement.ActionStart.performed += x => Hold();

        inputActions.Movement.ActionEnd.performed += x => Unhold();

        
        

         
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
                case Estados.Charging:
                    inputActions.Movement.ActionStart.Enable();
                     if(AxisInput != Vector2.zero)
                        {
                            angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                            angle = Mathf.Rad2Deg * angle;
                            angle += cam.transform.eulerAngles.y;
                            targetRotation = Quaternion.Euler(0,angle,0);
                            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                        }
                    rigidbody.AddForce(transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime,ForceMode.Impulse);

                    if(TimeHold <= 1)
                    {
                        TimeHold += 0.8f * Time.deltaTime;
                    }
                    

                
                break;
                case Estados.Idle:
              

                
                break;

                    case Estados.Playing:
                        
                        if(position  == true)
                        {
                            transform.position = new Vector3(-10.15f,2.3f,-196);
                            position = false;

                        }

                        inputActions.Movement.ActionStart.Enable();
                        if(AxisInput != Vector2.zero)
                        {
                            angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                            angle = Mathf.Rad2Deg * angle;
                            angle += cam.transform.eulerAngles.y;
                            targetRotation = Quaternion.Euler(0,angle,0);
                            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                        }
                     
                           
                        //transform.position += transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime;
                        rigidbody.AddForce(transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime,ForceMode.Impulse);
                        //rigidbody.velocity = transform.forward * (speed * 10 * MovementAxis.magnitude) * Time.deltaTime;
                                  

                break;

                  case Estados.Punch:
                                inputActions.Movement.ActionStart.Disable();

                                rate -= Time.deltaTime;
                                 if(AxisInput != Vector2.zero)
                                {
                                    angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                                    angle = Mathf.Rad2Deg * angle;
                                    angle += cam.transform.eulerAngles.y;
                                    targetRotation = Quaternion.Euler(0,angle,0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                                }
                                
                                //transform.position += transform.forward * punchStrenght * Time.deltaTime;
                                //rigidbody.velocity = transform.forward * punchStrenght * 1000 * Time.deltaTime;
                                rigidbody.AddForce(transform.forward * punchStrenght * Time.deltaTime,ForceMode.Impulse);

                               


                                AttackPlayerServerRpc();
                        
                               if( rate <= 0)
                                {
                                    //rigidbody.velocity = Vector3.zero;
                                    states = Estados.Playing;
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
                case Estados.Charging:
                    

                
                break;

                case Estados.Punch:
                    if(anim)
                    {
                        anim.SetBool("Punch",true);
                    }

                
                break;
                    case Estados.Playing:

                       if(anim)
                            {
                                anim.SetBool("Punch",false);
                                if(AxisInput != Vector2.zero)
                                    {
                                        anim.SetBool("Move",true);

                                    } 

                                   
                                        
                                    BlendValue = MovementAxis.magnitude;

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
                    
                    
                    GameObject gameObj = obj.PlayerObject.gameObject;
                    Vector3 direction = gameObj.transform.position -  transform.position;

                    ulong ID = obj.PlayerObject.GetComponent<NetworkObject>().NetworkObjectId;

                    gameObj.GetComponent<Playerr>().KnockbackServerRpc(direction,ID);


                }
           }
    
        }
    }


   [ServerRpc(RequireOwnership = false)]
   public void KnockbackServerRpc(Vector3 direction,ulong ID)
   {
        KnockbackClientRpc(direction,ID);
   }



    [ClientRpc]

   public  void KnockbackClientRpc(Vector3 direction,ulong ID)
   {
       vida.Value += 1200 * Time.deltaTime;

       
        rigidbody.AddForce(direction.normalized * vida.Value * Time.deltaTime,ForceMode.Impulse);

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
        characterID.Value = LocalID;

          for(int i = 0; i < characters.Length; i++)
            {
                if(characters[i].GetComponent<Animator>() != anim)
                {
                    characters[i].SetActive(false);
                }
                else
                {
                }
            }

        
        characters[characterID.Value].SetActive(true);
        anim = GetComponentInChildren<Animator>();




    }


    [ServerRpc(RequireOwnership = false)]
    void PositionServerRpc()
    {
        PositionClientRpc();
    }

    [ClientRpc]
    void PositionClientRpc()
    {
            
    }
   
  
  
     void OnGUI()
    {


         if (GUI.Button(new Rect(Screen.width * 0.9f,Screen.height * 0.1f, 100, 50),"Start Match" ))
              {
                 
                    PositionServerRpc();

                  
                  


              }



                    

              
        


    
       

        

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
                    cam.transform.position = new Vector3(-28.94f,52f,-146f);
                    cam.transform.eulerAngles = new Vector3(45f,180f,0f);
                    states = Estados.Playing;
                    position = true;
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

    void Hold()
    {
        TimeHold = 0;
        states = Estados.Charging;
    }
    
    void Unhold()
    {
        if(states == Estados.Charging)
        {
            rigidbody.velocity = Vector3.zero;
            rate = TimeHold;
            damage.Value = punchDamage * rate;
            states = Estados.Punch;
        }

       
      
        

    }
    

   

}
