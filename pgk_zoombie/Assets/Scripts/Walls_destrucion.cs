using UnityEngine;
using System.Collections;

public class Walls_destrucion : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void OnTriggerEnter(Collider wall)
    {
        if(wall.gameObject.CompareTag("Destructible_wall"))
        {
            wall.gameObject.SetActive(false);
        }
    }
}
