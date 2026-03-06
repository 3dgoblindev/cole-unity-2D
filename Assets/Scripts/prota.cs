using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class prota : MonoBehaviour
{ 
    [SerializeField] int velocidad = 3; //velocidad del personaje
    [SerializeField] int MAXvelocidad = 10; //velocidad del personaje
    [SerializeField] int fuerza_salto = 1; //fuerza del salto del personaje

    Animator anim; //Referencia a la animacion del personaje

    bool en_suelo = true; //variable para saber si el personaje esta en el suelo
    bool can_move = true; //variable para saber si el personaje puede moverse

    Rigidbody2D rb; //Referencia al rigidbody2D del personaje
    CapsuleCollider2D collider2D; //Referencia al collider2D del personaje

    InputAction movimiento;
    InputAction jumpAction;



    AudioSource au; //Referencia al audio source del personaje

    [SerializeField] AudioClip saltoclip; //Referencia al audio clip del salto
    [SerializeField] AudioClip muerteclip; //Referencia al audio clip de la muerte
    [SerializeField] AudioClip killclip; //Referencia al audio clip de matar a un enemigo
    [SerializeField] AudioClip itemclip; //Referencia al audio clip de recoger un item

    public GameObject GameManager;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.Find("GameManager");

        movimiento = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump"); 


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();

        au = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


        if (can_move == false || GameManager.GetComponent<GameManager>().pausa == true)
        {
            return;
        }

        float ejeX = movimiento.ReadValue<Vector2>().x;

        rb.linearVelocity = new Vector2(
            ejeX * velocidad,
            rb.linearVelocity.y
        );


        //salto
        if (jumpAction.WasPressedThisFrame() && en_suelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerza_salto);
            au.PlayOneShot(saltoclip);
        }
        
        //parte visual del movimiento
        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("walking", true);

            if (rb.linearVelocityX > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (rb.linearVelocityX < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {             
            anim.SetBool("walking", false);
        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            au.PlayOneShot(muerteclip);
            die();
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detectar colision con items
        if (collision.gameObject.tag == "Item")
        {
            GameManager.GetComponent<GameManager>().puntos += 1;
            //print("has tocado el item");
            Destroy(collision.gameObject);
            au.PlayOneShot(itemclip, 2);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.GetComponent<GameManager>().puntos += 5;
            Destroy(collision.gameObject);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerza_salto);
            au.PlayOneShot(killclip);

        }

        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("jumping", false);
            en_suelo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            en_suelo = false;
            anim.SetBool("jumping", true);
        }
    }

    private void die()
    {
        //desactivamos el movimiento, el collider y el rigidbody
        movimiento.Disable();
        collider2D.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;

        can_move = false;
       

        anim.SetBool("dead", true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Invoke("Reset_level", 2f);
    }

    private void Reset_level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
