﻿using UnityEngine;
using System.Collections;

public class FallingTree : MonoBehaviour {

    public Transform player;
    public float fallingSpeed = 100f;

	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z - player.position.z <= 13)
        {
            if (this.gameObject.tag == "Tree")
            {
                gameObject.tag = "KillingTree";
            }      


            if (this.transform.eulerAngles.x <= 70)
                fall();
            else
                gameObject.tag = "FallenTree";
        }

        
        
    }

    void fall()
    {
     
        transform.Rotate(Vector3.right * fallingSpeed * Time.deltaTime);

    }

}