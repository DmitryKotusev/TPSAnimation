using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    Shooter assaultRiffle;

    private void Update()
    {
        if(GameManager.Instance.InputController.fire1)
        {
            assaultRiffle.Fire();
        }
    }
}
