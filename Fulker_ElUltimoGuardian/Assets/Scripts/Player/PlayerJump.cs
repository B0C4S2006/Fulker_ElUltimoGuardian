using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce =  20.0f;
    public float fallGravityScale = 4.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radio = 0.1f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private int maxJumps = 1;
    [SerializeField] private int jumpCount = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && (IsGrounded() || jumpCount < maxJumps))
        {
            Jump();
            jumpCount++;
            IsGrounded();
        }
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            rb.gravityScale = 1f;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, radio, whatIsGround))
        {
            jumpCount = 0;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, radio);
    }
}
