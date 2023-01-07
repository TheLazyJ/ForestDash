using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    // Create the 4 array to store 4 kind of obstacles
    public GameObject[] jumpObstacle;
    public GameObject[] slideObstacle;
    public GameObject[] smallObstacle;
    public GameObject[] tallObstacle;
    public GameObject[] clouds;

    // Import collision mask "itemGenerate" & vector
    public LayerMask collisionMask;
    public Vector3 myPoint;

    // Create variable as each obstacle generate at different distance
    public float xPosSmall = 20f;
    public float xPosTall = 20f;
    public float xPosLog = 20f;
    public float xPosSlide = 30f;
    public float xPosCloud = 20f;
    public int secNum3;
    public int secNum4;
    public int lane;
    public float zPosSmall = -1.75f;

    void Update() {
        // Start the 4 Coroutine to generate obstacle, always true // and cloud
        StartCoroutine(GenerateSmallObstacles());
        StartCoroutine(GenerateTallObstacles());
        StartCoroutine(GenerateJumpObstacles());
       // StartCoroutine(GenerateSlideObstacles()); Removed for now
        StartCoroutine(GenerateCloud());
    }

    // Small obstacle, will choose lane 1 to 3, and do not generate more than 210 unit in advance of player
    IEnumerator GenerateSmallObstacles() {
        while (true && xPosSmall < PlayerMove.playerXPos + 210){
            lane = Random.Range(0, 3);
            if (lane == 0) {
                zPosSmall = -3.25f;
            } else if (lane == 1) {
                zPosSmall = -1.75f;
            } else {
                zPosSmall = -0.25f;
            }
            // Random size of the small obstacle array
            secNum3 = Random.Range(0, 6);
            // Create a Vector3 position, and check if there anything in layer "itemGenerate"
            myPoint = new Vector3(xPosSmall,-2.25f,zPosSmall);
            // If no collision, execute item
            if (!Physics.CheckSphere(myPoint, 0.5f, collisionMask)) {
                Instantiate(smallObstacle[secNum3], new Vector3(xPosSmall,-2.25f,zPosSmall), Quaternion.identity);
            }
            // Generate next obstacles at different distance
            xPosSmall += Random.Range(10,35); 
            // Option to slow down the generation
            yield return new WaitForSeconds(0f);
        }
    }

    // Tall obstacle, will choose lane 1 to 3, and do not generate more than 210 unit in advance of player
    IEnumerator GenerateTallObstacles() {
        while (true && xPosTall < PlayerMove.playerXPos + 210){
            lane = Random.Range(0, 3);
            if (lane == 0) {
                zPosSmall = -3.25f;
            } else if (lane == 1) {
                zPosSmall = -1.75f;
            } else {
                zPosSmall = -0.25f;
            }
            secNum4 = Random.Range(0, 3);
            myPoint = new Vector3(xPosTall,-1.5f,zPosSmall);
            if (!Physics.CheckSphere(myPoint, 0.5f, collisionMask)) {
                Instantiate(tallObstacle[secNum4], new Vector3(xPosTall,-1.5f,zPosSmall), Quaternion.identity);
            }
            xPosTall += Random.Range(10,35);
            yield return new WaitForSeconds(0f);
        }
    }

    // Jump obstacle, generate in the center, do not generate more than 210 unit in advance of player
    IEnumerator GenerateJumpObstacles() {
        while (true && xPosLog < PlayerMove.playerXPos + 210){
            myPoint = new Vector3(xPosLog,-2.5f,-2f);
            if (!Physics.CheckSphere(myPoint, 3f, collisionMask)) {
                Instantiate(jumpObstacle[0], new Vector3(xPosLog,-2.5f,-2f), Quaternion.identity);
            }
            xPosLog += Random.Range(50,100);
            yield return new WaitForSeconds(0f);
        }
    }

    // sGenerate cloud, no need to check for collision, random with
    IEnumerator GenerateCloud(){
        while (true && xPosCloud < PlayerMove.playerXPos + 310){
            Instantiate(clouds[Random.Range(0,2)], new Vector3(xPosCloud, 4f, Random.Range(-20f,10f)), Quaternion.identity);
            xPosCloud += Random.Range(40,80);
            yield return new WaitForSeconds(0f);
        }
    }
    /*
    // Slide obstacle, generate in the center, do not generate more than 210 unit in advance of player
    IEnumerator GenerateSlideObstacles() {
        while (true && xPosSlide < PlayerMove.playerXPos + 210){
            secNum4 = Random.Range(0, 3);
            myPoint = new Vector3(xPosSlide,0f,-1.5f);
            if (!Physics.CheckSphere(myPoint, 3f, collisionMask)) {
                Instantiate(slideObstacle[0], new Vector3(xPosSlide,0f,-1.5f), Quaternion.identity);
            }
            xPosSlide += Random.Range(50,100);
            yield return new WaitForSeconds(0f);
        }
    }
    */
}
