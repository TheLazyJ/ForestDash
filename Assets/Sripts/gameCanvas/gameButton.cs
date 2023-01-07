using UnityEngine;
using UnityEngine.UI;

public class gameButton : MonoBehaviour
{
    public GameObject pause;
    public GameObject play;
    public Button pauseB, playB;

    void Start() {
        // Listen if button is click
        pauseB.onClick.AddListener(pauseGame);
        playB.onClick.AddListener(unpauseGame);
        Invoke("PopPause", 2f);
    }

    void PopPause(){
        pause.SetActive(true);
    }

    void pauseGame(){
        // Pause game, desactive pause btn, show play btn 
        Time.timeScale = 0;
        pause.SetActive(false);
        play.SetActive(true);
    }

    void unpauseGame(){
        // Unpause game, show pause btn, desactive play btn
        Time.timeScale = 1;
        pause.SetActive(true);
        play.SetActive(false);
    }
}