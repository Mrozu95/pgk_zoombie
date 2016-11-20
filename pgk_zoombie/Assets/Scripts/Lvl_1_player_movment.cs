using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Lvl_1_player_movment : MonoBehaviour {


    public Rigidbody rb;
    private Vector3 movementDirection;

    public int speed;
    private float max_speed;
    private int horizontal_speed;
    private int vertical_speed;
    public float Jump; // wysokosc skoku
    bool inAir;
    bool canBeHitted; // czy mozna nas uderzyc

    public Text countText;
    public Text speedText;
    public Slider slider;

    public int coins_count;

    public GameObject summon;
    public Transform spawnPoint;


    // Dodanie monety
    public void SetCountText()
    {
        countText.text = "Monety : " + coins_count.ToString();
    }

    public void SetSpeedText()
    {
        speedText.text = "Speed : " + rb.velocity.ToString();
    }

    public void SetSlider()
    {
        slider.value = Health.currentHealth;
    }

    // Use this for initialization
    void Start()
    {
        speed = 10;
        max_speed = 20;
        rb = GetComponent<Rigidbody>();
        coins_count = 0; // początkowa ilośc monetek
        SetCountText();
        SetSpeedText();
        SetSlider();
        horizontal_speed = 20; // stale do movment, zwieksza plynnosc
        vertical_speed = 5; // stale do movment, zwieksza plynnosc
        Jump = 400.0f; // wysokosc skoku
        inAir = false; // czy kulka jest w podskoku, zmienna zeby nie mozna bylo podskakiwac bedac w skoku
        canBeHitted = false;
        movementDirection = new Vector3();
        
    }

    // Update is called once per frame
    void Update()
    {
        isitinAir();
        if (Input.GetKeyDown(KeyCode.Space) && inAir == false) // sprawdzanie przycisku spacji
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.Z) && coins_count >= 5) // sprawdzanie przycisku Z
        {
            teleport();
            coins_count = coins_count - 5;
        }

        if (Input.GetKeyDown(KeyCode.X) && coins_count >= 3) // sprawdzanie przycisku X
        {
            Push_back(50,1000);
            coins_count -= 3;
        }

        if (Input.GetKeyDown(KeyCode.C) && coins_count >= 3) // sprawdzanie przycisku C
        {
           coins_count -= 3;
           StartCoroutine(invulnerable());
        }

        if(Health.currentHealth > 0 || rb.position.z < 1070)
        {
            movement1();
            SetSpeedText();
            SetCountText();
            SetSlider();
        }

        
    }

    //sterowanie graczem
    void movement2()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * horizontal_speed;
        float moveVertical = Input.GetAxis("Vertical") * vertical_speed;

        movementDirection.Set(moveHorizontal, 0, moveVertical);

        //maksymalna szybkosc
        if (rb.velocity.magnitude > max_speed)
        {
            rb.velocity = rb.velocity.normalized * max_speed;
        }

        if (rb.velocity.magnitude > max_speed / 2.0f)
        {
            rb.AddForce(movementDirection * 2.5f);
        }
        else
        {
            rb.AddForce(movementDirection * speed);
        }
    }

    void movement1() //stabilniejsze sterowanie
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        movementDirection.Set(moveHorizontal, 0, moveVertical);
        movementDirection = movementDirection.normalized * 20 * Time.deltaTime; //20 - szybkosc
        rb.MovePosition(transform.position + movementDirection);
    }




    public void spawn()
    {
        Instantiate(summon, spawnPoint.position, spawnPoint.rotation);
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
    // spawn zombie
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coins_count++;
        }
        if (other.gameObject.CompareTag("Spawn"))
        {
            for(int i=0; i<5; i++)
            {
                spawn();
            }                      
        }
      
    }

    // kontakt z zombie zmniejsza pasek życia
    public void OnCollisionEnter(Collision collision)
    {
        if (canBeHitted)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                Health.subtractHealth(10);
            }
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

    
   

    //odepchniecie. Działa całkiem fajnie, ewentualnie dostosować promień i moc
    public void Push_back(float radius, float power)
    {
        

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Zombie"))
            {
                Rigidbody rb2 = hit.GetComponent<Rigidbody>();

                if (rb2 != null)
                    rb2.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }

        }
    }

    // niewrazliwosc na x sekund
    public IEnumerator invulnerable()
    {
        float x_seconds = 3.0f;

        canBeHitted = false;
        yield return new WaitForSeconds(x_seconds);
        canBeHitted = true;
    }

}
