using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Degme : MonoBehaviour
{
    public GameObject spherePrefab, Cam, WinPanel, StartPanel;
    public Text skorTxt;
    [SerializeField] int skorum, starti = 120;
    bool startb;


    void Start()
    {
        
    }

    void Update()
    {
        skorTxt.text = skorum.ToString();
        if (startb)
        {
            Cam.GetComponent<Animator>().Play("Cam");
            starti--;
            if(starti == 0)
            {
                this.GetComponent<SwipeDetector>().enabled = true;
            }
        }
        if(skorum == 100)
        {
            WinPanel.SetActive(true);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            if (skorum < 50)
            {
                GameObject.Instantiate(spherePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            Destroy(other.gameObject);
            skorum += 10;
        }
    }
    public void Baslat()
    {
        startb = true;
        StartPanel.SetActive(false);
    }
    public void TekrarBaslat()
    {
        Application.LoadLevel(0);
    }
}
