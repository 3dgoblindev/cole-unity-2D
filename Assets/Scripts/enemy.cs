using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] int velocidad = 1; //velocidad del personaje
    [SerializeField] int MAXvelocidad = 2; //velocidad del personaje
                                           // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb; //Referencia al rigidbody2D del personaje

    bool direccion = true; //true derecha, false izquierda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direccion == false)
            rb.linearVelocityX = Mathf.Max(rb.linearVelocityX - velocidad, -MAXvelocidad);
        else
            rb.linearVelocityX = Mathf.Min(rb.linearVelocityX + velocidad, MAXvelocidad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyWall")
        {
            direccion = !direccion;
        }
    }
}
