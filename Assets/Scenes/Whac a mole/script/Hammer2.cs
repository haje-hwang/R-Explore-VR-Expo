using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//VR기기로 두더지 잡기

public class Hammer2 : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private GameObject moleHitEffectPrefab;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mole"))
        {
            Debug.Log("아파");

            MoleFSM mole = other.GetComponent<MoleFSM>();

            if (mole.MoleState == MoleState.UnderGround) return;

            mole.ChangeState(MoleState.UnderGround);

            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
            main.startColor = mole.GetComponent<MeshRenderer>().material.color;

            //gameController.Score += 100;
            MoleHitProcess(mole);

        }

    }

    private void MoleHitProcess(MoleFSM mole)
    {
        if (mole.MoleType == MoleType.Normal)
        {
            gameController.NormalMoleHitCount++;
            gameController.Combo++;

            float scoreMultiple = 1 + gameController.Combo / 10;
            int getScore = (int)(scoreMultiple * 100);

            gameController.Score += getScore;
        }
        else if (mole.MoleType == MoleType.Red)
        {
            gameController.RedMoleHitCount++;
            gameController.Combo = 0;
            gameController.Score -= 500;
        }
        else if (mole.MoleType == MoleType.Blue)
        {
            gameController.BlueMoleHitCount++;
            gameController.Combo++;
            gameController.CurrentTime += 10;
        }
        else if (mole.MoleType == MoleType.Green)
        {
            gameController.GreenMoleHitCount++;
            gameController.Combo++;

            float scoreMultiple = 1 + gameController.Combo / 10;
            int getScore = (int)(scoreMultiple * 300);

            gameController.Score += getScore;
        }

        PlaySound((int)mole.MoleType);
    }

    private void PlaySound(int index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
