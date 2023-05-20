using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;
    public FadeCanvas fadeCanvas;

    public void Recenter()
    {
        fadeCanvas.QuickFadeIn();
        
        Vector3 offset = head.position - origin.position;
        offset.y = 0;
        origin.position = target.position - offset;

        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0;

        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

        origin.RotateAround(head.position, Vector3.up, angle);

        fadeCanvas.StartFadeOut();
    }
}
