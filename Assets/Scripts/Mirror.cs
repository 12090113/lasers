using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 6) * 60);
    }
}
