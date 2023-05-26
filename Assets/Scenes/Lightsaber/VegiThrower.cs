using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegiThrower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Vegis;
    // [SerializeField]
    private Transform camera;

    public float jumpAngleInDegree = 70;
    public int ThrowedVegiCount;
    private bool isCoroutineRunning;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;

        ThrowedVegiCount = 0;
        isCoroutineRunning = false;
        // StartCoroutine(throwtest());
    }

    public void ToggleVegiThrower()
    {
        if(isCoroutineRunning)
        {
            StopCoroutine(throwtest());
            isCoroutineRunning = false;
        }
        else
        {
            StartCoroutine(throwtest());
        }
    }

    IEnumerator throwtest()
    {
        isCoroutineRunning = true;
        WaitForSeconds wait = new WaitForSeconds(2f);
        while(true)
        {
            CreateVegi(Random.Range(0,4));
            ThrowedVegiCount++;
            yield return wait;
        }
        isCoroutineRunning = false;
    }

    public void CreateVegi(int i)
    {
        var vegi = Instantiate(Vegis[i], transform.position, Random.rotation);
        Rigidbody v_rb = vegi.GetComponent<Rigidbody>();
        v_rb.velocity = ComputeVelocity();
        v_rb.AddTorque(Random.rotation.eulerAngles.normalized, ForceMode.Impulse);
    }

    public Vector3 ComputeVelocity()
    {
        Vector3 diff = camera.position - transform.position;
        Vector3 diff_XZ = new Vector3(diff.x, 0, diff.z);
        float diff_XZ_Length = diff_XZ.magnitude;
        float diff_Y_Length = diff.y;

        float angleInRadian = jumpAngleInDegree * Mathf.Deg2Rad;
        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diff_XZ_Length, 2)/ (2 * Mathf.Cos(angleInRadian)*Mathf.Cos(angleInRadian)*(diff_XZ.magnitude * Mathf.Tan(angleInRadian) - diff_Y_Length)));
        

        Vector3 jumpVelocityVector = diff_XZ.normalized * Mathf.Cos(angleInRadian) * jumpSpeed + Vector3.up * Mathf.Sin(angleInRadian) * jumpSpeed;
        return jumpVelocityVector;
    }
}
