using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healingAmount = 20; // Quantidade de vida recuperada
    public GameObject aperteE;
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Verifica se o jogador apertou a tecla "E"
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                LifeHUD lifeHUD = player.GetComponent<LifeHUD>();
                if (lifeHUD != null)
                {
                    lifeHUD.Heal(healingAmount); // Cura o jogador
                    Destroy(gameObject); // Remove o item após interação
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
             if (collision.CompareTag("Player"))
                {
                    isPlayerNearby = true;
                    aperteE.SetActive(true);
                }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
                if (collision.CompareTag("Player"))
                {
                    isPlayerNearby = false;
                    aperteE.SetActive(false);
                }
           
    }
}
