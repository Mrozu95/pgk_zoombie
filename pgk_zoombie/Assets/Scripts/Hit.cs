using UnityEngine;
using System.Collections;

    class Hit : MonoBehaviour
    {

        Animator anim;
        public Rigidbody player;
        float tempHealth = 100;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (tempHealth != Health.currentHealth)
            {
                
            anim.SetTrigger("Hit");
            tempHealth = Health.currentHealth;
            }
        }
    }

