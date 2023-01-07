using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    public Button mainMenuBtn;

    void Start(){
        // Only btn in the scene is to go back to main menu
        mainMenuBtn.onClick.AddListener(gomainmenu);
    }

    void gomainmenu(){
        // Load main menu scene
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

}
