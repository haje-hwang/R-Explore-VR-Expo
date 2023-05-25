using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToYourRoom : MonoBehaviour
{
    [SerializeField]
    private RecenterOrigin recenterOrigin;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("PlayerHead"))
        {
            recenterOrigin.Recenter();
        }
    }
}
