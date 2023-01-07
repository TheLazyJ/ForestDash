using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class LaneTracker : MonoBehaviour
{
    public float currentLane = -1.15f; // current lane of the character, with 0 being the center lane
    public float laneWidth = 1.5f; // width of each lane
    public float laneChangeSpeed = 5.0f; // speed at which the character changes lanes
    private float minSwipeDistance = 151f;
    private Vector2 startPos;
    public Button leftArrowBtn, rightArrowBtn;
    
    void Start(){
        leftArrowBtn.onClick.AddListener(leftLane);
        rightArrowBtn.onClick.AddListener(rightLane);
    }
    void Update() {

        // canMove is false as soon player hit object
        if (PlayerMove.canMove == true) {
            // Check for lane change input
            if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane > -1.25) {
                currentLane--;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane < -0.20) {
                currentLane++;
            }

            
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
                        if (swipeDistance.x > 0 && currentLane > -1.25){
                            currentLane--; //right swipe
                        } else if ( currentLane < -0.20){
                            currentLane++; //left swipe
                        }
                    }
                } else if (touch.phase == TouchPhase.Ended) {
                    // Reset the starting position of the touch
                    startPos = Vector2.zero;
                }
            }
            
            
            // Update Z target to change lane
            float targetZ = currentLane * laneWidth;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, targetZ), laneChangeSpeed * Time.deltaTime);
        }
    }

    void leftLane(){
        // Check for lane change input
        if (currentLane < -0.20) {
            currentLane++;
        }
    }

    void rightLane(){
        // Check for lane change input
        if (currentLane > -1.25) {
            currentLane--;
        }
    }

}
