using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 positionOffset;
    public Vector3 rotation;

    public Transform pivot;

    void Update()
    {
        transform.position = pivot.position + positionOffset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
