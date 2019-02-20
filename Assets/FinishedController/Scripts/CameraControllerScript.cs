using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform mTransform;
    public Transform target;
    public Transform pivot;
    public bool leftPivot;
    float delta;

    float mouseX;
    float mouseY;
    float smoothX;
    float smoothY;
    float smoothXVelocity;
    float smoothYVelocity;
    float lookAngle;
    float tiltAngle; // угол наклона
    ControllerStates states; // состояния персонажа

    public CameraValues values;

    public void Init(Transform target, ControllerStates states)
    {
        mTransform = this.transform;
        this.states = states;
        this.target = target;
    }

    public void FixedTick(float deltaTime)
    {
        delta = deltaTime;
        
        if(target == null)
        {
            return;
        }

        HandlePositions();
        HandleRotation();

        float speed = values.moveSpeed;
        if(states.isAiming)
        {
            speed = values.aimSpeed;
        }

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, target.position, delta * speed);
        mTransform.position = targetPosition;
    }

    void HandlePositions()
    {
        float targetX = values.normalX; //Стандартные координаты центра вращения камеры по оси X
        float targetZ = values.normalZ; //Стандартные координаты центра вращения камеры по оси Z
        float targetY = values.normalY;
        if(states.isCrouching)
        {
            targetY = values.crouchY;
        }
        if (states.isAiming)
        {
            targetX = values.aimX;
            targetZ = values.aimZ;
        }
        if(leftPivot)
        {
            targetX = -targetX;
        }
        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = cameraTransform.localPosition;
        newCameraPosition.z = targetZ;

        float t = values.adaptSpeed * delta; // Аргумент линейной интерполяции
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        if(values.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, values.turnSmooth * delta);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, values.turnSmooth * delta);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * values.yRotateSpeed;
        Quaternion rotationAngle = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = rotationAngle;

        tiltAngle -= smoothY * values.xRotateSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, values.minAngle, values.maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }
}
