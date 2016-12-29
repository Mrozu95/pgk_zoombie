using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Lvl_1_player_movment : MonoBehaviour {
    
    
    /*private float max_speed; STARY MOVMENT
    private int horizontal_speed;
    private int vertical_speed;*/

    public Rigidbody rb;
    private Vector3 movementDirection;

    public int speed;
    public float Jump; // wysokosc skoku
    bool inAir;
    bool canBeHitted; // czy mozna nas uderzyc
    bool slowed; // zmienna do spowalniaczy/wody
    public static bool sterowanie = true; // zmienna do zmiany sterowania dla tru sztrzalki dla false wsad

    Animator anim;


    public Image damageImage; // do migniecia po uderzeniu
    public float flashSpeed = 5.0f;
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);

    public Image SkillShadeMiddle;
    public Image SkillShadeRight;
    public Image SkillShadeLeft;
    public Color flashColour2 = new Color(0.5f, 0.5f, 0.5f, 0.4f);
    public Color flashColour3 = new Color(1.0f, 1.0f, 1.0f, 0.01f);


    public Text countText;
    public Text speedText;
    public Slider slider;

    public static int coins_count;

    public GameObject summon;
    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;

    public GameObject truck2;
    public Transform truckSpawn;

    // Dodanie monety
    public void SetCountText()
    {
        countText.text = "Monety : " + coins_count.ToString();
    }

    public void MapCoveredText()
    {
        double distanceCovered = System.Math.Round(rb.position.z / 434 * 100);
        if (distanceCovered < 0)
        {
            distanceCovered = 0;
        }
        else if (distanceCovered > 100)
        {
            distanceCovered = 100;
        }
        speedText.text = "Map Covered: " + distanceCovered + "%";
        
        
    }

    public void SetSlider()
    {
        slider.value = Health.currentHealth;
    }

    // Use this for initialization
    void Start()
    {
        speed = 10;
       
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        coins_count = 0; // początkowa ilośc monetek
        SetCountText();
        MapCoveredText();
        SetSlider();
        
        Jump = 400.0f; // wysokosc skoku
        inAir = false; // czy kulka jest w podskoku, zmienna zeby nie mozna bylo podskakiwac bedac w skoku
        canBeHitted = true;
        slowed = false;
        movementDirection = new Vector3();
        


         /*max_speed = 20; STARY MOVMENT
        horizontal_speed = 20; // stale do movment, zwieksza plynnosc
        vertical_speed = 5; // stale do movment, zwieksza plynnosc*/
    }

    // Update is called once per frame
    void Update()
    {
        isitinAir();
        CheckButtons();

        if (Health.currentHealth > 0 || rb.position.z < 430.5)
        {
            movement1();
            MapCoveredText();
            SetCountText();
            SetSlider();
            SkillsAnimations();
            DeadWhenFall();
        }

        if(inAir == false)
        {
            rb.velocity = rb.velocity * 0.0f;
        }

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // po hicie zmiana z ekranu czerwonego na odmyslny
        

    }

    //sterowanie graczem
    /*void movement2()
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
    }*/

    void movement1() //stabilniejsze sterowanie
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(slowed==false)
        {
            movementDirection.Set(moveHorizontal, 0, moveVertical);
            movementDirection = movementDirection * 20 * Time.deltaTime; //20 - szybkosc
            rb.MovePosition(transform.position + movementDirection);
        }
        else
        {
            movementDirection.Set(moveHorizontal, 0, moveVertical);
            movementDirection = movementDirection * 5 * Time.deltaTime; //20 - szybkosc
            rb.MovePosition(transform.position + movementDirection);
            slowed = false;
        }

        Animating(moveHorizontal, moveVertical);
       
    }

    public void CheckButtons()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inAir == false) // sprawdzanie przycisku spacji
        {
            jump();
        }

        if (sterowanie == true)
        {
            if (Input.GetKeyDown(KeyCode.Z) && coins_count >= 5) // sprawdzanie przycisku Z
            {
                teleport();
                coins_count = coins_count - 5;
            }

            if (Input.GetKeyDown(KeyCode.X) && coins_count >= 3) // sprawdzanie przycisku X
            {
                Push_back(50, 1000);
                coins_count -= 3;
            }

            if (Input.GetKeyDown(KeyCode.C) && coins_count >= 3) // sprawdzanie przycisku C
            {
                coins_count -= 3;
                StartCoroutine(invulnerable(3.0f));
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Comma) && coins_count >= 5) // sprawdzanie przycisku Z
            {
                teleport();
                coins_count = coins_count - 5;
            }

            if (Input.GetKeyDown(KeyCode.Period) && coins_count >= 3) // sprawdzanie przycisku X
            {
                Push_back(50, 1000);
                coins_count -= 3;
            }

            if (Input.GetKeyDown(KeyCode.Slash) && coins_count >= 3) // sprawdzanie przycisku C
            {
                coins_count -= 3;
                StartCoroutine(invulnerable(3.0f));
            }
        }

        
       
    }


    public void SkillsAnimations() // przyciemnianie i rozjasnainei obrazkow ze skillami
    {
        if (coins_count < 3)
        {
            SkillShadeMiddle.color = flashColour2;
            SkillShadeRight.color = flashColour2;

        }
        else
        {
            SkillShadeMiddle.color = flashColour3;
            SkillShadeRight.color = flashColour3;
        }
        if (coins_count < 5)
        {
            SkillShadeLeft.color = flashColour2;

        }
        else
        {
            SkillShadeLeft.color = flashColour3;
        }
    }

    public void DeadWhenFall()
    {
        if(rb.position.y < 37.0)
        {
            Health.subtractHealth(100);
        }
    }


    public void spawn()
    {
        Instantiate(summon, new Vector3(spawnPoint.position.x + 15, spawnPoint.position.y, spawnPoint.position.z), spawnPoint.rotation);
    }

    public void spawn2()
    {
        Instantiate(summon, new Vector3(spawnPoint1.position.x, spawnPoint1.position.y, spawnPoint1.position.z), spawnPoint1.rotation);
        Instantiate(summon, new Vector3(spawnPoint2.position.x, spawnPoint2.position.y, spawnPoint2.position.z), spawnPoint2.rotation);
        
    }
    public void spawn3()
    {
        Instantiate(summon, new Vector3(spawnPoint3.position.x, spawnPoint3.position.y, spawnPoint3.position.z), spawnPoint3.rotation);
        Instantiate(summon, new Vector3(spawnPoint4.position.x, spawnPoint4.position.y, spawnPoint4.position.z), spawnPoint4.rotation);

    }

    public void spawnTruck2()
    {
        Instantiate(truck2, new Vector3(truckSpawn.position.x, truckSpawn.position.y, truckSpawn.position.z), truckSpawn.rotation);
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
        if (other.gameObject.CompareTag("Destructible_wall"))
        {
            other.gameObject.SetActive(false);
            //coins_count++;
        }
        if (other.gameObject.CompareTag("Spawn"))
        {
            for(int i=0; i<100; i++)
            {
                spawn();
            }
            spawnTruck2();
        }
        if (other.gameObject.CompareTag("Spawn1"))
        {
            for (int i = 0; i < 100; i++)
            {
                spawn2();
                other.enabled = false;
            }
        }
        if (other.gameObject.CompareTag("Spawn2"))
        {
            for (int i = 0; i < 10; i++)
            {
                spawn3();
            }
            spawnTruck2();
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
                damageImage.color = flashColour;
            }
            if (collision.gameObject.CompareTag("Truck"))
            {
                Health.subtractHealth(100);
                
            }
        }
    }

    // woda - spowalniacz  
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {

            slowed = true;

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
    public IEnumerator invulnerable(float x_seconds)
    {
        canBeHitted = false;
        yield return new WaitForSeconds(x_seconds);
        canBeHitted = true;
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool running = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsRunning", running);
    }
}
