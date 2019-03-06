using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 4, -8);
    public float xTilt = 10;

    Vector3 destination = Vector3.zero;
    ThirdPersonPlayerController charController;
    float rotateVel = 0f;

    private void Start()
    {
        SetCameraTarget(target);
    }

    public void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<ThirdPersonPlayerController>())
            {
                charController = target.GetComponent<ThirdPersonPlayerController>();
            }
            else
                Debug.LogError("The camera's target nees a character controller");
        }
        else
            Debug.LogError("Camera needs a target");
    }

    private void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    void MoveToTarget()
    {
        destination = charController.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        transform.rotation = Quaternion.Euler(transform. eulerAngles.x, eulerYAngle, 0f);
    }
}
