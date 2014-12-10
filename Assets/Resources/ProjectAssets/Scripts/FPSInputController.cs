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
    private float height;
    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        ch = GetComponent<CharacterController> ();
        height = ch.height;
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
        float h = height;
        float speed = walkSpeed;
        
        if (ch.isGrounded && Input.GetKey (KeyCode.LeftShift)) {
            speed = runSpeed;
        }
        if (Input.GetKey (KeyCode.C)) {
            h = .5f*height;
            speed = crchSpeed;
        }
        motor.movement.maxForwardSpeed = speed;
        float lastHeight = ch.height;
        ch.height = Mathf.Lerp (ch.height, h, 5 * Time.deltaTime);
    }
}