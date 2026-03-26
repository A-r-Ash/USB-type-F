using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour, IMovable, IDamagable
{

    [Header("IDamagable interface")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public float Damage => damage;


    [Header("IMovable interface")]
    public float moveSpeed;
    public float Speed => moveSpeed;


    public Slider healthSlider;


    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.maxValue = maxHealth;
    }

    public void FixedUpdate()
    {
        UpdateUI();
        
    }


    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
    }

    public void UpdateUI()
    {
        healthSlider.value = currentHealth;
    }

    public void Move(Vector2 direction)
    {

    }

}
