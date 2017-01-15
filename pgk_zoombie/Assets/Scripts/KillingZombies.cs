using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillingZombies : MonoBehaviour {

    public Text killedZombiestext;
    private int zombiesAmount;


    // Use this for initialization
    void Start()
    {
        killedZombiestext.enabled = true;
        zombiesAmount = transform.childCount;
        killedZombiestext.text = "Pozostało " + transform.childCount + " ZOMBIE!";
        StartCoroutine(wait(2));
    }

    // Update is called once per frame
    void Update()
    {
        if (zombiesAmount > transform.childCount)
        {
            if (killedZombiestext.enabled == true)
            {
                killedZombiestext.enabled = false;
            }
            killedZombiestext.enabled = true;
            killedZombiestext.text = "Pozostało " + transform.childCount + " ZOMBIE!";
            zombiesAmount = transform.childCount;
            StartCoroutine(wait(2));
        }
        else if (zombiesAmount < transform.childCount)
        {
            if (killedZombiestext.enabled == true)
            {
                killedZombiestext.enabled = false;
            }
            killedZombiestext.enabled = true;
            killedZombiestext.text = "NOWE ZOMBIE /n Pozostało " + transform.childCount + " ZOMBIE!";
            zombiesAmount = transform.childCount;
            StartCoroutine(wait(2));
        }
    }

    public IEnumerator wait(float x_seconds)
    {
        yield return new WaitForSeconds(x_seconds);
        killedZombiestext.enabled = false;
    }

}

