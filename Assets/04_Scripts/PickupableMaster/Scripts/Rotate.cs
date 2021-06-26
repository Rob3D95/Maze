using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    Transform transform;

    [SerializeField]
    float rotationSpeed = 20;
    private void Start()
    {
        transform = gameObject.GetComponent<Transform>();
       
    }

    private void Update()
    {

       

        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        
    }

}

