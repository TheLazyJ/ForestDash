using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public Button mainMenuBtn, runBtn, leaderBtn, storeBtn, muteBtn, unmuteBtn, exitBtn;
    public TMP_Text totalcoinCountDisplay;
    public TMP_Text scoreDisplay;
    public GameObject muteBtnDisplay, unmuteBtnDisplay;

    void Start(){   
        // Save coin 
        PlayerPrefs.SetInt("totalCoins", MainRun.totalCoins);
        PlayerPrefs.Save();

        // Add High Score to the list
        HighscoreTable.AddHighscoreEntry(MainRun.totalRun, PlayerPrefs.GetString("skinChoiceSave"));

        // List all buttons in the canvas, and associate a function to them
        mainMenuBtn.onClick.AddListener(gomainmenu);
        runBtn.onClick.AddListener(runGame);
        leaderBtn.onClick.AddListener(showLeaderboard);
        storeBtn.onClick.AddListener(showStore);
        muteBtn.onClick.AddListener(muteSound);
        unmuteBtn.onClick.AddListener(unmuteSound); 
        exitBtn.onClick.AddListener(exitGame);    
    }

    void Update() {
        // Check if sound was desactivated from another menu
        if (MainRun.soundStatus == 0) {
            unmuteBtnDisplay.SetActive(true);
            muteBtnDisplay.SetActive(false);
        } 

        // Get score and score from mainrun.cs (already in int format), and show
       scoreDisplay.SetText(MainRun.totalRun.ToString());
       totalcoinCountDisplay.SetText(MainRun.totalCoins.ToString());
    }

    void runGame() {   
        // Load base scene
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    void gomainmenu() {
        //return main menu scene
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    void showLeaderboard() {
        // show leaderboard scene
        SceneManager.LoadScene("LeaderBoard", LoadSceneMode.Single);
    }

    void showStore() {
        // load store scene
        SceneManager.LoadScene("StoreScene", LoadSceneMode.Single);
    }

    void muteSound(){
        unmuteBtnDisplay.SetActive(true);
        muteBtnDisplay.SetActive(false);
        // Mute all sound
        AudioListener.volume = 0;
        MainRun.soundStatus = 0;
    }

    void unmuteSound(){
        unmuteBtnDisplay.SetActive(false);
        muteBtnDisplay.SetActive(true);
        // Unmute all sound
        AudioListener.volume = 1;
        MainRun.soundStatus = 1;
    }

    void exitGame(){
        Application.Quit();
    }

}
