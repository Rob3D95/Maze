using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class GateAgent : MonoBehaviour
{
    [SerializeField]
    public int coinsNeeded;


    public Transform doorLeft, doorRight;

    public BoxCollider collider;

    AudioSource source;

    

    private void Start()
    {
       

        collider = gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponentInChildren<Animation>();

        doorRight = gameObject.transform.GetChild(0).GetChild(0);
        doorLeft = gameObject.transform.GetChild(0).GetChild(1);

        source = GetComponent<AudioSource>();
      
        
    }
    private void OnEnable()
    {
        
        GateMaster.Gates.Add(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            GateMaster.master.TriggerEnter(this, coinsNeeded, source);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GateMaster.master.TriggerExit(this);
        }
    }


    private void OnDisable()
    {
       
        GateMaster.Gates.Remove(this);
    }

    
}
