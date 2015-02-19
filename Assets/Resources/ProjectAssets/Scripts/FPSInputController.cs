using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/FPS Input Controller")]

public class FPSInputController : MonoBehaviour
{
    private CharacterMotor motor;
    float walkSpeed = 7f;
    float crchSpeed = 3f;
    float runSpeed = 20f;
   

    private CharacterController ch;
	private Transform tr;
    private float dist;
	void Start()
	{
		motor =  GetComponent<CharacterMotor>();
		tr = transform;
		CharacterController ch = GetComponent<CharacterController>();
		dist = ch.height/2; // calculate distance to ground
	}
    // Use this for initialization
    void Awake()
    {
       // motor = GetComponent<CharacterMotor>();
        //ch = GetComponent<CharacterController> ();
       
    }

    // Update is called once per frame
    void Update()
    {

        // Get the input vector from kayboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Crouch ();
        if (directionVector != Vector3.zero)
        {
            // Get the length of the directon vector and then normalize it
            // Dividing by the length is cheaper than normalizing when we already have the length anyway
            float directionLength = directionVector.magnitude;
            directionVector = directionVector / directionLength;

            // Make sure the length is no bigger than 1
            directionLength = Mathf.Min(1.0f, directionLength);

            // Make the input vector more sensitive towards the extremes and less sensitive in the middle
            // This makes it easier to control slow speeds when using analog sticks
            directionLength = directionLength * directionLength;

            // Multiply the normalized direction vector by the modified length
            directionVector = directionVector * directionLength;
        }

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = Input.GetButton("Jump");
    }
        void Crouch()
    {

		float vScale = 1.0f;
		float speed = walkSpeed;
		
		if ((Input.GetKey("left shift") || Input.GetKey("right shift")) && motor.grounded)
		{
			speed = runSpeed;            
		}
		
		if (Input.GetKey("c"))
		{ // press C to crouch
			vScale = 0.5f;
			speed = crchSpeed; // slow down when crouching
		}
		
		motor.movement.maxForwardSpeed = speed; // set max speed
		float ultScale = tr.localScale.y; // crouch/stand up smoothly 
		
		Vector3 tmpScale = tr.localScale;
		Vector3 tmpPosition = tr.position;
		
		tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
		tr.localScale = tmpScale;
		
		tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position        
		tr.position = tmpPosition;
    }
}