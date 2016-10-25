using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Lvl_1_player_movment : MonoBehaviour {

    Rigidbody rb;
    public int speed;
    public int health;
    public Text countText;
    public Text speedText;
    public int count;

    private float max_speed;
    private int horizontal_speed;
    private int vertical_speed;
    public float Jump; // wysokosc skoku
    bool inAir;

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
        health = Health.currentHealth;
        rb = GetComponent<Rigidbody>();
        count = 0; // początkowa ilośc monetek
        SetCountText();
        SetSpeedText();
        horizontal_speed = 20; // stale do movment, zwieksza plynnosc
        vertical_speed = 5; // stale do movment, zwieksza plynnosc
        Jump = 400.0f; // wysokosc skoku
        inAir = false; // czy kulka jest w podskoku, zmienna zeby nie mozna bylo podskakiwac bedac w skoku

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        isitinAir();
        if (Input.GetKeyDown(KeyCode.Space) && inAir == false) // sprawdzanie przycisku spacji
        {
            jump();
        }


        if (Input.GetKeyDown(KeyCode.Z) && count >= 5) // sprawdzanie przycisku spacji
        {
            teleport();
            count = count - 5;
        }

        movment();
        SetSpeedText();
        SetCountText();
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

        if (rb.velocity.magnitude > max_speed / 2.0f)
        {
            rb.AddForce(movment * 2.5f);
        }
        else
        {
             rb.AddForce(movment * speed);
        }
       
        
    }

    //skakanie 
    void jump()
    {
        rb.AddForce(Vector3.up * Jump);
    }
    void isitinAir()
    {
        if (rb.velocity.y < 0.17 && rb.velocity.y > -0.17)
        {
            inAir = false;
        }
        else
        {
            inAir = true;
        }
    }

    void teleport()
    {
        this.transform.position = new Vector3(rb.position.x, rb.position.y, rb.position.z + 30.0f);       
    }

    //znikanie monet
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count++;
        }
        
    }

    //jeszcze nie odejmuje HP
    public void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Health.subtractHealth(10);
        }
    }

    // woda - spowalniacz
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            rb.velocity = rb.velocity * 0.885f;
        }
           
    }
}
