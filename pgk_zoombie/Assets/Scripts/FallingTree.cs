using UnityEngine;
using System.Collections;

public class FallingTree : MonoBehaviour {

    public Transform player;
    public float fallingSpeed = 600f;
    public int  triggerDistance = 13;

	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z - player.position.z <= triggerDistance)
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
