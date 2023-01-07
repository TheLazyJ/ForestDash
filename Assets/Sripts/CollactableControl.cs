using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Import TMPro vice UI to edit txt on the canva, unity 2022
using TMPro;

public class CollactableControl : MonoBehaviour
{
    // Collect Coins
    public static int coinCount = 0;
    public static int scoreCount = 0;
    public TMP_Text coinCountDisplay;
    public TMP_Text scoreCountDisplay;

    void Update(){
        // Adjust Coin total & distance on the canvas, to string as TXT take string not int
        coinCountDisplay.SetText(coinCount.ToString());
        scoreCount = (int)PlayerMove.playerXPos;
        scoreCountDisplay.SetText(scoreCount.ToString());
    }

}
