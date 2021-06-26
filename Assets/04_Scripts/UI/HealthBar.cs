using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  
    public Slider slider;

    private void OnEnable() 
    {
        //EnemiesMaster.OnEnemyCollide += SetHealth;
        PickupableMaster.HealthBarUpdate += SetHealth;
    }
    private void OnDisable()
    {
        //EnemiesMaster.OnEnemyCollide -= SetHealth;
        PickupableMaster.HealthBarUpdate -= SetHealth;
    }


    public void SetHealth( int currentHealth)
    {
        slider.value = currentHealth;
        //Debug.Log(slider.value);
        //Debug.Log(currentHealth);
       
    }
}
