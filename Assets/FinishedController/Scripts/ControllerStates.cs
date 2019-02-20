using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/States")]
public class ControllerStates : ScriptableObject
{
    public bool onGround;
    public bool isAiming;
    public bool isCrouching;
    public bool isSlowWalking;
    public bool isRunning;
    public bool isInteracting;
}
