using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
[RequireComponent(typeof(AudioSource))]
public class PickupableAgent : MonoBehaviour
{
    [SerializeField]
    PickupableMaster.pickupType type;

    [SerializeField]
    int itemValue;

 
   

    AudioSource audioSource;
    AudioClip sound;
    private void Start()
    {
       
        audioSource = gameObject.GetComponent<AudioSource>();
       
    }

    private void OnEnable()
    {
        PickupableMaster.OnItemPickUp += OnPickup;
        PickupableMaster.Pickupables.Add(this);
    }
     public void OnTriggerEnter(Collider other)
    {



        if (other.tag == "Player")
        {
            if (this.type != PickupableMaster.pickupType.heart)
            {
                PickupableMaster.master.PickedUpCoin(this, type, itemValue, sound);
            }
            else if (this.type == PickupableMaster.pickupType.heart && EnemiesMaster.master.currentHealth < 100)
            {
                PickupableMaster.master.PickedUpCoin(this, type, itemValue, sound);
            }
        }   
    }


    private void OnPickup(PickupableAgent agent, int coins, int value, AudioClip sound)
    {
        if(PickupableMaster.master.DebugMessages) { Debug.Log("Coin Picked Up"); }

       
        audioSource.PlayOneShot(sound, 0.1f); ;
        if (agent == this)
        {
            
            Destroy(this.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        PickupableMaster.Pickupables.Remove(this);
        PickupableMaster.OnItemPickUp -= OnPickup;
    }

    private void OnDisable()
    {
        PickupableMaster.OnItemPickUp -= OnPickup;
        PickupableMaster.Pickupables.Remove(this);
    }




}
