using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    // Slider healthSlider; // Slider da HUD
    //public TMP_Text text; // Slider da HUD

    private void Start()
    {
        currentHealth = maxHealth;
        //healthSlider.maxValue = maxHealth;
        //healthSlider.value = currentHealth;
        //text.text = "Vida : " + currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthSlider.value = currentHealth;
        //text.text = "Vida : "  + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("O jogador morreu!");
        gameObject.SetActive(false); // Desativa o jogador
        // Aqui você pode chamar uma tela de game over
    }
}

