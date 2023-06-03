using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomeScript : MonoBehaviour
{
    public ParticleSystem explosion_particle;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        explosion_particle = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag.Equals("Beam")||other.transform.tag.Equals("Saber"))
        {
            meshRenderer.enabled = false;
            if(!explosion_particle.isPlaying)
            {
                explosion_particle.Play();
            }
            Invoke("DestroySelf", 1f);
        }
    }

    public void DestroySelf()
    {
        CancelInvoke();
        Destroy(this.gameObject);
    }
}
