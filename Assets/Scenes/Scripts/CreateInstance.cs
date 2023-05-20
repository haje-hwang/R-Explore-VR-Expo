using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInstance : MonoBehaviour
{
    public GameObject prefab;

    public void Create()
    {
        Instantiate(prefab, new Vector3(0, 3, 1.5f), Quaternion.identity);
    }
}
