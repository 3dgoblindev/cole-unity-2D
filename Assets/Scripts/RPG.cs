using UnityEngine;
using UnityEngine.InputSystem;

public class RPG : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    public GameObject InteractIcon;

    private Vector2 movement;
    private Vector2 last_movement;


    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y = 1;
        if (Keyboard.current.sKey.isPressed)
            movement.y = -1;
        if (Keyboard.current.aKey.isPressed)
            movement.x = -1;
        if (Keyboard.current.dKey.isPressed)
            movement.x = 1;

        rb.linearVelocity = movement.normalized * speed;

        if (movement != Vector2.zero)
        {
            last_movement = movement;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetFloat("Mov_X", movement.x);
        animator.SetFloat("Mov_Y", movement.y);

        animator.SetFloat("Last_Mov_X", last_movement.x);
        animator.SetFloat("Last_Mov_Y", last_movement.y);

        if (movement.x == 1)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x == -1)
        {
            spriteRenderer.flipX = false;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Casa"))
        {
            spriteRenderer.sortingOrder = 1;
            print("Collided with Casa, sorting order set to 1");
        }
        print("Collided with " + collision.gameObject.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Casa"))
        {
            spriteRenderer.sortingOrder = 3;
            print("Exited collision with Casa, sorting order set to 3");
        }
    }

    public void InteractVisual(bool can) {
        InteractIcon.SetActive(can);
    }
}