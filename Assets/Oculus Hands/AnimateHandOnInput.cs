using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
* https://www.youtube.com/watch?v=8PCNNro7Rt0&t=638s
* How to Make a VR Game in Unity 2022 - PART 2 - INPUT and HAND PRESENCE
* Download the oculus hand package : 
* https://drive.google.com/file/d/10b39IekUdpBHlcTslZ-BlNRyH5uqPUe1/view
*/
public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimation;
    public Animator handAnimator;

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger",triggerValue);

        float gripValue = gripAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip",gripValue);
    }
}
