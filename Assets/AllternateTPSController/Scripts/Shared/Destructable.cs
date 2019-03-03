using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    float hitPoints;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public float HitPointsRemaining
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }

    public bool IsALive
    {
        get
        {
            return HitPointsRemaining > 0;
        }
    }

    public virtual void Die()
    {
        if(!IsALive)
        {
            return;
        }
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        damageTaken += amount;

        if(OnDamageReceived != null)
        {
            OnDamageReceived();
        }

        if(HitPointsRemaining <= 0)
        {
            Die();
        }
    }

    public void Reset()
    {
        damageTaken = 0;
    }
}
