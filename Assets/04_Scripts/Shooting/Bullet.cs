using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
   

    [SerializeField]
    float lifeDuration = 3f;

    float lifeTime;

    [SerializeField]
    float speed = 8f;

   

    private void Start()
    {
        lifeTime = lifeDuration;
   
    }
    private void Update()
    {
       
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0f)
        {
            Destroy(gameObject);
        }
    }
    
}
