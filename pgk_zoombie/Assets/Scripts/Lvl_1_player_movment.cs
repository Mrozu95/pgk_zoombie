using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lvl_1_player_movment : MonoBehaviour {

    /*private float max_speed; STARY MOVMENT
    private int horizontal_speed;
    private int vertical_speed;*/

    public Rigidbody rb;
    private Vector3 movementDirection;

    public float speed = 150f;
    public float Jump; // wysokosc skoku
    bool inAir;
    public static bool canBeHitted; // czy mozna nas uderzyc
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
    public Slider sliderMap;

    public static int coins_count;
    public static int coins_count_all;

    public GameObject summon;
    public Transform summonTransform;
    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;
    public Transform spawnPoint9;
    public Transform spawnPoint10;
    public Transform spawnPoint11;
    public Transform spawnPoint12;
    public Transform spawnPoint13;

    public GameObject truck2;
    public Transform truckSpawn;

    public float deathFallSpeed;
    public bool canMove;
    CapsuleCollider playerCollider;

    public Text diamonds;
    public Text zombies;
    public Text time;
    public Text distance;

    string timeSinceLevelStarted;

    public double distanceCovered;
    bool countTime;
    bool finished;
    int zombiesCount;

    // Dodanie monety
    public void SetCountText()
    {
        countText.text = coins_count.ToString();
    }

    public void MapCoveredText()
    {
        distanceCovered = System.Math.Round(rb.position.z / 470 * 100);
        if (distanceCovered < 0)
        {
            distanceCovered = 0;
        }
        else if (distanceCovered > 100)
        {
            distanceCovered = 100;
        }
        //speedText.text = "Map Covered: " + distanceCovered + "%";
        sliderMap.value = (float)distanceCovered;    
    }

    public void SetSlider()
    {
        slider.value = Health.currentHealth;
    }

    public void SetStatisticsText()
    {
        diamonds.text = "Diamonds collected : " + coins_count_all.ToString();
        zombies.text = "Zombie slained : " + zombiesCount; 
        distance.text = "Distance covered : " + distanceCovered.ToString() + "%";
        time.text = "Time passed : " + timeSinceLevelStarted + " s";
    }

    // Use this for initialization
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        coins_count = 0; // początkowa ilośc monetek
        coins_count_all = 0;
        SetCountText();
        MapCoveredText();
        SetSlider();
        
        Jump = 800.0f; // wysokosc skoku
        inAir = false; // czy kulka jest w podskoku, zmienna zeby nie mozna bylo podskakiwac bedac w skoku
        canBeHitted = true;
        slowed = false;
        movementDirection = new Vector3();
        canMove = true;

        SetStatisticsText();
        countTime = true;
        finished = false;

        /*max_speed = 20; STARY MOVMENT
       horizontal_speed = 20; // stale do movment, zwieksza plynnosc
       vertical_speed = 5; // stale do movment, zwieksza plynnosc*/
    }

    // Update is called once per frame
    void Update()
    {
        isitinAir();
        CheckButtons();

        if (Health.currentHealth > 0 || rb.position.z < 470.5)
        {
            movement1();
            MapCoveredText();
            SetCountText();
            SetSlider();
            SkillsAnimations();
            DeadWhenFall();
            SetStatisticsText();
        }

        if (Health.currentHealth <= 0)
        {
            countTime = false;
            canMove = false;
            anim.SetBool("IsRunning", false);
            if (this.transform.eulerAngles.x <= 75)
                transform.Rotate(Vector3.right * deathFallSpeed * Time.deltaTime);
        }

        if(countTime == true && finished == false)
        {
            timeSinceLevelStarted = timeSinceLevelStarted = System.Math.Round(Time.timeSinceLevelLoad,2).ToString();
            zombiesCount = KillingZombies.licznik;
        }

        if (inAir == false)
        {
            rb.velocity = rb.velocity * 0.0f;
        }

        if(rb.position.z > 470.5)
        {
            finished = true;
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
        if(canMove == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (slowed == false)
            {
                movementDirection.Set(moveHorizontal, 0, moveVertical);
                movementDirection = movementDirection * 10 * Time.deltaTime; //20 - szybkosc
                
                if (this.transform.position.z <= -10)
                {
                    movementDirection.Set(0, 0, 0.01f);
                }
                if (this.transform.position.x <= -12)
                {
                    movementDirection.Set(0.01f, 0, 0);
                }
                if (this.transform.position.x >= 12)
                {
                    movementDirection.Set(-0.01f, 0, 0);
                }
                rb.MovePosition(transform.position + movementDirection);
            }
            else
            {
                movementDirection.Set(moveHorizontal, 0, moveVertical);
                movementDirection = movementDirection * 3 * Time.deltaTime; //20 - szybkosc

                if (this.transform.position.z <= -10)
                {
                    movementDirection.Set(0, 0, 0.01f);
                }
                if (this.transform.position.x <= -12)
                {
                    movementDirection.Set(0.01f, 0, 0);
                }
                if (this.transform.position.x >= 12)
                {
                    movementDirection.Set(-0.01f, 0, 0);
                }
                rb.MovePosition(transform.position + movementDirection);
                slowed = false;
            }

            Animating(moveHorizontal, moveVertical);

            playerRotation(moveHorizontal, moveVertical); // obrot gracza 
        }
    }

    private void playerRotation(float moveHorizontal, float moveVertical)
    {
        if (moveVertical > 0 && moveHorizontal > 0) // przod prawo
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (moveVertical > 0 && moveHorizontal < 0) //przod lewo
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (moveVertical < 0 && moveHorizontal > 0) //tyl prawo
        {
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (moveVertical < 0 && moveHorizontal < 0) //tyl lewo
        {
            transform.rotation = Quaternion.Euler(0, -135, 0);
        }
        else if (moveVertical > 0) //przod
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(moveVertical < 0) // tyl
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveHorizontal < 0) // lewo
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (moveHorizontal > 0) //prawo
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
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
                StartCoroutine(teleport());
                coins_count = coins_count - 5;
            }

            if (Input.GetKeyDown(KeyCode.X) && coins_count >= 3) // sprawdzanie przycisku X
            {
                Push_back(50, 50);
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
                StartCoroutine(teleport());
                coins_count = coins_count - 5;
            }

            if (Input.GetKeyDown(KeyCode.Period) && coins_count >= 3) // sprawdzanie przycisku X
            {
                Push_back(50, 50);
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
        if(rb.position.y < 27.0)
        {
            Health.subtractHealth(200);
        }
        if (rb.position.x > -27.0 || rb.position.x < -37.0)
        {
            Health.subtractHealth(200);
        }
    }

    public void spawn(Transform spawnPoint, float distance)
    {
        Instantiate(summon, new Vector3(spawnPoint.position.x + distance, spawnPoint.position.y, spawnPoint.position.z + distance/2), spawnPoint.rotation, summonTransform);
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

    IEnumerator teleport()
    {
        yield return new WaitForSeconds(1.0f);
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
            coins_count_all++;
        }
        if (other.gameObject.CompareTag("Destructible_wall"))
        {
            other.gameObject.SetActive(false);
            //coins_count++;
        }
        if (other.gameObject.CompareTag("Spawn"))
        {
            for(int i=0; i<5; i++)
            {
                spawn(spawnPoint ,i * 2);
                spawn(spawnPoint6, i * 2);          
            }

            for (int i = 0; i < 10; i++)
            {
                spawn(spawnPoint11, i * 2);
                spawn(spawnPoint12, i * 2);
            }
            //other.enabled = false;
        }
        if (other.gameObject.CompareTag("Spawn1"))
        {
            for (int i = 0; i < 3; i++)
            {
                spawn(spawnPoint1, i * 3);
                //other.enabled = false;
            }
            for (int i = 0; i < 15; i++)
            {
                spawn(spawnPoint2, i * 1);
                //other.enabled = false;
            }
        }
        if (other.gameObject.CompareTag("Spawn2"))
        {
            for (int i = 0; i < 3; i++)
            {
                spawn(spawnPoint3, i * 3);
               // spawn(spawnPoint4, i * 3);
                spawn(spawnPoint5, i * 3);
                spawn(spawnPoint10, i * 3);
            }

            for(int i = 0; i <20; i++)
            {
                spawn(spawnPoint4, i + 1);
            }
            
            //other.enabled = false;
        }
        if (other.gameObject.CompareTag("Spawn4"))
        {
            for (int i = 0; i < 3; i++)
            {
                spawn(spawnPoint9, i * 2);
            }

            for (int i = 0; i < 10; i++)
            {
                spawn(spawnPoint13, i * 1);
            }

            //other.enabled = false;
        }
    }

    // kontakt z zombie zmniejsza pasek życia
    public void OnCollisionEnter(Collision collision)
    {
        if (canBeHitted)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                damageImage.color = flashColour;
            }
            if (collision.gameObject.CompareTag("Truck"))
            {
                Health.subtractHealth(200);
            }

            if (collision.gameObject.CompareTag("KillingTree"))
            {
                Health.subtractHealth(200);
            }

            if (collision.gameObject.CompareTag("KillingPlane"))
            {
                Health.subtractHealth(200);
                Destroy(playerCollider);
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
