using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;


[ExecuteAlways]
public class PickupableMaster : MonoBehaviour
{
    public static PickupableMaster master;
    public delegate void ItemLogic();

    public static List<PickupableAgent> Pickupables = new List<PickupableAgent>();

    public bool DebugMessages = false;
    public bool ShowConnections = false;

    Color lineColor = Color.yellow;

    public int coins = 0;
    public int ammo = 20;

   TextMeshProUGUI CoinCounter;
   TextMeshProUGUI AmmoCounter;

    [SerializeField]
    AudioClip coinSound, ammoSound, heartSound;

    AudioClip Chosensound;


    public enum pickupType { coin, ammo, heart}


    private void Awake()
    {
        if (master == null)
        {
            master = this;
        } else
        {
            if (DebugMessages) Debug.LogWarning("Multiple Pickupable Masters detected, deleting" + this.gameObject.name);
            Destroy(this);
        }
    }

    private void Start()
    {
        CoinCounter = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<TextMeshProUGUI>();

        if(CoinCounter != null)
        {
            CoinCounter.SetText(coins.ToString());
        } else if (CoinCounter == null)
        {
            Debug.Log("CoinCounter is null");

            return;
        }
     

        AmmoCounter = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<TextMeshProUGUI>();

        if (AmmoCounter != null)
        {
            AmmoCounter.SetText(ammo.ToString());
        }
        else if (AmmoCounter == null)
        {
            Debug.Log("AmmoCounter is null");

            return;

        }
       
    }



    public static Action<PickupableAgent, int, int, AudioClip> OnItemPickUp;
    public static Action<int> HealthBarUpdate;
    public void PickedUpCoin(PickupableAgent agent, pickupType type, int value, AudioClip sound)
    {
       if(OnItemPickUp != null)
        {

            if (type == pickupType.ammo)
            {
                Chosensound = ammoSound;
            }
            else if (type == pickupType.coin)
            {
                Chosensound = coinSound;
            } else if(type == pickupType.heart)
            {
                Chosensound = heartSound;
            }

            
            
            OnItemPickUp(agent, coins, value, Chosensound);
            
            

            switch (type)
            {
                case pickupType.coin:
                    coins += value;
                    
                    break;
                case pickupType.ammo: 
                    ammo += value;

                    break;

                case pickupType.heart:
                    EnemiesMaster.master.currentHealth += value;
                    if(EnemiesMaster.master.currentHealth >= 100)
                    {
                        EnemiesMaster.master.currentHealth = 100;
                    }
                    HealthBarUpdate(EnemiesMaster.master.currentHealth);
                    Debug.Log(EnemiesMaster.master.currentHealth);
                    break;
                default:
                    break;
            }

            CounterUI(coins,ammo);
           
           
           
        }
    }

    private void Update()
    {
        CounterUI(coins, ammo);
    }




#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowConnections)
            foreach (PickupableAgent agent in Pickupables)
            {
                Vector3 managerPos = transform.position;
                Vector3 barrelPos = agent.transform.position;

                float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;

                Vector3 offset = Vector3.up * halfHeight;
                if(agent.tag == "Ammo")
                {
                     lineColor = Color.red;
                }

                if (agent.tag == "Coin")
                {
                    lineColor = Color.yellow;
                }

                if (agent.tag == "Heart")
                {
                    lineColor = Color.blue;
                }
                Handles.DrawBezier(
                    managerPos,
                    barrelPos,
                    managerPos - offset,
                    barrelPos + offset,

                    lineColor,
                    EditorGUIUtility.whiteTexture,
                    1f

                    ) ;

            }
    }
#endif
    private void CounterUI(int coins, int ammo)
    {

        CoinCounter.SetText(coins.ToString());
        AmmoCounter.SetText(ammo.ToString());

    }

}
