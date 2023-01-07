using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainRun : MonoBehaviour
{
    
    public Button charLeft, charRight, runBtn, leaderBtn, storeBtn, muteBtn, unmuteBtn, exitBtn;
    public static int totalCoins;
    public static int totalRun = 0;
    public List<string> skinList2 = new List<string>  {"Amy", "Luchador", "Granny", "Michelle", "Mousey"};//, "XBot"};
    public TMP_Text charName;
    public int charSelected;
    public GameObject Amy, Luchador, Granny, Michelle, Mousey, XBot, muteBtnDisplay, unmuteBtnDisplay;
    public int listLength;
    public AudioSource menuFX, menuSwitchFX;
    public static int soundStatus = 1; // Keep if sound was mute or note
    private float minSwipeDistance = 200f;
    private Vector2 startPos;

    void Start() {
        //PlayerPrefs.DeleteAll(); //reset game for production

        // Pull total coins from playerpref ssave
        if (PlayerPrefs.GetInt("totalCoins") != null) {
            totalCoins = PlayerPrefs.GetInt("totalCoins");
        }

        // Pull the char saved
        charSelected = PlayerPrefs.GetInt("charIntSave");
        if (charSelected == null){
            charName.SetText("Amy"); //Default name
            charSelected = 0;
        }
        changeCharImg(); // Update Img to show

        // Check if player bought skin
        if (PlayerPrefs.GetInt("charUnlock") == 101){
            skinList2.Add("XBot");
        }

        // Create listener for all buttons on canvas
        charLeft.onClick.AddListener(changeLeftChar);
        charRight.onClick.AddListener(changeRightChar);
        runBtn.onClick.AddListener(runGame);
        leaderBtn.onClick.AddListener(showLeaderboard);
        storeBtn.onClick.AddListener(showStore);
        muteBtn.onClick.AddListener(muteSound);
        unmuteBtn.onClick.AddListener(unmuteSound); 
        exitBtn.onClick.AddListener(exitGame);     
    }

    void Update(){
        // Check if sound was desactivated from another menu
        if (soundStatus == 0) {
            unmuteBtnDisplay.SetActive(true);
            muteBtnDisplay.SetActive(false);
        } 

        listLength = skinList2.Count - 1;

        // Script to detect left/right swipe
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                // Store the starting position of the touch
                startPos = touch.position;
            } else if (touch.phase == TouchPhase.Moved) {
                // Calculate the swipe distance
                Vector2 swipeDistance = touch.position - startPos;

                // Check if the swipe distance is greater than the minimum swipe distance
                if (swipeDistance.magnitude > minSwipeDistance) {
                    // Check if the swipe is to the left or right
                    if (swipeDistance.x > 0){
                        changeLeftChar(); //right swipe
                    } else {
                        changeRightChar(); //left swipe
                    }
                }
            } else if (touch.phase == TouchPhase.Ended) {
                // Reset the starting position of the touch
                startPos = Vector2.zero;
            }
        }
    }

    void runGame() {
        // Save skin choice
        PlayerPrefs.SetInt("charIntSave", charSelected);
        PlayerPrefs.SetString("skinChoiceSave", skinList2[charSelected]);
        PlayerPrefs.Save();
        // load base game scene
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    void changeLeftChar() {
        if (charSelected > 0){
            charSelected--; 
            menuSwitchFX.Play();
        }
        changeCharImg();
    }

    void changeRightChar() {
        if (charSelected < listLength){
            charSelected++; 
            menuSwitchFX.Play();
        }
        changeCharImg();
    }

    // Function to show good img, to do it again, I would use name vs #
    void changeCharImg(){
        Amy.SetActive(false);
        Luchador.SetActive(false);
        Granny.SetActive(false);
        Michelle.SetActive(false);
        Mousey.SetActive(false);
        XBot.SetActive(false);
        switch (charSelected){
            case 0:
                Amy.SetActive(true);
                charName.SetText("Amy"); 
                break;
            case 1:
                Luchador.SetActive(true);
                charName.SetText("Luchador");
                break;
            case 2:
                Granny.SetActive(true);
                charName.SetText("Super Granny");
                break;
            case 3:
                Michelle.SetActive(true);
                charName.SetText("Michelle");
                break;
            case 4:
                Mousey.SetActive(true);
                charName.SetText("Mousey");
                break;
            case 5:
                XBot.SetActive(true);
                charName.SetText("XBot");
                break;                                                                                
        }     
    }

    void showLeaderboard() {
        // Load leaderboard scene
        SceneManager.LoadScene("LeaderBoard", LoadSceneMode.Single);
    }

    void showStore() {
        // Load store scene
        SceneManager.LoadScene("StoreScene", LoadSceneMode.Single);
    }

    void muteSound() {
        unmuteBtnDisplay.SetActive(true);
        muteBtnDisplay.SetActive(false);
        // Mute all sound
        AudioListener.volume = 0;
        soundStatus = 0; //track menu
    }

    void unmuteSound() {
        unmuteBtnDisplay.SetActive(false);
        muteBtnDisplay.SetActive(true);
        // UnMute all sound
        AudioListener.volume = 1;
        soundStatus = 1;
    }

    void exitGame(){
        Application.Quit();
    }
    
}
