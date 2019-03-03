using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    float damping;
    Transform cameraLookTarget;
    public Player localPlayer;
    void Awake()
    {
        GameManager.Instance.OnLocalePlayerJoined += HandleOnLocalePlayerJoined;
    }

    void HandleOnLocalePlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("CameraLookTarget");

        if(cameraLookTarget == null)
        {
            cameraLookTarget = localPlayer.transform;
        }
    }

    void Update()
    {
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
            localPlayer.transform.up * cameraOffset.y + localPlayer.transform.right * cameraOffset.x;

        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * damping);
    }
}
