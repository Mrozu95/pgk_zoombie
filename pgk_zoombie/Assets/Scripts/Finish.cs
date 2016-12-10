using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

    Animator anim;
    public Rigidbody player;
    public float restartDelay = 5f;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > 430.5)
        {
            anim.SetTrigger("Finish");
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                Application.LoadLevel("MainMenu");
            }

        }
    }
}
