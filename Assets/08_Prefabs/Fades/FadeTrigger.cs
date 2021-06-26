using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{


   
    
    Animation FadeOutAnimation;
    Animation FadeInAnimation;

    public bool Fadeout;
    public bool Fadein;

    private void Start()
    {
       
        FadeOutAnimation = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<Animation>();
        FadeInAnimation = GameObject.FindGameObjectWithTag("SceneFadeIn").GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Fadeout)
            {
                FadeOutAnimation.Play();
                gameObject.GetComponent<Collider>().enabled = false;
                GameMaster.TriggerWin();
            }
            if (Fadein)
            {
                FadeInAnimation.Play();
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
       
       
    }

}
