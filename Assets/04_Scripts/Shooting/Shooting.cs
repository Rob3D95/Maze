using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    Transform barrelEnd;

    [SerializeField]
    AudioClip shootSound;
    AudioSource source;

    
  
    private void Start()
    {
        barrelEnd = GameObject.FindGameObjectWithTag("BarrelEnd").transform;
        source = gameObject.GetComponent<AudioSource>();
       
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (PickupableMaster.master.ammo > 0)
        {
            PickupableMaster.master.ammo--;
            source.PlayOneShot(shootSound);
            
            GameObject bulletObject = Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation * Quaternion.Euler(0,90,0)); ;
            bulletObject.transform.position = barrelEnd.position;
            
           
        }
    }
}
