using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public GameObject weaponHolder;

    public float minimumYRot = -60f;
    public float maximumYRot = 60f;

    private Camera cam;
    private GameObject target;

    private float transitionMovSpeed;
    private float transitionRotSpeed;
    private bool transitioning;

    private float rotationY = 0f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    private void Update()
    {
        if (!transitioning)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 5f * Time.deltaTime; // TODO Change 5f to sensitivity

            rotationY += Input.GetAxis("Mouse Y") * 5f * Time.deltaTime; // TODO Change 5f to sensitivity
            rotationY = Mathf.Clamp(rotationY, minimumYRot, maximumYRot);
             
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }

    private void LateUpdate()
    {
        if (transitioning)
        {
            float movStep = transitionMovSpeed * Time.deltaTime;
            float rotStep = transitionRotSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movStep);

            Vector3 newDir = Vector3.RotateTowards(transform.forward, target.transform.forward, rotStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                transitioning = false;
                weaponHolder.SetActive(true);
            }
        }
    }

    public void SetCurrentPosition(Transform newPos)
    {
        transform.SetPositionAndRotation(newPos.position, newPos.rotation);
    }

    public void TransitionTo(GameObject newTarget, float transitionMovS, float transitionRotS)
    {
        if (newTarget == null)
            Debug.LogError("Trying to transition to a null target, skipping");
        else
        {
            weaponHolder.SetActive(false);

            target = newTarget;
            transitionMovSpeed = transitionMovS;
            transitionRotSpeed = transitionRotS;
            transitioning = true;
        }
    }
}
