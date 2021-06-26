using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemiesMovement : MonoBehaviour
{
    public float pointRadius, pointTimer;

    public bool DrawGizmos;

   
    NavMeshAgent agent;

    float timer;

    Vector3 newPos;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = pointTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, newPos);
        

        if(timer >= pointTimer || distance < 1.1f)
        {
            newPos = RandomNavSphere(transform.position, pointRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.DrawWireSphere(newPos, 1);
        }
    }
}
