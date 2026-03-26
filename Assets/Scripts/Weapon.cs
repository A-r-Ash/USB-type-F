using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float weaponDamage = 10f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable target = collision.GetComponent<IDamagable>();

        if(target != null)
        {
            target.TakeDamage(weaponDamage);
            Debug.Log("Hit " + collision.name + " for " + weaponDamage + " damage!");
        }

        else
        {
            // This runs if we hit a wall or floor (things that can't be "damaged")
            Debug.Log("Hit something that isn't damagable: " + collision.name);
        }
    }
}
