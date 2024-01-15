using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageble
{
    float MaxHP { get; }
    float CurrentHP { get; }

    void Damage(float damage);
    void Die();
}
