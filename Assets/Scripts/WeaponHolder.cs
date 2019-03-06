using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // TODO Switch overlay from scope type
    public GameObject defaultOverlay;
    public GameObject weaponCamera;

    private Animator animator;
    private bool isScoped = false;
    private GameObject overlay = null;

    public void SetScopeOverlay(GameObject scopeOverlay)
    {
        if (scopeOverlay.tag == "ScopeOverlay")
        {
            overlay = scopeOverlay;
        }
        else
        {
            Debug.LogError("Tring to set a scope overlay from an object that doesn't have the 'ScopeOverlay' tag");
        }
    }

    public void ResetScopeOverlay() { overlay = null; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("IsScoped", isScoped);

            //            if (overlay != null)
            //            {
            if (isScoped)
                    StartCoroutine(OnScope());
                else
                    OnUnscope();
//            }
        }
    }

    private IEnumerator OnScope()
    {
        yield return new WaitForSeconds(.15f);

        weaponCamera.SetActive(false);
        defaultOverlay.SetActive(true);
        //        overlay.SetActive(true);
    }

    private void OnUnscope()
    {
        weaponCamera.SetActive(true);
        defaultOverlay.SetActive(false);
//        overlay.SetActive(false);
    }
}
