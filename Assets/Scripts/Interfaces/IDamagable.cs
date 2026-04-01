using UnityEditor;
using UnityEngine;

public interface IDamagable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    void TakeDamage(float damageAmount);
}
