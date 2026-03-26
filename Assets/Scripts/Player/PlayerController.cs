using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovable, IPlayer
{
    [Header("Jump detection setting")]
    public Transform groundCheck;
    public float groundRadius = 2f;
    public LayerMask groundLayer;

    [Header("IDamagable interface")]




    [SerializeField] private float moveSpeed = 5f;


    public Rigidbody2D rb;
    public float jumpForce = 1f; 

    private Vector2 moveInput;
    private bool jumpButton;

    // Satisfying your IMovable interface from earlier!
    public float Speed => moveSpeed;

    public void OnMove(InputAction.CallbackContext context)
    {
        // This stores the WASD values (-1 to 1) into our variable
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpButton = context.performed;
    }

    private void FixedUpdate()
    {
        
        Move(moveInput);
        Jump(jumpButton);        
        
        
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

    public void Die()
    {

    }
}