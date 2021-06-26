using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    PlayerScript playerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = new PlayerScript();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript != null)
        {
            if(playerScript.boolean == true)
            {
                Animation.setBool("animationbool", true);

            }
        } else if(playerScript == null)
        {
            Debug.Log("Playerscript is null");
        }
    }
}
