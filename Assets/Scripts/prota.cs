using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class prota : MonoBehaviour
{ 
    [SerializeField] int velocidad = 3; //velocidad del personaje
    [SerializeField] int MAXvelocidad = 10; //velocidad del personaje
    [SerializeField] int fuerza_salto = 1; //fuerza del salto del personaje

    bool en_suelo = true; //variable para saber si el personaje esta en el suelo

    Rigidbody2D rb; //Referencia al rigidbody2D del personaje

    public InputAction movimiento;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movimiento.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del personaje
        /*
        //Izquierda y derecha
        if (Keyboard.current.dKey.isPressed)

        {
            //Movimiento a la derecha
            rb.linearVelocityX = Mathf.Min(rb.linearVelocityX + velocidad, MAXvelocidad);
        }
        if (Keyboard.current.aKey.isPressed)

        {
            //Movimiento a la izquierda
            rb.linearVelocityX = Mathf.Max(rb.linearVelocityX - velocidad, -MAXvelocidad);
        }
        */
        float ejeX = movimiento.ReadValue<float>();

        rb.linearVelocity = new Vector2(
            ejeX * velocidad,
            rb.linearVelocity.y
        );


        //salto
        if (Keyboard.current.wKey.wasPressedThisFrame && en_suelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerza_salto);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            die();
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detectar colision con items
        if (collision.gameObject.tag == "Item")
        {
            //print("has tocado el item");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerza_salto);

        }

        if (collision.gameObject.tag == "Floor")
        {
            en_suelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            en_suelo = false;
        }
    }

    private void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
