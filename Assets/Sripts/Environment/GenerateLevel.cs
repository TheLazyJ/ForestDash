using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    // Level separte in 3 part, the trail, and each side
    public GameObject[] dirtFloor;
    public GameObject[] sideLeft;
    public GameObject[] sideRight;
    // Starting point
    public float xPos = 16f;
    public float xPosTrail;
    public int secNum1;
    public int secNum2;

    // Our starting sound
    public AudioSource goFX;

    void Start(){
        StartCoroutine(StartSound());
    }

    void Update(){
        // Start the coroutine to generate map
        StartCoroutine(GenerateSection());
    }

    // No more than 210 unit in advance of player.position.X
    IEnumerator GenerateSection() {
        while (true && xPos < PlayerMove.playerXPos + 210){
            secNum1 = Random.Range(0, 4);
            secNum2 = Random.Range(0, 4);
            xPosTrail = xPos;
            // Each side has different array of possibility
            Instantiate(sideLeft[secNum1], new Vector3(xPos,-2.6f,20.3f), Quaternion.identity);
            Instantiate(sideRight[secNum2], new Vector3(xPos,-2.6f,-23.7f), Quaternion.identity);
            // To make curved world more fluid, center trail is in 10 unit length, vice 50 for the side
            // Reason why it loop 5 time so the map by each IEnumeration generate, 50 unit of maps
            for (int i = 0; i < 5; i++) {
                Instantiate(dirtFloor[0], new Vector3(xPosTrail,-3f,-1.75f), Quaternion.identity);
                xPosTrail += 10;
            }
            xPos += 50;
            // For now, no need to wait as loop is limited by no more than 210 unit (4x200)
            yield return new WaitForSeconds(0f);
        }
    }

    // Generate starting sound, nothing for now
    IEnumerator StartSound() {
        yield return new WaitForSeconds(3f);
    }   


}
