using UnityEngine;
using System.Collections;

public class Medkit_script : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Medkit"))
        {
            other.gameObject.SetActive(false);
            Health.addHealth(20);
        }
    }
}
