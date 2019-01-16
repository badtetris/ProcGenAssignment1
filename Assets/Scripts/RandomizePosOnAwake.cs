using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosOnAwake : MonoBehaviour {

    public float minX = -50;
    public float maxX = 50;

    public float minY = -4;
    public float maxY = 4;

    void Awake() {
        transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    }

}
