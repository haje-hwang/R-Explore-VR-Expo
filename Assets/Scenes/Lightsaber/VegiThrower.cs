using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegiThrower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Vegis;
    // [SerializeField]
    private Transform Player;

    public float jumpAngleInDegree = 70;
    public int ThrowedVegiCount;
    [SerializeField]
    private MyScoreText myScoreText;
    private bool isCoroutineRunning;
    private Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = Camera.main.transform;

        ThrowedVegiCount = 0;
        set_isCoroutineRunning(false);
        // StartCoroutine(throwtest());
    }

    public void ToggleVegiThrower()
    {
        if(isCoroutineRunning)
        {
            StopCoroutine(throwtest());
            set_isCoroutineRunning(false);
        }
        else
        {
            StartCoroutine(throwtest());
        }
    }

    public void StopVegiThrower()
    {
        StopCoroutine(throwtest());
        set_isCoroutineRunning(false);
    }

    IEnumerator throwtest()
    {
        set_isCoroutineRunning(true);
        WaitForSeconds wait = new WaitForSeconds(2f);
        while(isCoroutineRunning)
        {
            CreateVegi(Random.Range(0,4));
            ThrowedVegiCount++;
            myScoreText.thrownVegi_count = ThrowedVegiCount;
            myScoreText.SetText();
            yield return wait;
        }
        set_isCoroutineRunning(false);
    }

    public void CreateVegi(int i)
    {
        StartPos = transform.position;
        StartPos.x += Random.Range(-3, 3);

        var vegi = Instantiate(Vegis[i], StartPos, Random.rotation);
        Rigidbody v_rb = vegi.GetComponent<Rigidbody>();
        v_rb.velocity = ComputeVelocity();
        v_rb.AddTorque(Random.rotation.eulerAngles.normalized, ForceMode.Impulse);
    }

    public Vector3 ComputeVelocity()
    {
        Vector3 diff = Player.position - StartPos;
        Vector3 diff_XZ = new Vector3(diff.x, 0, diff.z);
        float diff_XZ_Length = diff_XZ.magnitude;
        float diff_Y_Length = diff.y;

        jumpAngleInDegree = Random.Range(20, 70);
        float angleInRadian = jumpAngleInDegree * Mathf.Deg2Rad;
        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diff_XZ_Length, 2)/ (2 * Mathf.Cos(angleInRadian)*Mathf.Cos(angleInRadian)*(diff_XZ.magnitude * Mathf.Tan(angleInRadian) - diff_Y_Length)));
        

        Vector3 jumpVelocityVector = diff_XZ.normalized * Mathf.Cos(angleInRadian) * jumpSpeed + Vector3.up * Mathf.Sin(angleInRadian) * jumpSpeed;
        return jumpVelocityVector;
    }

    private void set_isCoroutineRunning(bool b)
    {
        isCoroutineRunning = b;
    }
}
