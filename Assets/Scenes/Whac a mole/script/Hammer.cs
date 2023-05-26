using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스 클릭으로 두더지 잡기

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float minY;
    [SerializeField]
    private ObjectDetector objectDetector;
    private Movement3D movement3D;
    [SerializeField]
    private GameObject moleHitEffectPrefab;
    [SerializeField]
    private GameController gameController;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private void Awake()
    {
        movement3D= GetComponent<Movement3D>();

        objectDetector.raycastEvent.AddListener(OnHit);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHit(Transform target)
    {
        if (target.CompareTag("mole"))
        {
            MoleFSM mole = target.GetComponent<MoleFSM>();

            if (mole.MoleState == MoleState.UnderGround) return;

            transform.position=new Vector3(target.position.x,minY,target.position.z);

            mole.ChangeState(MoleState.UnderGround);

            ShakeCamera.Instance.OnShakeCamera(0.1f, 0.1f);

            GameObject clone = Instantiate(moleHitEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem.MainModule main = clone.GetComponent<ParticleSystem>().main;
            main.startColor = mole.GetComponent<MeshRenderer>().material.color;

            //gameController.Score += 100;
            MoleHitProcess(mole);

            StartCoroutine("MoveUp");
        }
        
    }

    private IEnumerator MoveUp()
    {
        movement3D.MoveTo(Vector3.up);

        while (true)
        {
            if(transform.position.y>=maxY)
            {
                movement3D.MoveTo(Vector3.zero);

                break;
            }

            yield return null;
        }
    }

    private void MoleHitProcess(MoleFSM mole)
    {
        if(mole.MoleType == MoleType.Normal)
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
            gameController.Combo=0;
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
