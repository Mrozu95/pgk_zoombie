using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Lvl_1_player_movment : MonoBehaviour {

    Rigidbody rb;
    public int speed;
    public Text countText;
    public Text speedText;
    public int count;

    private float max_speed;
    private int horizontal_speed;
    private int vertical_speed;


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
        speed = 10;
        max_speed = 20;
        rb = GetComponent<Rigidbody>();
        count = 0; // początkowa ilośc monetek
        SetCountText();
        SetSpeedText();
        horizontal_speed = 7;
        vertical_speed = 5;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        movment();
        SetSpeedText();
       
    }


    //sterowanie graczem
    void movment()
    {
        float moveHorizontal = Input.GetAxis("Horizontal")* horizontal_speed;
        float moveVertical = Input.GetAxis("Vertical") * vertical_speed;

        Vector3 movment = new Vector3(moveHorizontal, 0, moveVertical);

        //maksymalna szybkosc
        if(rb.velocity.magnitude > max_speed)
        {
            rb.velocity = rb.velocity.normalized* max_speed;
        }

        rb.AddForce(movment * speed);
        
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
            rb.velocity = rb.velocity * 0.775f;
        }
           
    }
}
