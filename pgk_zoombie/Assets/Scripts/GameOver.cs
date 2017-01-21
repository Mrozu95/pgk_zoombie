using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    float restartDelay = 30f;

    Animator anim;
    float restartTimer;
    
	void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Health.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
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
