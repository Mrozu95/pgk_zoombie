using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public Transform target;
    private Transform myTransform;
    public Vector3 offsetToTarget = new Vector3(0.0f, 0.0f, 0.0f);

    Animator anim;
    int coins_count;

    void Start()
    {
        myTransform = transform;
        anim = GetComponent<Animator>();
        coins_count = Lvl_1_player_movment.coins_count;
    }

    void Update()
    {
        myTransform.position = target.position + offsetToTarget;
        coins_count = Lvl_1_player_movment.coins_count;
        teleporterControl();
    }

    public void teleporterControl()
    {
        if ((Input.GetKeyDown(KeyCode.Z) && coins_count >= 5) || (Input.GetKeyDown(KeyCode.Comma) && coins_count >= 5)) // sprawdzanie przycisku C
        {
            anim.SetTrigger("teleport");
        }
    }
}
