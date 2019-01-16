using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.CompareTag("Player")) {
            EndScene.loadWinScene();
        }
    }
}
