using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlBotones : MonoBehaviour
{
    public void Start_Button()
    {
        SceneManager.LoadScene("SampleScene");
        print("Start");
    }

    public void Exit_Button()
    {
        print("Exit");
    }
}
