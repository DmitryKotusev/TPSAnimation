using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    int clipSize;

    public int ammo;
    public int shotsFiredInClip;
    bool isReloading;

    public int roundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if(isReloading)
        {
            return;
        }

        isReloading = true;

        GameManager.Instance.Timer.Add(ExecuteReload, reloadTime);
    }

    private void ExecuteReload()
    {
        isReloading = false;
        ammo -= shotsFiredInClip;
        shotsFiredInClip = 0;

        if(ammo < 0)
        {
            shotsFiredInClip += -ammo;
            ammo = 0;
        }
    }

    public void TakeFormClip(int amount)
    {
        shotsFiredInClip += amount;
    }
}
