﻿using UnityEngine;
using System.Collections;


public class Coin_script : MonoBehaviour {


	// Update is called once per frame
    // obracanie sie diamencikow/monetek
	void Update () {
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime);
	}

}
