using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerController : MonoBehaviour
{
    public CamerasController camController;

    public float inputDelay = 0.1f;
    public float forwardVel = 12.0f;
    public float rotateVel = 100.0f;

    private Quaternion targetRotation;
    private Rigidbody rBody;
    private Animator animator;
    private float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation;  }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The character needs a rigidbody");

        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();
        else
            Debug.LogError("The character needs an Animator");
        forwardInput = turnInput = 0f;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            rBody.velocity = transform.forward * forwardInput * forwardVel;
            
        }
        else
            rBody.velocity = Vector3.zero;
    }

    void Turn()
    {
        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    void Update()
    {
        GetInput();
        Turn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShootingSpot" && Input.GetKeyDown(KeyCode.X))
        {
            ShootingSpot ss = other.GetComponent<ShootingSpot>();
            camController.ToShootingMode(other.gameObject, this);
            Debug.Log("Shooting spot hit");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ShootingSpot" && Input.GetKeyDown(KeyCode.X))
        {
            camController.ToShootingMode(other.gameObject, this);
            Debug.Log("Shooting spot hit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ShootingSpot")
        {
            Debug.Log("Shooting spot leaved");
        }
    }
}
