using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageble : MonoBehaviour
{
    public float MaxHP { get; protected set; }
    public float CurrentHP { get; protected set; }
    public int Team { get; protected set; }

    public event Action<Damageble> OnDied;
    public event Action<float> OnCurrentHPChanged;

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    public void Damage(float damage)
    {
        CurrentHP -= damage;
        OnCurrentHPChanged?.Invoke(CurrentHP);

        if (CurrentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDied?.Invoke(this);
        Destroy(gameObject);
    }
}
