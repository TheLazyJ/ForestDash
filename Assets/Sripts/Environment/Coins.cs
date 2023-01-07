using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    void Update(){
        // Rotate Coins
        transform.Rotate(0, 0.25f, 0, Space.World);
    }

    // Play pick-up sound, add 1 to coin counter and destroy the coin pick
    void OnTriggerEnter(Collider other){
        CollactableControl.coinCount += 1;
        Destroy(gameObject);
    }
}
