using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform target;
    public void RespawnObject()
    {
        target.position = transform.position;
    }
}
