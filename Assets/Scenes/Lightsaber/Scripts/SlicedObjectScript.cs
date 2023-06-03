using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedObjectScript : MonoBehaviour
{   
    private void Start()
    {
        Invoke("SetLayer_to_Sliceable", 0.5f);
        Invoke("DestroySelf", 20f);
    }
    public void SetLayer_to_Sliceable()
    {
        gameObject.layer = 6; //Sliceable Layer
    }
    private void DestroySelf()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
