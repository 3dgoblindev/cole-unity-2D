using UnityEngine;
using UnityEngine.InputSystem;

public class RPG : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
}