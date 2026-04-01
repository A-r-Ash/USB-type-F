using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovable, IPlayer, IDamagable
{
    [Header("Jump detection setting")]
    public Transform groundCheck;
    public float groundRadius = 2f;
    public LayerMask groundLayer;

    [Header("IDamagable interface")]

    public float CurrentHealth => currentHealth;
    public float MaxHealth => 100f;


    public float currentHealth;


    [Header("Sprites")]
    [SerializeField] private Sprite[] frames;

    [SerializeField] private float moveSpeed = 5f;


    public Rigidbody2D rb;
    public float jumpForce = 1f; 

    private Vector2 moveInput;
    private bool jumpButton;

    // Satisfying your IMovable interface from earlier!
    public float Speed => moveSpeed;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    private void FixedUpdate()
    {
        if (currentHealth >= 100) currentHealth = 100f; 
        Move(moveInput);
        Jump(jumpButton);        
        
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // This stores the WASD values (-1 to 1) into our variable
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpButton = context.performed;
    }

    public void Move(Vector2 direction)
    {
        // Calculate: Direction * Speed * Time
        Vector3 movement = new Vector3(direction.x, 0, 0) * Speed * Time.deltaTime;

        // Apply it to our position
        transform.position += movement;
    }

    public void Jump(bool jumped)
    {

        if (Grounded()&& jumped)
        {
            rb.AddForce(Vector2.up.normalized * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool Grounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    }

    private void OnDrawGizmos()
    {
        if(groundCheck == null ) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(groundCheck.position, groundRadius);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }
}