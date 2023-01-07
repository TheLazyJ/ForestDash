using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public static float playerXPos;
    public float moveSpeed = 5;
    public float tempX = 0;
    static public bool canMove;
    //public AudioSource coinFX;
    public GameObject charModel;
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject fadeout;
    public BoxCollider boxCollider; 
    public string skinChoice;
    public GameObject[] skins;
    public int skinNumber;
    public GameObject player;
    public AudioSource jumpFX;
    public AudioSource coinFX;
    private int tempCoinCount;
    private float minSwipeDistance = 3f;
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;
    public Button jumpBtn;

    void Start () {
        jumpBtn.onClick.AddListener(jump);
        canMove = true;
        CollactableControl.coinCount = 0;

        // Pull from Playerpref the skinchoice made in MainRun.Cs
        skinChoice = PlayerPrefs.GetString("skinChoiceSave");
        if (skinChoice == null){
            skinChoice = "Amy";
        }

        // Attach the good number for the skin choice to array skin generatior
        switch (skinChoice){
            case "Amy":
                skinNumber = 0;
                break;
            case "Luchador":
                skinNumber = 1;
                break;
            case "Granny":
                skinNumber = 2;
                break;
            case "Michelle":
                skinNumber = 3;
                break;
            case "Mousey":
                skinNumber = 4;
                break;
            case "XBot":
                skinNumber = 5;
                break;                                                                                
        }

        // Create the skin Prefab
        Instantiate(skins[skinNumber], player.transform);
        charModel = GameObject.Find(skinChoice + "(Clone)");

        // Initiate sound loop
        tempCoinCount = CollactableControl.coinCount;
    }

    

    void Update() {
        // Loop to play sound when picking coin
        if (CollactableControl.coinCount > tempCoinCount){
            coinFX.Play();
            tempCoinCount = CollactableControl.coinCount;
        }

        // Always update playerXPos, used to limit where to generate objects and when to destroy them
        playerXPos = transform.position.x;

        // Script to accelerate moveSpeed depending of the distance done
        if (playerXPos < 250) {
            if (playerXPos - tempX >= Random.Range(20,30)){
                moveSpeed += 1;
                tempX = playerXPos;
            }
        } else if (playerXPos < 750) {
            if (playerXPos - tempX >= Random.Range(70,90)){
                moveSpeed += 1;
                tempX = playerXPos;
            }
        } else if (playerXPos < 1250) {
            if (playerXPos - tempX >= Random.Range(120,150)){
                moveSpeed += 1;
                tempX = playerXPos;
            }
        } else {
            if (playerXPos - tempX >= Random.Range(180,220)){
                moveSpeed += 1;
                tempX = playerXPos;
            }
        }

        // Move player on the X axis to constant pace
        if (canMove == true){
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);

            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y == -1.5) {
                jump();
            } else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y == -1.5){
                //   slide(); removed for now, didn't had to the gameplay/experience
            }

            // Script to detect if screen tap to jump
            /*
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    swipeStartPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    swipeEndPos = touch.position;

                    float swipeDistance = Vector2.Distance(swipeStartPos, swipeEndPos);
                    if (swipeDistance > minSwipeDistance)
                    {
                        Vector2 swipeDirection = swipeEndPos - swipeStartPos;
                        if (swipeDirection.y > 0)
                        {
                            jump();
                        }
                    }
                }
            }
            */
        }

    }

    void jump(){
        boxCollider.center = new Vector3(0f, 0.5f, 0f);
        charModel.GetComponent<Animator>().Play("normalJump");
        Invoke("EndJumpSlide", 0.6f);
    }

    void EndJumpSlide(){
        boxCollider.center = new Vector3(0f, 0f, 0f);
        jumpFX.Play();
    }

    /*
    void slide(){
        boxCollider.center = new Vector3(0f, -0.5f, 0f);
        charModel.GetComponent<Animator>().Play("slide");
        Invoke("EndJumpSlide", 0.6f);
    }
    */
    

    // Start as soon hit any object with the tag 'obstacle'
    void OnTriggerEnter(Collider other){
        if (other.tag == "obstacle"){
            // Desactive the Box Collider of player to avoid loop
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            // Make sure no more player movement (jump/slide/lateral)
            canMove = false;
            // Use the animation on the camera to shake the screen
            mainCam.GetComponent<Animator>().enabled = true;
            // Switch to the animation 'runningHit'
            charModel.GetComponent<Animator>().Play("runningHit");
            crashThud.Play();
            fadeout.SetActive(true);
            // Wait 1.5s before loading gameoverMenu function
            Invoke("gameoverMenu", 1.5f);
        }
    }

    // Function that change scene, but also save score and coins in static variable in mainrun.cs
    void gameoverMenu() {   
        MainRun.totalCoins += CollactableControl.coinCount;
        MainRun.totalRun = CollactableControl.scoreCount;
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }
}
