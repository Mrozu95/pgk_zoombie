using UnityEngine;
using System.Collections;

public class Lvl_1_player_movment : MonoBehaviour {

    Rigidbody rb;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        set_speed(30);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        movment();
	}

    void set_speed(float speed)
    {
        this.speed = speed;
    }


    //sterowanie graczem
    void movment()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movment * speed);
    }
}
