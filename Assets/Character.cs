using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class Character : NetworkBehaviour
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
            if(AxisInput != Vector2.zero)
            {
                angle = Mathf.Atan2(AxisInput.x,AxisInput.y);
                angle = Mathf.Rad2Deg * angle;
                angle += cam.eulerAngles.y;
                targetRotation = Quaternion.Euler(0,angle,0);
                transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Turnspeed * Time.deltaTime);
            }
            rigidbody.AddForce(MovementAxis * speed * Time.deltaTime,ForceMode.Impulse);
        }
    }

    Vector2 AxisInput => inputActions.Movement.Axis.ReadValue<Vector2>();

    Vector3 MovementAxis => new Vector3(AxisInput.x, 0f, AxisInput.y);


}
