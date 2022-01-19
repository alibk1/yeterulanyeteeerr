using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour {

    public GameObject ball;
    Vector3 offset;

	void Start () {
        offset = transform.position - ball.transform.position;
	}
	
	void LateUpdate () {
        transform.position = ball.transform.position + offset;

    }
}
