using UnityEngine;
using System.Collections;

public class TruckMovement : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if(Health.currentHealth > 0 && Lvl_1_player_movment.canBeHitted == true)
        { 
            transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
            back();
        }
    }

    void back()
    {
        if (this.transform.position.x >= 35)
            transform.position = new Vector3(-30, this.transform.position.y, this.transform.position.z);
    }
}
