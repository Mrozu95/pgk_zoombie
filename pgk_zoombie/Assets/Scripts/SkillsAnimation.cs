using UnityEngine;
using System.Collections;

public class SkillsAnimation : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
	    if(Lvl_1_player_movment.coins_count >= 5)
        {
            anim.SetBool("Skill1", true);
            anim.SetBool("Skill23", true);
        }
        if(Lvl_1_player_movment.coins_count < 5 && Lvl_1_player_movment.coins_count >=3)
        {
            anim.SetBool("Skill1", false);
            anim.SetBool("Skill23", true);
        }
        if (Lvl_1_player_movment.coins_count < 3)
        {
            anim.SetBool("Skill23", false);
            anim.SetBool("Skill1", false);
        }
    }
}
