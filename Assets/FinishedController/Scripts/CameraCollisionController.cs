using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionController : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smooth = 10f;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;


    void Awake()
    {
        dollyDir = transform.localPosition.normalized; // Локальные координаты камеры
        distance = transform.localPosition.magnitude;
    }
    void FixedUpdate()
    {
        Vector3 desiredCameraPosition = transform.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, desiredCameraPosition, out hit))
        {
            distance = Mathf.Clamp(hit.distance * 0.85f, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
} 
