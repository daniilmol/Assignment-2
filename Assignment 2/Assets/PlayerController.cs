using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //create private internal references
    private InputActions inputActions;
    private InputAction movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActions = new InputActions(); //create new InputActions
        movement = inputActions.Player.Movement; //get reference to movement action
    }

    //called when script enabled
    private void OnEnable()
    {
        movement.Enable();
    }

    //called when script disabled
    private void OnDisable()
    {
        movement.Disable();
    }

    //called every physics update
    private void FixedUpdate()
    {
        Vector2 v2 = movement.ReadValue<Vector2>(); //extract 2d input data
        Vector3 v3 = new Vector3(v2.x, 0, v2.y); //convert to 3d space
        rb.AddForce(v3, ForceMode.VelocityChange);
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 leftStick = gamepad.leftStick.ReadValue();
            Vector3 lStickV3 = new Vector3(leftStick.x, 0, leftStick.y); //convert to 3d space
            rb.AddForce(lStickV3, ForceMode.VelocityChange);
        }
    }
}
