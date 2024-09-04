using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public Animator animator;
    public GameObject Enemy;
    public static Health Instance;

    public float MaxHealth, Chealth;

    private void Start()
    {
        Instance = this;
        Chealth = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.value = Chealth;
    }

    public void health(float Damage)
    {
        if (Chealth <= 0 || IsGamePaused()) return;
        Chealth -= Damage;
        slider.value = Chealth;
        Debug.Log(Chealth);
        if ( Chealth <= 0)
        {
            Death();
        }
    }

    public void ResetHealth(float newMaxHealth)
    {
        MaxHealth = newMaxHealth;
        Chealth = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.value = Chealth;
        Debug.Log("Health reset to: " + Chealth);
    }

    public void Death()
    {
        if (animator!= null)
        {
          animator.SetTrigger("Death");
            animator.SetBool("Dead", true);
        }

        Destroy(Enemy);

        Debug.Log("Health Decreased and entered the Death Function");
    }

    private bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }

    public void ResetsHealth()
    {
        Chealth = MaxHealth;
        slider.value = Chealth;
        Debug.Log("Health reset to max: " + Chealth);
    }
}
