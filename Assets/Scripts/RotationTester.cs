using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTester : MonoBehaviour
{

    public int timer = 0;
    public GameObject follower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer++;

        if (timer > 1000)
        {
            Debug.Log(gameObject.transform.eulerAngles);
            follower.transform.eulerAngles = gameObject.transform.eulerAngles;
            timer = 0;
        }
        
    }
}
