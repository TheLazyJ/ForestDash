using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCoins : MonoBehaviour
{
    //Starting points & prefabs
    float xPos = 30;
    public GameObject coins;

    // Import collision mask "itemGenerate" & vector (same as obstacles)
    public LayerMask collisionMask;
    public Vector3 myPoint;

    void Update(){
        // Start the Coroutine to spawn coins
        StartCoroutine(SpawnCoins());
    }

    // Coins spawner, no more than 210 unit from player.X
    IEnumerator SpawnCoins() {
        while (true && xPos < PlayerMove.playerXPos + 210) {
            float zPos = -1.75f;
            // choose lane
            int lane = Random.Range(0, 3);
            if (lane == 0) {
                zPos = -3.25f;
            } else if (lane == 1) {
                zPos = -1.75f;
            } else {
                zPos = -0.25f;
            }

            // number of coins to spawn in a row (to create line)
            int coinsThisRow = Random.Range(3, 20);

            // generate the random coin in a row
            for (int i = 0; i < coinsThisRow; i++) {

                // Use Vector3, LayerMask = "itemGenerate" & Physics.ChecSphere, to avoid generating coins on obstacles
                myPoint = new Vector3(xPos,-2, zPos);
                if (!Physics.CheckSphere(myPoint, 0.5f, collisionMask)) {
                    Instantiate(coins, new Vector3(xPos,-2, zPos), Quaternion.identity);
                }
                // Put each coin 1 unit further
                xPos += 1;
            }
            // Create some space between coin row
            xPos += Random.Range(5,10);

            yield return new WaitForSeconds(0f);
        }
    }

}
