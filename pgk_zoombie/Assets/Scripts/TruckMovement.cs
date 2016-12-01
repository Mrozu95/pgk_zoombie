using UnityEngine;
using System.Collections;

public class TruckMovement : MonoBehaviour {
    public bool canMove = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(canMove)
        transform.position = new Vector3(this.transform.position.x + 0.58f, this.transform.position.y, this.transform.position.z);
	}

    public void setCanMove(bool can)
    {
        canMove = can;
    }
}
