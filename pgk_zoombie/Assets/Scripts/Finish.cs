using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

    Animator anim;
    public Rigidbody player;
    float restartDelay = 30f;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > 470.5)
        {
            anim.SetTrigger("Finish");
            restartTimer += Time.deltaTime;

            if (Input.GetKeyDown("return"))
                restartTimer = 30f;

            if (restartTimer >= restartDelay)
            {
                Application.LoadLevel("MainMenu");
            }

        }
    }
}
