using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Item itemData; // Referência ao ScriptableObject do item
    private bool isPlayerInRange = false; // Verifica se o jogador está próximo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Se o jogador pressionar "E"
        {
            InventoryManager.Instance.AddItem(itemData);
            Destroy(gameObject); // Remove o item da cena após a coleta
        }
    }
}


