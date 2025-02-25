using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public GameObject portaSala;
    public GameObject Victory;
    public Slider slide;

    void Start()
    {
        currentHealth = maxHealth;
        slide.maxValue = currentHealth;
        slide.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyShooter shooter = this.gameObject.GetComponent<EnemyShooter>();
        EnemyCharge charger = this.gameObject.GetComponent<EnemyCharge>();
        EnemyChase chase = this.gameObject.GetComponent<EnemyChase>();
        if (shooter != null ) 
        {
            shooter.TakeDamage();
            AudioManager.instance.PlaySFX("dmg_boss");
            currentHealth -= damage;
            Debug.Log(gameObject.name + " tomou " + damage + " de dano. Vida restante: " + currentHealth);
        }
        else if (charger != null ) 
        { 
            charger.TakeDamage(damage);
            AudioManager.instance.PlaySFX("dmg_boss");
        }
        else if( chase != null )
        {
            if (chase.canTakeDamage)
            {
                chase.TakeDamage();
                AudioManager.instance.PlaySFX("dmg_boss");
                currentHealth -= damage;
                slide.value = currentHealth;    
                Debug.Log(gameObject.name + " tomou " + damage + " de dano. Vida restante: " + currentHealth);
            }
        }



        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        Destroy(gameObject);
        if(gameObject.name == "CorpoSeco")
        {
            Victory.SetActive(true);
            Time.timeScale = 0;
        }
        portaSala.GetComponent<SalaMiniBoss>().Sair = true;
    }
}
