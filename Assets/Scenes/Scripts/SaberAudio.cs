using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberAudio : MonoBehaviour
{
    public AudioClip[] SaberAudioes;
    private AudioSource audioSource;
    private float initVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initVolume = audioSource.volume;
        PlayIdle();
    }
    private void Play(int i)
    {
        audioSource.volume = initVolume;
        StopCoroutine(FadeOut());
        audioSource.PlayOneShot(SaberAudioes[i]);
        StartCoroutine(IdleAfterPlay());
    }
    
    public void SaberOn()
    {
        Play(0);
    }

    
    public void SaberOff()
    {
        audioSource.PlayOneShot(SaberAudioes[1]);
        StartCoroutine(FadeOut());
    }
    public void SaberSlice()
    {
        Play(2);
    }
    public void PlayIdle()
    {
        audioSource.clip = SaberAudioes[3];
        audioSource.Play();
        audioSource.loop = true;
    }

    public void ChangeVolume(float vol)
    {
        initVolume = vol;
        audioSource.volume = vol;
    }

    IEnumerator IdleAfterPlay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayIdle();
    }
    IEnumerator FadeOut()
    {
        initVolume = audioSource.volume;
        while(audioSource.volume > 0.01f)
        {
            audioSource.volume *= 0.9f;
            yield return new WaitForSeconds(0.05f);
        }
        audioSource.Stop();
        audioSource.volume = initVolume;
    }
}
