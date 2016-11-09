using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

	void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Health.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}
}
