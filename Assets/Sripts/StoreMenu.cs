using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoreMenu : MonoBehaviour
{
    public Button mainMenuBtn, buyBtn, CharSelect;
    public TMP_Text totalcoinCountDisplay;
    public GameObject nothingName, buyBtnDisplay, CharSelectDisplay;
    public AudioSource buyFX;

    void Start() {
        // Initiate listener for buttons in the scene
        mainMenuBtn.onClick.AddListener(gomainmenu);
        buyBtn.onClick.AddListener(buySkin);
        CharSelect.onClick.AddListener(selectXBot);
    }

    void Update(){
        // Show total coins
        totalcoinCountDisplay.SetText(MainRun.totalCoins.ToString()); 

        // Check if char already unlock, if yeah, show a sorry msg
        if (PlayerPrefs.GetInt("charUnlock") == 101)
        {
            CharSelectDisplay.SetActive(false);
            nothingName.SetActive(true);
        }
    }

    void gomainmenu(){
        // Load mian menu scene
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    void selectXBot(){
        // Grey the XBot image when selected and display buy btn
        Color selectedColor;
        ColorUtility.TryParseHtmlString("#989898", out selectedColor);
        CharSelect.GetComponent<Image>().color = selectedColor;
        buyBtnDisplay.SetActive(true);
    }

    void buySkin(){
        // Buy XBot Skin for 5000 gold
        if (MainRun.totalCoins > 5000){
            MainRun.totalCoins -= 5000;
            PlayerPrefs.SetInt("charUnlock", 101);
            buyFX.Play();
            CharSelectDisplay.SetActive(false);
            buyBtnDisplay.SetActive(false);
            nothingName.SetActive(true);
        }
    }


}
