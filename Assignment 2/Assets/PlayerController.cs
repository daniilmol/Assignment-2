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
    public GameObject ball;
    private float rotX = 0f;
    //create private internal references
    private InputActions inputActions;
    private InputAction movement;
    private InputAction vision;
    private InputAction shoot;
    private Vector3 startingPoint;
    
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActions = new InputActions(); //create new InputActions
        movement = inputActions.Player.Movement; //get reference to movement action
        vision = inputActions.Player.Vision;
        shoot = inputActions.Player.Shoot;
        startingPoint = transform.position;
    }

    //called when script enabled
    private void OnEnable()
    {
        movement.Enable();
        vision.Enable();
        shoot.Enable();
        inputActions.Player.Ability.performed += SwitchAbility;
        inputActions.Player.Ability.Enable();
        inputActions.Player.Reset.performed += ResetEntities;
        inputActions.Player.Reset.Enable();
    }

    //called when script disabled
    private void OnDisable()
    {
        movement.Disable();
        vision.Disable();
        shoot.Disable();
        inputActions.Player.Ability.performed -= SwitchAbility;
        inputActions.Player.Ability.Disable();
        inputActions.Player.Reset.performed -= ResetEntities;
        inputActions.Player.Reset.Disable();
        inputActions.Player.Shoot.performed -= Shoot;
        inputActions.Player.Shoot.Disable(); 
    }

    private void Shoot(InputAction.CallbackContext obj){

    }

    //switch of through wall ability
    private void SwitchAbility(InputAction.CallbackContext obj) {
        this.GetComponent<Collider>().isTrigger = !this.GetComponent<Collider>().isTrigger;
    }

    private void ResetEntities(InputAction.CallbackContext obj){
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Reset();
        enemy.GetComponent<EnemyBehaviour>().Reset();
    }

    private void Reset(){
        transform.position = startingPoint;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
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
