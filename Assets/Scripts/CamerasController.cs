using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public GameObject TPCamera;
    public GameObject FPSCamera;

    public float transitionMovSpeed = 0.1f;
    public float transitionRotSpeed = 0.1f;

    private void Start()
    {
        FPSCamera.SetActive(false);
    }

    public void ToShootingMode(GameObject spot, ThirdPersonPlayerController ply)
    {
        FPSCamera FPSCam = FPSCamera.GetComponent<FPSCamera>();

        // Disabling the third person player
        ply.gameObject.SetActive(false);

        // Switching camera mode
        TPCamera.SetActive(false);
        FPSCamera.SetActive(true);

        // Starting the transition between cameras
        FPSCam.SetCurrentPosition(TPCamera.transform);
        FPSCam.TransitionTo(spot, transitionMovSpeed, transitionRotSpeed);

        Debug.Log("ToShootingMode() called");
    }

    public void ToThirdPersonMode(ThirdPersonPlayerController ply)
    {
        ply.gameObject.SetActive(true);
        TPCamera.SetActive(true);
        FPSCamera.SetActive(false);
    }
}
