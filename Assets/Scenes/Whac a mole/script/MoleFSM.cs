using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum MoleState { UnderGround = 0, OnGround, MoveUp, MoveDown}
public enum MoleType {  Normal=0,Red,Blue,Green}
public class MoleFSM : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private float waitTimeOnGround;
    [SerializeField]
    private float limitMinY;
    [SerializeField]
    private float limitMaxY;
    
    private Movement3D movement3D;
    private MeshRenderer meshRenderer;

    private MoleType moleType;
    private Color defaultColor;

    public MoleState MoleState { private set; get; }

    public MoleType MoleType
    {
        set
        {
            moleType= value;

            switch(moleType)
            {
                case MoleType.Normal:
                    meshRenderer.material.color = defaultColor;
                    break;
                case MoleType.Red:
                    meshRenderer.material.color = Color.red;
                    break;
                case MoleType.Blue:
                    meshRenderer.material.color = Color.blue;
                    break;
                case MoleType.Green:
                    meshRenderer.material.color = Color.green;
                    break;
            }
        }
        get => moleType;
    }

    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();

        defaultColor = meshRenderer.material.color;
        ChangeState(MoleState.UnderGround);
    }

    public void ChangeState(MoleState newState)
    {
        StopCoroutine(MoleState.ToString());

        MoleState = newState;

        StartCoroutine(MoleState.ToString());
    }

    private IEnumerator UnderGround()
    {
        movement3D.MoveTo(Vector3.zero);
        //Debug.Log("UnderGround ");
        transform.position = new Vector3(transform.position.x, limitMinY, transform.position.z);

        yield return null;

    }

    private IEnumerator OnGround()
    {
        movement3D.MoveTo(Vector3.zero);
        //Debug.Log("OnGround ");
        transform.position = new Vector3(transform.position.x, limitMaxY, transform.position.z);

        yield return new WaitForSeconds(waitTimeOnGround);

        ChangeState(MoleState.MoveDown);
    }

    private IEnumerator MoveUp()
    {
        movement3D.MoveTo(Vector3.up);
        audioSource.Play();
        //Debug.Log("MoveUp ");
        while (true)
        {
            if (transform.position.y >= limitMaxY)
            {
                ChangeState(MoleState.OnGround);
            }

            yield return null;
        }
    }

    private IEnumerator MoveDown()
    {
        movement3D.MoveTo(Vector3.down);
        //Debug.Log("MoveDown ");
        while (true)
        {
            if(transform.position.y <= limitMinY) 
            {

                break;

            }

            yield return null;
        }
        if(moleType == MoleType.Normal)
        {
            gameController.Combo = 0;
        }

        ChangeState(MoleState.UnderGround);
    }
}
