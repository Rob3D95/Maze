using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameMaster : MonoBehaviour
{
    public static GameMaster master;
    public delegate void MasterLogic();

    public static Action OnPlayerDies;

    public bool DebugMessages;

    bool PauseMenuActive = false;
    bool LoseOverlayActive = false;
    bool WinOverlayActive = false;


    FPS_NoJump ps;
    Shooting sh;

    Canvas PauseOverlay;
    Canvas LoseOverlay;
    Canvas WinOverlay;
    GameObject AudioMaster;

    [SerializeField]
    float timeValue = 600;

    public TextMeshProUGUI timeText;

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

    private void OnEnable()
    {
        TriggerDefeat += DefeatScreenActivate;
        TriggerWin += WinScreenActivate;
    }
    private void OnDisable()
    {
        TriggerDefeat += DefeatScreenActivate;
        TriggerWin -= WinScreenActivate;
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<FPS_NoJump>();
        sh = GameObject.FindGameObjectWithTag("Gun").GetComponent<Shooting>();

        PauseOverlay = GameObject.FindGameObjectWithTag("PauseOverlay").GetComponent<Canvas>();
        if(PauseOverlay != null)
        {
            PauseOverlay.enabled = false;
        } else if (PauseOverlay == null)
        {
            Debug.Log("PauseOverlay is null");
            return;
        }

        LoseOverlay = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<Canvas>();

        if (LoseOverlay != null)
        {
            LoseOverlay.enabled = false;
        }
        else if (LoseOverlay == null)
        {
            Debug.Log("LoseOverlay is null");
            return;
        }

        WinOverlay = GameObject.FindGameObjectWithTag("WinScreen").GetComponent<Canvas>();

        if (WinOverlay != null)
        {
            WinOverlay.enabled = false;
        }
        else if (WinOverlay == null)
        {
            Debug.Log("WinOverlay is null");
            return;
        }


        AudioMaster = GameObject.FindGameObjectWithTag("AudioMaster");

        if (AudioMaster != null)
        {
            AudioMaster.SetActive(true);
        }
        else if (AudioMaster == null)
        {
            Debug.Log("AudioMaster is null");
            return;
        }

       

        timeText = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
        Debug.Assert(timeText != null, "timeText equals null, please fix!");


    }
    private void Update()
    {
        if (EnemiesMaster.master.currentHealth <= 0)
        {
            TriggerDefeat();
        }

        PauseGame();
        Timer();
    }

    void PauseGame()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenuActive = true;
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                ps.enabled = false;
                sh.enabled = false;
                PauseOverlay.enabled = true;
                AudioMaster.SetActive(false);
            }
            else
            {
                ps.enabled = true;
                sh.enabled = true;
                Time.timeScale = 1;
                PauseOverlay.enabled = false;
                AudioMaster.SetActive(true);
            }
           

        }

        if (PauseMenuActive && Input.GetKeyDown(KeyCode.Escape))
        {

           
            SceneManager.LoadSceneAsync(0);
        }

    }

    void Timer()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        } else
        {
            timeValue = 0;
            TriggerDefeat();
        }


        TimerDisplay(timeValue);

       
    }
    void TimerDisplay(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }




    public static Action TriggerDefeat;
    void DefeatScreenActivate()
    {
        LoseOverlayActive = true;
        Time.timeScale = 0;
        ps.enabled = false;
        sh.enabled = false;
        

        if (LoseOverlayActive)
        {
            LoseOverlay.enabled = true;
        }

        if(LoseOverlayActive && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

        }
           
    }

    public static Action TriggerWin;
    

    void WinScreenActivate()
    {
        WinOverlayActive = true;
        Time.timeScale = 0;
        ps.enabled = false;
        sh.enabled = false;
        AudioMaster.SetActive(false);
        


        if (WinOverlayActive)
        {
            WinOverlay.enabled = true;
        }
        
       

        if (WinOverlayActive && Input.GetKeyDown(KeyCode.Escape))
        {
            
            SceneManager.LoadScene(0);
        }

    }
}

   
