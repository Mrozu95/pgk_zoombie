using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Lvl_1_player_movment : MonoBehaviour {

    Rigidbody rb;
    public float speed;
    public Text countText;
    public Text speedText;
    public int count;

    // Dodanie monety
    public void SetCountText()
    {
        countText.text = "Monety : " + count.ToString();
    }
    public void SetSpeedText()
    {
        speedText.text = "Speed : " + rb.velocity.ToString();
    }
    // Use this for initialization
    void Start ()
    {
        set_speed(10);
        rb = GetComponent<Rigidbody>();
        count = 0; // początkowa ilośc monetek
        SetCountText();
        SetSpeedText();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        movment();
        SetSpeedText();
       
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

        Vector3 compare = new Vector3(0.0f, 0.0f, 5.0f); // do stworzenia predksoci maxymalnej ale nie dziala to za bardzo
        

        if (compare.z - movment.z > 0)
        {
            rb.AddForce(movment * speed);
        }


        
    }
    //znikanie monet
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        
    }

    // woda - spowalniacz
   void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            //Vector3 temp = new Vector3(0.00f,0.00f,0.15f);
            rb.velocity = rb.velocity * 0.975f;
        }
           
    }
}
