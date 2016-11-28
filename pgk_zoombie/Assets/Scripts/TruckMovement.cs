using UnityEngine;
using System.Collections;

public class TruckMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(this.transform.position.x + 0.61f, this.transform.position.y, this.transform.position.z);
	}
}
