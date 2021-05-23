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

 
   PhotonRealtimeTransport transport;

    public Texture btnTexture;
   
    public NetworkVariable<string> nombrenNet = new NetworkVariable<string>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, "Default");

  
     public GUIStyle  textfieldStyle;

     public GUIStyle  titlestyle;

    public GUIStyle  nameStyle;



    public string nombre;

    bool nameset = false;

    public NetworkVariable<int> characterID = new NetworkVariable<int>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, 0);


    [SerializeField]
    GameObject[] characters;

     Color colorname;



     enum Estados {Idle,Playing,Death}

        Estados states = Estados.Idle;


        [SerializeField]
        TextMeshPro text;


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
       
       if(IsLocalPlayer)
       {
            //characters[characterID.Value].SetActive(true);

            //SetCharacterServerRpc();
           
            text.text = nombrenNet.Value;

            anim = GetComponentInChildren<Animator>();

            

            Debug.Log(anim);

            Debug.Log(anim);



            


             colorname = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));



            if(anim == null)
            {
                Debug.Log("Nada xd");
            }
       }
  
        
    }

    // Update is called once per frame
    void Update()
    {
       if(IsLocalPlayer)
       {
           



            text.text = nombrenNet.Value;

     

            switch(states)
            {
                case Estados.Idle:


                break;

                    case Estados.Playing:

                    if(AxisInput != Vector2.zero)
                                {
                                    Debug.Log("AAAAAAAAAAAAAAAAAA");
                                    angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                                    angle = Mathf.Rad2Deg * angle;
                                    angle += cam.eulerAngles.y;
                                    targetRotation = Quaternion.Euler(0,angle,0);
                                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
                                }
                                
                                rigidbody.AddForce(transform.forward * (speed * MovementAxis.magnitude) * Time.deltaTime,ForceMode.Impulse);


                                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
                                

                break;
            }



        }
            
            
       
       
    }




 
    private void LateUpdate() {
       if(IsLocalPlayer)
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
            
            


            

        
    }

  

  
     void OnGUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        // draw the name with a shadow (colored for buf)	

        

        nameStyle.alignment = TextAnchor.MiddleCenter;


        nameStyle.normal.textColor = colorname;

    
        GUI.color = Color.white;
        GUI.Label(new Rect(pos.x -50, Screen.height - pos.y - 35, 100, 30), nombrenNet.Value, nameStyle);


        GUI.Label(new Rect(pos.x -50, Screen.height - pos.y - 35, 100, 30), nombrenNet.Value, nameStyle);

        

         if(IsLocalPlayer)
       {
            if(nameset == false)
            GUI.Label(new Rect(Screen.width * 0.43f , Screen.height * 0.1f , 100 , 30 ),"Select Fighter", titlestyle);
           if(nameset == false)
        nombrenNet.Value = GUI.TextField(new Rect( Screen.width * 0.45f ,Screen.height * 0.7f , 80, 20), nombrenNet.Value, 8,textfieldStyle);


        if(nameset == false)
        {
               if (GUI.Button(new Rect(Screen.width * 0.3f,Screen.height * 0.5f, 90, 30), "<"))
                {  /* 
                    if(characterID.Value == 0)
                    {
                        characters[characterID.Value].SetActive(false);
                        characterID.Value = 7;
                        characters[characterID.Value].SetActive(true);
                    }
                    else
                    {
                        characters[characterID.Value].SetActive(false);
                        characterID.Value--;
                        characters[characterID.Value].SetActive(true);

                    }
                    
                    anim = GetComponentInChildren<Animator>();

*/

                }

                 if (GUI.Button(new Rect(Screen.width * 0.6f,Screen.height * 0.5f, 90, 30), ">"))
                {
                    /*
                    if(characterID.Value == 7)
                    {
                        characters[characterID.Value].SetActive(false);
                        characterID.Value = 0;
                        characters[characterID.Value].SetActive(true);
                    }
                    else
                    {
                        characters[characterID.Value].SetActive(false);
                        characterID.Value++;
                        characters[characterID.Value].SetActive(true);

                    }

                    anim = GetComponentInChildren<Animator>();


*/

                }

              if (GUI.Button(new Rect(Screen.width * 0.45f,Screen.height * 0.8f, 90, 30), "Start Game"))
                {
                    states = Estados.Playing;
                    nameset = true;
                    //SetCharacterServerRpc();

                }
        }
      
       }
           
       }   
    

    
    Vector2 AxisInput => inputActions.Movement.Axis.ReadValue<Vector2>();

    Vector3 MovementAxis => new Vector3(AxisInput.x, 0f, AxisInput.y);

   

}
