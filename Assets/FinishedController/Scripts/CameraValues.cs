using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Camera Values")]
public class CameraValues : ScriptableObject
{
    public float turnSmooth = 0.1f;
    public float moveSpeed = 9;
    public float aimSpeed = 25;
    public float yRotateSpeed = 8;
    public float xRotateSpeed = 8;
    public float minAngle = -35;
    public float maxAngle = 35;
    public float normalZ;
    public float normalX;
    public float aimZ = -.5f;
    public float aimX = 0;
    public float normalY;
    public float crouchY;
    public float adaptSpeed = 9f;
}
