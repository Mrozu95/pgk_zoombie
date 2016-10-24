﻿using UnityEngine;
using System.Collections;

public class Zoombie_movment : MonoBehaviour {

    Rigidbody rb;
    public Transform player;
    private float zoombie_speed; //pseudo szybkosc, jest to maksymalna różnica odległości od gracza, ale im da sie wieksza to poruszaja sie szybciej
    private Vector3 direction;

    // Use this for initialization
    void Start () {
        //rb = new Rigidbody();
        rb = GetComponent<Rigidbody>(); // tak dziala nasz movment z new rigidbody nie dzialalo
        zoombie_speed = 0.15f;
    }
	
	// Update is called once per frame
	void Update () {
        movment();
	}

    //pogon za graczem
    public void movment()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, zoombie_speed);
    }

    //metoda nie dokonczona pracuje nad tym, alternatywny sposob poruszania sie zoombie
    public void movment2()
    {
        direction = player.transform.position;
        rb.AddForce(direction * 10);
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
}
