using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraMoveSpeed = 120f;
    public GameObject cameraFollowObject;
    Vector3 followPos;
    public float claimAngle = 80f;
    public float inputSensitivity;
    public GameObject cameraMiddleWare;
    public GameObject cameraObject;
    public float cameraDistanceXToPlayer;
    public float cameraDistanceYToPlayer;
    public float cameraDistanceZToPlayer;

    public float mouseX;
    public float mouseY;

    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    public bool leftPivot = false;

    public float normalCameraMiddleWareX = 0;
    public float normalCameraMiddleWareZ = 0;
    public float normalCameraMiddleWareY = 0;

    public float aimCameraMiddleWareX = 0.2f;
    public float aimCameraMiddleWareZ = 0;
    public float aimCameraMiddleWareY = 0.1f;

    public float minNormalDistance = 0.5f;
    public float maxNormalDistance = 2f;

    public float minAimDistance = 0.1f;
    public float maxAimDistance = 0.6f;

    public float normalFollowObjPositionY = 1.5f;
    public float crouchFollowObjPositionY = 1f;

    public float smooth = 10f;
    public float crouchSmooth = 2f;
    public Vector3 dollyDir; // Вектор напраления от центра вращения камеры до самой камеры
    public float distance;
    ControllerStates states;

    public void Init(ControllerStates states)
    {
        this.states = states;
        dollyDir = cameraObject.transform.localPosition.normalized; // Локальные координаты камеры
        distance = cameraObject.transform.localPosition.magnitude;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Tick(float delta)
    {
        //float inputX = Input.GetAxis("RightStickHorizontal");
        //float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = mouseX; // + inputX
        finalInputZ = -mouseY; // + inputZ

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -claimAngle, claimAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    public void FixedTick(float delta)
    {
        HandlePositions(delta);
        if(states.isAiming)
        {
            ManageCameraCollision(minAimDistance, maxAimDistance);
        }
        else
        {
            ManageCameraCollision(minNormalDistance, maxNormalDistance);
        }
    }

    public void LateTick(float delta)
    {
        CameraUpdater();
    }

    /// <summary>
    /// Метод следования носителя камеры за её целью (cameraFollowObject),
    /// которая закреплена на игроке
    /// </summary>
    void CameraUpdater()
    {
        Transform target = cameraFollowObject.transform;

        float step = cameraMoveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void ManageCameraCollision(float minDistance, float maxDistance)
    {
        Vector3 desiredCameraPosition = cameraObject.transform.parent.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if (Physics.Linecast(cameraObject.transform.parent.position, desiredCameraPosition, out hit))
        {
            distance = Mathf.Clamp(hit.distance * 0.8f, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        cameraObject.transform.localPosition = Vector3.Lerp(cameraObject.transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }

    void HandlePositions(float delta)
    {
        Vector3 newCameraFollowObjectPosition = cameraFollowObject.transform.localPosition;
        if (states.isCrouching)
        {
            newCameraFollowObjectPosition.y = crouchFollowObjPositionY;
            cameraFollowObject.transform.localPosition = Vector3.Lerp(cameraFollowObject.transform.localPosition, newCameraFollowObjectPosition, delta * crouchSmooth);
        }
        else
        {
            newCameraFollowObjectPosition.y = normalFollowObjPositionY;
            cameraFollowObject.transform.localPosition = Vector3.Lerp(cameraFollowObject.transform.localPosition, newCameraFollowObjectPosition, delta * crouchSmooth);
        }

        float middleWareX = normalCameraMiddleWareX;
        float middleWareZ = normalCameraMiddleWareZ;
        float middleWareY = normalCameraMiddleWareY;

        if (states.isAiming)
        {
            middleWareX = aimCameraMiddleWareX;
            middleWareZ = aimCameraMiddleWareZ;
            middleWareY = aimCameraMiddleWareY;
        }

        Vector3 newMiddleWarePosition = cameraMiddleWare.transform.localPosition;
        newMiddleWarePosition.x = middleWareX;
        newMiddleWarePosition.y = middleWareY;
        newMiddleWarePosition.z = middleWareZ;
        cameraMiddleWare.transform.localPosition = Vector3.Lerp(cameraMiddleWare.transform.localPosition, newMiddleWarePosition,  delta * smooth);
    }
}
