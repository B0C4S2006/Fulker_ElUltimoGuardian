using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private bool isDashing = true;
    [SerializeField] private float originalFallGravity;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        originalFallGravity = playerJump.fallGravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = false;
        playerJump.fallGravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);
        playerJump.fallGravityScale = originalFallGravity;
        isDashing = true;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            playerMovement.Move();
        }
    }
}

