using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
* https://www.youtube.com/watch?v=WU23Uj1oeh8
* Half Life Alyx Distance Grab - Unity Tutorial 
* by Valem Tutorials
*/
public class XRAlyxGrabInteractable : XRGrabInteractable
{
    public float velocityThreshold = 2;
    public float jumpAngleInDegree = 60;
    [SerializeField]
    private XRRayInteractor rayInteractor;
    private Vector3 previousPos;
    private Rigidbody interactableRigidbody;
    private bool canJump = true;
    protected override void Awake()
    {
        base.Awake();
        interactableRigidbody = GetComponent<Rigidbody>();
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRRayInteractor)
        {
            trackPosition = false;
            trackRotation = false;
            throwOnDetach = false;

            rayInteractor = (XRRayInteractor)args.interactorObject;
            previousPos = rayInteractor.transform.position;
            canJump = true;
        }
        else
        {
            trackPosition = true;
            trackRotation = true;
            throwOnDetach = true;
        }
        base.OnSelectEntered(args);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(isSelected && firstInteractorSelecting is XRRayInteractor)
        {
            Vector3 velocity = (rayInteractor.transform.position - previousPos) / Time.fixedDeltaTime;
            previousPos = rayInteractor.transform.position;

            if(velocity.magnitude > velocityThreshold)
            {
                Drop();
                interactableRigidbody.velocity = ComputeVelocity();
                canJump = false;
            }
        }

    }

    public Vector3 ComputeVelocity()
    {
        Vector3 diff = rayInteractor.transform.position - transform.position;
        Vector3 diff_XZ = new Vector3(diff.x, 0, diff.z);
        float diff_XZ_Length = diff_XZ.magnitude;
        float diff_Y_Length = diff.y;

        float angleInRadian = jumpAngleInDegree * Mathf.Deg2Rad;
        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diff_XZ_Length, 2)/ (2 * Mathf.Cos(angleInRadian)*Mathf.Cos(angleInRadian)*(diff_XZ.magnitude * Mathf.Tan(angleInRadian) - diff_Y_Length)));
        

        Vector3 jumpVelocityVector = diff_XZ.normalized * Mathf.Cos(angleInRadian) * jumpSpeed + Vector3.up * Mathf.Sin(angleInRadian) * jumpSpeed;
        return jumpVelocityVector;
    }
}
