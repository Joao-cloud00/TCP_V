using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public GameObject portaSala;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        EnemyShooter shooter = this.gameObject.GetComponent<EnemyShooter>();
        if (shooter != null ) 
        {
            shooter.TakeDamage();
            currentHealth -= damage;
            Debug.Log(gameObject.name + " tomou " + damage + " de dano. Vida restante: " + currentHealth);
        }
        else 
        { 
            EnemyCharge charger = this.gameObject.GetComponent<EnemyCharge>();
            charger.TakeDamage(damage);
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
        portaSala.GetComponent<SalaMiniBoss>().Sair = true;
    }
}
