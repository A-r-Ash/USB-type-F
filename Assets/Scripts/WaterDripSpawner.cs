using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Water drip hazard. Hangs kinematic at ceiling, drops dynamic after delay,
/// plays splash on contact, damages player, then resets.
/// </summary>
public class WaterDripSpawner : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float dropDelay = 4f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D waterDropRigidbody;
    [SerializeField] private BoxCollider2D waterDropCollider;
    [SerializeField] private Animator animator;

    [Header("Damage")]
    [SerializeField] private float damageAmount = 50f;

    private Vector3 startPosition;
    private float timer;
    private bool isDropping;
    private bool hasSplashed;

    private void Start()
    {
        InitializeDroplet();
    }

    private void Update()
    {
        if (isDropping || hasSplashed) return;

        timer += Time.deltaTime;

        if (timer >= dropDelay)
        {
            DropWater();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDropping || hasSplashed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(damageAmount);
        }

        StartCoroutine(SplashAndReset());
    }

    private IEnumerator SplashAndReset()
    {
        hasSplashed = true;
        waterDropRigidbody.bodyType = RigidbodyType2D.Kinematic;
        waterDropRigidbody.linearVelocity = Vector2.zero;
        waterDropCollider.enabled = false; // disable collider to prevent multiple collisions during splash

        animator.SetTrigger("Splash");

        yield return null; // wait one frame for Animator to transition into Splash state
        float splashLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(splashLength);

        ResetDroplet();
    }

    private void DropWater()
    {
        isDropping = true;
        waterDropRigidbody.bodyType = RigidbodyType2D.Dynamic;
        animator.SetTrigger("Drop");
    }

    private void InitializeDroplet()
    {
        startPosition = transform.position;
        waterDropRigidbody.freezeRotation = true;
        waterDropRigidbody.bodyType = RigidbodyType2D.Kinematic;
        timer = 0f;
        isDropping = false;
        hasSplashed = false;
    }

    private void ResetDroplet()
    {
        transform.position = startPosition;
        waterDropRigidbody.bodyType = RigidbodyType2D.Kinematic;
        waterDropRigidbody.linearVelocity = Vector2.zero;
        waterDropCollider.enabled = true;

        timer = 0f;
        isDropping = false;
        hasSplashed = false;

        //animator.Play("Idle"); // force Animator back to Idle state
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (dropDelay <= 0f)
        {
            dropDelay = 1f;
            Debug.LogWarning("Drop delay must be greater than 0.");
        }
    }
#endif
}