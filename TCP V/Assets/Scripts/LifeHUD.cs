using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour
{
    public Slider healthBar; // Slider da barra de vida
    public TMP_Text healthText; // Texto exibindo o valor da vida
    public int maxHealth = 100;
    private int currentHealth = 50;

    void Start()
    {
        UpdateHUD();
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHUD();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (healthBar != null)
            healthBar.value = (float)currentHealth / maxHealth;

        if (healthText != null)
            healthText.text = $"Vida: {currentHealth}/{maxHealth}";
    }
}
