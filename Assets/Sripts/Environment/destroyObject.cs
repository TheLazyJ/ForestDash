using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{

    void Update(){
        // destroy all object that is past 50 unit from the playerXPos
        if (transform.position.x < PlayerMove.playerXPos - 50) {
            Destroy(gameObject);
        }
    }
}
