using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject cameraZoom;
    bool zoomedIn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
