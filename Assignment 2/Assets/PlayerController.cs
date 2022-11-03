using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 1.0f;
    public float moveSpeed = 2.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX = 0f;
    //create private internal references
    private InputActions inputActions;
    private InputAction movement;
    private InputAction vision;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActions = new InputActions(); //create new InputActions
        movement = inputActions.Player.Movement; //get reference to movement action
        vision = inputActions.Player.Vision;
    }

    //called when script enabled
    private void OnEnable()
    {
        movement.Enable();
        vision.Enable();
        inputActions.Player.Ability.performed += SwitchAbility;
        inputActions.Player.Ability.Enable();
    }

    //called when script disabled
    private void OnDisable()
    {
        movement.Disable();
        vision.Disable();
        inputActions.Player.Ability.performed -= SwitchAbility;
        inputActions.Player.Ability.Disable();
    }

    //switch of through wall ability
    private void SwitchAbility(InputAction.CallbackContext obj) {
        this.GetComponent<Collider>().enabled = !this.GetComponent<Collider>().enabled;
    }

    //called every physics update
    private void FixedUpdate()
    {
        // player rotation
        Vector2 visionV2 = vision.ReadValue<Vector2>();
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y + visionV2.x * turnSpeed, 0);

        // movement
        Vector2 v2 = movement.ReadValue<Vector2>(); //extract 2d input data
        float degree = this.transform.eulerAngles.y;
        rb.velocity = transform.forward * v2.y * moveSpeed; // move straight
        rb.velocity += transform.right * v2.x * moveSpeed; // move left or right

        // camera rotation (vertical)
        GameObject camera = this.gameObject.transform.GetChild(0).gameObject;
        rotX += visionV2.y * turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        camera.transform.eulerAngles = new Vector3(-rotX, this.transform.eulerAngles.y, 0);
    }
}
