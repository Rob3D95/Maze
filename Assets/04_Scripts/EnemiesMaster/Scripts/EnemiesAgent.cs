using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnemiesAgent : MonoBehaviour
{
    [SerializeField]
    int damage = 35;

    int enemyHealth = 50;

    [SerializeField]
    int bulletDamage = 10;

    [SerializeField]
    AudioClip bulletHit;

    [SerializeField]
    AudioClip enemyDying;

    AudioSource source;
    [SerializeField]
    AudioClip ouchSound;

    private void OnEnable()
    {
        EnemiesMaster.Enemies.Add(this);
        EnemiesMaster.OnEnemyCollide += EnemyHealth;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }



    private void OnDisable()
    {
        EnemiesMaster.Enemies.Remove(this);
        EnemiesMaster.OnEnemyCollide -= EnemyHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemiesMaster.master.Collide(this, damage, 0);
            source.PlayOneShot(ouchSound);
        }
        
        if(other.tag == "Bullet")
        {
            if(source != null)
            {
                source.PlayOneShot(bulletHit);
            }
           EnemyHealth(this, 0, enemyHealth);
           Destroy(other);
           
        }
    }

    void EnemyHealth(EnemiesAgent agent, int damage, int health)
    {
        enemyHealth -= bulletDamage;
        //Debug.Log(enemyHealth);

        if(enemyHealth < 0)
        {
            
            if (source != null)
            {
                source.PlayOneShot(enemyDying);
            }
            Destroy(this.gameObject);
        }
    }

  
}
