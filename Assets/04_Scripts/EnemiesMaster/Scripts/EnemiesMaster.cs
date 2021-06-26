using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[ExecuteAlways]
public class EnemiesMaster : MonoBehaviour
{
    public static EnemiesMaster master;
    public delegate void EnemiesLogic();

    public bool DebugMessages = false;
    public bool ShowConnections = false;

    public static List<EnemiesAgent> Enemies = new List<EnemiesAgent>();

    public int currentHealth = 100;

    Color lineColor = Color.blue;
    

    private void Awake()
    {
        if (master == null)
        {
            master = this;
        }
        else
        {
            if (DebugMessages) Debug.LogWarning("Multiple Pickupable Masters detected, deleting" + this.gameObject.name);
            Destroy(this);
        }
    }
    


    public static Action<EnemiesAgent, int, int> OnEnemyCollide;
    public void Collide(EnemiesAgent agent, int damage, int health)
    {
        if (OnEnemyCollide != null)
        {
            health = currentHealth;
            currentHealth -= damage;


            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameMaster.TriggerDefeat();

            }

            if(currentHealth >= 100)
            {
                currentHealth = 100;
            }
            OnEnemyCollide(agent, damage, currentHealth);
            PickupableMaster.HealthBarUpdate(currentHealth);
            if (DebugMessages) Debug.Log("Collided with enemy");
        }
        

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowConnections)
            foreach (EnemiesAgent agent in Enemies)
            {
                Vector3 managerPos = transform.position;
                Vector3 barrelPos = agent.transform.position;

                float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;

                Vector3 offset = Vector3.up * halfHeight;
                if (agent.tag == "Ammo")
                {
                    lineColor = Color.red;
                }

                if (agent.tag == "Coin")
                {
                    lineColor = Color.yellow;
                }
                Handles.DrawBezier(
                    managerPos,
                    barrelPos,
                    managerPos - offset,
                    barrelPos + offset,

                    lineColor,
                    EditorGUIUtility.whiteTexture,
                    1f
                    );
            }
    }
#endif
}
