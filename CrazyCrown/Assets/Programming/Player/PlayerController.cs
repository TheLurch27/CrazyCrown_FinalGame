using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sneakSpeed = 2.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private bool isWalking = false;
    private bool isSneaking = false;
    private bool isSaluting = false;
    private bool isInteracting = false;
    private Rigidbody2D rb;

    private bool blockPlayerInput = false; // Flag, um den Input zu blockieren

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!blockPlayerInput) // Input nur zulassen, wenn nicht blockiert
        {
            HandleMovement();
            HandleSalute();
            HandleInteract();
            HandlePickUpItem();
        }
        else
        {
            rb.velocity = Vector2.zero; // Bewegung stoppen, wenn Input blockiert ist
        }
    }

    // Methode, um den Input zu blockieren oder freizugeben
    public void BlockPlayerInput(bool shouldBlock)
    {
        blockPlayerInput = shouldBlock;

        if (shouldBlock)
        {
            rb.velocity = Vector2.zero; // Bewegung stoppen, wenn der Input blockiert ist
        }
    }

    void HandleMovement()
    {
        if (!isInteracting && !isSaluting) // Bewegung nur erlauben, wenn nicht salutiert oder interagiert wird
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(horizontalInput, 0f);

            float speed = isSneaking ? sneakSpeed : walkSpeed;
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

            UpdateAnimation(horizontalInput);
            UpdateSpriteDirection(horizontalInput);
        }
        else
        {
            rb.velocity = Vector2.zero; // Bewegung stoppen, wenn Interaktion oder Salutieren aktiv ist
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSneaking = false;
        }

        animator.SetBool("isSneaking", isSneaking);
    }

    void UpdateAnimation(float horizontalInput)
    {
        isWalking = horizontalInput != 0f;
        animator.SetBool("isWalking", isWalking);
    }

    void UpdateSpriteDirection(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void HandleSalute()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSaluting = true;
            animator.SetBool("isSaluting", true);
            animator.SetBool("isWalking", false);

            rb.velocity = Vector2.zero;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isSaluting = false;
            animator.SetBool("isSaluting", false);
            animator.SetBool("isWalking", true);
        }
    }

    void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
            animator.SetBool("isInteracting", true);
            rb.velocity = Vector2.zero; // Bewegung stoppen
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = false;
            animator.SetBool("isInteracting", false);
        }
    }

    void HandlePickUpItem()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("isPickingUpItem");
        }
    }
}
