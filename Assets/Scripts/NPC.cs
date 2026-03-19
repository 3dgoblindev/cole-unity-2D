using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    private InputAction accionHablar;
    bool puedesHablar = false; // Flag to determine if the player can enter the door
    public GameObject dialogo;
    GameObject player;

    void Start()
    {
        
        accionHablar = InputSystem.actions.FindAction("Jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            // Example: SceneManager.LoadScene("NextScene");
            puedesHablar = true; // Set the flag to true when the player enters the door
            player = collision.gameObject; // Store a reference to the player GameObject

            player.GetComponent<RPG>().InteractVisual(true); // Call the InteractVisual method on the player's RPG script to show the interaction prompt

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            puedesHablar = false; // Set the flag to false when the player exits the door
            player.GetComponent<RPG>().InteractVisual(false); // Call the InteractVisual method on the player's RPG script to show the interaction prompt
            dialogo.SetActive(false);
            player = null;
        }
    }

    void Update()
    {
        if (puedesHablar && accionHablar != null && accionHablar.WasPressedThisFrame())
        {
            print("Estamos hablando");
            dialogo.SetActive(true);
            player.GetComponent<RPG>().InteractVisual(false); // Call the InteractVisual method on the player's RPG script to show the interaction prompt


        }
    }
}
