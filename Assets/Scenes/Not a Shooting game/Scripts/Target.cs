using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject Broken;
    public MeshRenderer model;
    private Transform point1;
    private Transform point2;
    public ScoreManager scoreManager;
    public int points;
    public bool despawn = true;
    private float pathTime;
    private float currentPathTime;
    private bool moving;
    private void Start()
    {
        moving = true;
        currentPathTime = 0;
    }

    private void Update()
    {
        if (moving)
        {
            currentPathTime += Time.deltaTime;
            transform.position = Vector3.Lerp(point1.position, point2.position, currentPathTime / pathTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Bullet"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<AudioSource>().Play();
            model.enabled = false;
            Broken.SetActive(true);
            Rigidbody[] pieces = Broken.GetComponentsInChildren<Rigidbody>();
            float force = 1000;
            foreach(Rigidbody rb in pieces)
            {
                rb.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)));
            }
            scoreManager.AddScore();
            Destroy(gameObject, 3f);
            if (FindObjectOfType<TargetManager>().isWave())
            {
                ScoreKeeper.current.ChangeScore(points);
            }
            moving = false;
        }
    }

    internal void SetPath(Transform startPos, Transform endpos)
    {
        point1 = startPos;
        point2 = endpos;
    }

    public void SetPathTime(float t)
    {
        pathTime = t;
    }
}
