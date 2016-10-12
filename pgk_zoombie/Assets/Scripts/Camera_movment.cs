using UnityEngine;
using System.Collections;

public class Camera_movment : MonoBehaviour {

    public Transform object_to_follow;

    //pozycja kamery wzgledem obiektu object_to_folllow
    private Vector3 transformation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transformation = new Vector3(-object_to_follow.position.x, 10, -10);
        transform.position = object_to_follow.position + transformation;
	}
}
