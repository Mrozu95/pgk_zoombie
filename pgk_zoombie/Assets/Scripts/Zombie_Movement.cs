using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Zombie_Movement : MonoBehaviour {

    Rigidbody rb;
    public Transform player;
    public GameObject ja;
    private float zoombie_speed; //pseudo szybkosc, jest to maksymalna różnica odległości od gracza, ale im da sie wieksza to poruszaja sie szybciej
    private Vector3 direction;
    private bool stop;
    NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        //rb = new Rigidbody();
        rb = GetComponent<Rigidbody>(); // tak dziala nasz movment z new rigidbody nie dzialalo
        zoombie_speed = 0.14f;
        stop = false;
        UIManager.pauseState = false;
        agent = GetComponent<NavMeshAgent>();
        
        //  transform.position = new Vector3(this.transform.position.x, transform.position.y, transform.position.z + 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (stop == false)
        //{
        //    movment2();
        //}
        //else if (stop == true && UIManager.pauseState == false)
        //{
        //    this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 0.07f);
        //    haltnichtsitzen();
        //}
        movment2();

        zombieRotation();
    }

    //pausa po hicie
    public IEnumerator haltnichtsitzen()
    {
        float x_seconds = 0.2f;

        yield return new WaitForSeconds(x_seconds);
        stop = false;
    }

    //pogon za graczem
    public void movment()
    {
        if(UIManager.pauseState == false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, zoombie_speed);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 0.0f);
        }
    }

    //metoda nie dokonczona pracuje nad tym, alternatywny sposob poruszania sie zoombie
    public void movment2()
    {
        agent.SetDestination(player.position);       
    }

    private void zombieRotation()
    {
        Vector3 relativePos = player.position - transform.position;
        relativePos = new Vector3(relativePos.x, 0, relativePos.z);
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }

    //teoretycznie woda tez powinna spowalniac zoombie ale przy uzyciu metody movment() nie zabardzo sie to chyba sprawdza dlatego pracuje nad movment2()
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            //zoombie_speed *= 0.5f; 
            rb.velocity = rb.velocity * 0.895f; // woda spowalnia zombiakow troche wolniej, zeby zmusic przeciwnikow do unikania jej
        }
    }

    // zombi zatrzymuje sie po hicie
    public bool OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stop = true;
        }
        if (collision.gameObject.CompareTag("Truck"))
        {
            killZombie();
        }
        if(collision.gameObject.CompareTag("KillingPlane"))
        {
            killZombie();
        }

        if (collision.gameObject.CompareTag("KillingTree"))
        {
            killZombie();
        }
        return stop;
    }

    private void killZombie()
    {
        Destroy(ja);
        //Lvl_1_player_movment.enableKilledZombiesText();
    }
}
