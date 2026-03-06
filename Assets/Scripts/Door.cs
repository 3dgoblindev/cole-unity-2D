using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    private InputAction accionEntrar;
    bool puedesEntrar = false; // Flag to determine if the player can enter the door

    GameObject player;

    Vector2 target_pos;
    public Transform target_transform;

    void Start()
    {
        target_pos = target_transform.position;
        accionEntrar = InputSystem.actions.FindAction("Jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Load the next scene or perform any desired action
            Debug.Log("Player entered the door!");
            // Example: SceneManager.LoadScene("NextScene");
            puedesEntrar = true; // Set the flag to true when the player enters the door
            player = collision.gameObject; // Store a reference to the player GameObject

            player.GetComponent<RPG>().InteractVisual(true); // Call the InteractVisual method on the player's RPG script to show the interaction prompt

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Perform any desired action when the player exits the door
            Debug.Log("Player exited the door!");
            puedesEntrar = false; // Set the flag to false when the player exits the door
            player.GetComponent<RPG>().InteractVisual(false); // Call the InteractVisual method on the player's RPG script to show the interaction prompt

            player = null;
        }
    }

    void Update()
    {
        // You can use the puedesEntrar flag to trigger actions in other scripts
        if (puedesEntrar && accionEntrar != null && accionEntrar.WasPressedThisFrame())
        {
            print("Player can enter the door and has pressed the action button!");
            // Example: Trigger an animation, enable a UI element, etc.

            player.transform.position = target_pos; // Move the player to the target position

        }
    }
}
