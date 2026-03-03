using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cameraZoom;
    bool zoomedIn = false;

    public bool pausa = false; //variable para saber si el juego esta en pausa 
    public GameObject menu_pause; //Referencia al menu de pausa
    GameObject musica; //Referencia a la musica del juego
    AudioSource music_au; //Referencia al audio source de la musica del juego

    public int puntos = 0; //variable para contar los puntos del
    public GameObject marcador_pt; //Referencia al marcador de puntos
    public TMP_Text text_pt; //Referencia al texto del marcador de puntos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musica = GameObject.Find("musica");

        music_au = musica.GetComponent<AudioSource>();

        text_pt = marcador_pt.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        text_pt.text = "Puntos: " + puntos; //actualiza el marcador de puntos
        //menu
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            pausa = !pausa;
            if (pausa)
            {
                print("pausado");
                Time.timeScale = 0;
                menu_pause.SetActive(true);
                music_au.volume = 0.2f;
            }
            else
            {
                print("no pausado");
                Time.timeScale = 1;
                menu_pause.SetActive(false);
                music_au.volume = 1f;
            }
        }

        if (Keyboard.current.rKey.wasPressedThisFrame) //reset the level
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        //Zoom z
        if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            zoomedIn = !zoomedIn;
            cameraZoom.SetActive(zoomedIn);
        }
        //Zoom x
    }
}
