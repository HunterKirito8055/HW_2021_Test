using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPosition = new Vector3(0,4f,-9f);
    public float speed = 5f;
    private void FixedUpdate()
    {
        Vector3 toPos = target.position + offsetPosition;
        transform.position = Vector3.Lerp(transform.position, toPos, Time.deltaTime * speed);
    }
}
