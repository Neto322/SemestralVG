using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
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

    [SerializeField]
    Animator anim;

    [SerializeField]
   float maxSpeed;

   float BlendValue;

   [SerializeField]
   TextMeshPro textMeshPro;

   PhotonRealtimeTransport transport;

    public Texture btnTexture;
   
    public NetworkVariable<string> nombrenNet = new NetworkVariable<string>(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly}, "Default");

  
    public string stringToEdit = "Hello World";

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
       
       if(IsOwner)
       {

      
       

       }
  
        
    }

    // Update is called once per frame
    void Update()
    {
       if(IsOwner)
       {
            if(AxisInput != Vector2.zero)
            {
                
                angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                angle = Mathf.Rad2Deg * angle;
                angle += cam.eulerAngles.y;
                targetRotation = Quaternion.Euler(0,angle,0);
                transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
            }
            
            rigidbody.AddForce(MovementAxis * speed * Time.deltaTime,ForceMode.Impulse);


            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
       }
            
            
       
       
    }

 
    private void LateUpdate() {
       if(IsOwner)
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

    [ServerRpc]
    void SetNameServerRpc()
    {
        nombrenNet.Value = stringToEdit;
    }

     void OnGUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        // draw the name with a shadow (colored for buf)	
        GUI.color = Color.black;
        GUI.Label(new Rect(pos.x - 20, Screen.height - pos.y - 30, 100, 30), nombrenNet.Value);


        GUI.Label(new Rect(pos.x - 21, Screen.height - pos.y - 31, 100, 30), nombrenNet.Value);


         if(IsOwner)
       {
        stringToEdit = GUI.TextField(new Rect(pos.x - 45, Screen.height - pos.y - 45, 400, 400), stringToEdit, 25);

    

        if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
        {
            SetNameServerRpc();
        }
           
       }   
    }

    
    Vector2 AxisInput => inputActions.Movement.Axis.ReadValue<Vector2>();

    Vector3 MovementAxis => new Vector3(AxisInput.x, 0f, AxisInput.y);

   

}
