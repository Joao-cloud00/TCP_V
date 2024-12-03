using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Inventario inventory;
    private GameObject currentItem; // O item atualmente carregado pelo jogador

    private void Start()
    {
        // Obtém o componente de inventário do jogador
        inventory = GetComponent<Inventario>();
    }

    private void Update()
    {
        // Interação com os itens
        if (Input.GetKeyDown(KeyCode.E) && currentItem != null) // Pega o item
        {
            currentItem.GetComponent<Item>().PickUp();
            inventory.AddItem(currentItem); // Adiciona o item ao inventário
            currentItem = null; // Limpa o item atual
        }

        if (Input.GetKeyDown(KeyCode.Q) && inventory.items.Count > 0) // Solta o item
        {
            DropItem();
            currentItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta se o jogador entrou em contato com um item
        if (collision.CompareTag("Item"))
        {
            currentItem = collision.gameObject; // Guarda o item
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Item"))
    //    {
    //        currentItem = collision.gameObject; // Guarda o item
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    currentItem = null;
    //}

    private void DropItem()
    {
        // Solta o item do inventário
        GameObject itemToDrop = inventory.items[inventory.items.Count - 1]; // Pega o último item
        inventory.RemoveItem(itemToDrop); // Remove do inventário
        itemToDrop.GetComponent<Item>().Drop(transform.position);// Solta o item na posição do jogador
    }
}





