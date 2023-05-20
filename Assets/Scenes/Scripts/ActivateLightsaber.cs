using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLightsaber : MonoBehaviour
{
    private bool isSaberOn;
    public GameObject beam;

    private SliceScript sliceScript;
    private SaberAudio saberAudio;
    
    private void Awake()
    {
        beam = transform.Find("SABER Model").Find("beam").gameObject;
        isSaberOn = beam.activeSelf;
        sliceScript = GetComponent<SliceScript>();
        saberAudio = GetComponent<SaberAudio>();
    }
    private void Start()
    {
        sliceScript.Set_isSaberOn(isSaberOn);
    }

    public void ToggleSaber()
    {
        if(isSaberOn)   //if lightsaber is on, turn off.
        {
            beam.SetActive(false);
            isSaberOn = false;
            sliceScript.Set_isSaberOn(false);
            saberAudio.SaberOff();
        }
        else
        {
            beam.SetActive(true);
            isSaberOn = true;
            sliceScript.Set_isSaberOn(true);
            saberAudio.SaberOn();
        }
    }
}
