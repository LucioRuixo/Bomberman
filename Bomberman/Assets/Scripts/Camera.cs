using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 offset;

    public Transform pivot;

    void Start()
    {
        transform.position = pivot.position + offset;
    }

    void Update()
    {
        transform.position = pivot.position + offset;
    }
}
