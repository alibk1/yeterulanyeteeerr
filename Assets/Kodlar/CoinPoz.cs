using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoz : MonoBehaviour
{
    float x, z;
    void Start()
    {
        x = Random.Range(2,12);
        z = Random.Range(-10,17);
        this.transform.position = new Vector3(x, -6.8f, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
