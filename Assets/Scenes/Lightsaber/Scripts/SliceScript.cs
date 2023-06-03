using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceScript : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public Material crossSectionMaterial;
    public float cutForce = 300;
    private SaberAudio saberAudio;
    private bool isSaberOn = true;
    [SerializeField]
    private MyScoreText myScoreText;
    private void Awake()
    {
        saberAudio = GetComponent<SaberAudio>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isSaberOn)
        {
            bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
            if(hasHit)
            {
                saberAudio.SaberSlice();
                GameObject target = hit.transform.gameObject;
                Slice(target);
                if(target.tag.Equals("Vegi"))
                {
                    myScoreText.CuttenVegi_count += 1;
                    myScoreText.SetText();
                }
                if(target.tag.Equals("Bomb"))
                {
                    //game end
                }
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
        }

        Destroy(target);
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        XRAlyxGrabInteractable XRgrab = slicedObject.AddComponent<XRAlyxGrabInteractable>();
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
        slicedObject.AddComponent<SlicedObjectScript>();
    }

    public void Set_isSaberOn(bool b)
    {
        isSaberOn = b;
    }
}
