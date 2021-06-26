using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[ExecuteAlways]
public class GateMaster : MonoBehaviour
{
    public static GateMaster master;
    public delegate void GateTrigger();

    public static List<GateAgent> Gates = new List<GateAgent>();

    public bool DebugMessages = false;
    public bool ShowConnections = false;

   

    [SerializeField]
    Color lineColor = Color.white;

    GameObject coinUI;

    Canvas coincanvas;

    TextMeshProUGUI NeededCoinsCounter;

    [SerializeField]
    AudioClip gateOpenSound;

 


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
    public void Start()
    {
        coinUI = GameObject.FindGameObjectWithTag("CoinRequiredUI");
        

        coincanvas = coinUI.GetComponent<Canvas>();
        NeededCoinsCounter = GameObject.FindGameObjectWithTag("CoinsNeededCounter").GetComponent<TextMeshProUGUI>();

        if (coinUI != null) coincanvas.enabled = false;
        else if (coinUI == null) Debug.Log("coinUI is null");

        
    }


    public static Action<GateMaster> OnGateTriggerExit;

    public static Action<GateMaster, int> OnGateOpen;

    public static Action<GateMaster> OnGateTriggerEnter;
    [SerializeField]
    AudioClip errorSound;

    public void TriggerEnter(GateAgent agent, int coinsNeeded, AudioSource source)
    {
        SetCoinUI(agent);
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (agent.coinsNeeded <= PickupableMaster.master.coins)
            {
                if (DebugMessages) Debug.Log("Space Pressed");
                agent.doorLeft.Rotate(new Vector3(0, 0, -80));
                agent.doorRight.Rotate(new Vector3(0, 0, 80));
                if (DebugMessages) Debug.Log("Movement active");
                source.PlayOneShot(gateOpenSound);

                Destroy(agent.gameObject.GetComponent<BoxCollider>());
                coincanvas.enabled = false;

                PickupableMaster.master.coins -= agent.coinsNeeded;
            } else
            {
                Debug.Log("not enough coins");
                NeededCoinsCounter.color = Color.red;
                source.PlayOneShot(errorSound);
                StartCoroutine(NoCoins(2));
            }
        }
    }


    private System.Collections.IEnumerator NoCoins(float time)
    {
        yield return new WaitForSeconds(time);
        NeededCoinsCounter.color = Color.white;
    }
    public void TriggerExit(GateAgent agent)
    {
        coincanvas.enabled = false;
    }

    private void SetCoinUI(GateAgent agent)
    {
        NeededCoinsCounter.SetText(agent.coinsNeeded.ToString() + " x ");
        coincanvas.enabled = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowConnections)
            foreach (GateAgent agent in Gates)
            {
                Vector3 managerPos = transform.position;
                Vector3 gatePos = agent.transform.position;

                float halfHeight = (managerPos.y - gatePos.y) * 0.5f;

                Vector3 offset = Vector3.up * halfHeight;
               
                Handles.DrawBezier(
                    managerPos,
                    gatePos,
                    managerPos - offset,
                    gatePos + offset,

                    lineColor,
                    EditorGUIUtility.whiteTexture,
                    1f

                    );

            }
    }
#endif
}
