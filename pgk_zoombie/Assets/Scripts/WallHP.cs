using UnityEngine;
using System.Collections;

public class WallHP : MonoBehaviour {

    public int HP { get; set; }
    // Use this for initialization
    void Start () {
        HP = 100;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //public void OnCollisionEnter(Collider zombie)
    //{
       // if (zombie.gameObject.CompareTag("Zombie"))
        //{
           // HP -= 10;
            //if (HP <= 0)
                //this.gameObject.SetActive(false);
        //}
    //}
}
