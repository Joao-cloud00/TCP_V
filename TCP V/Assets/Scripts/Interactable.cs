using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item requiredItem; // O item necess�rio para interagir

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.SetInteractableObject(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.ClearInteractableObject(gameObject);
        }
    }

    public bool CanInteract(Item item)
    {
        return item == requiredItem; // S� interage se o item for o correto
    }

    public void Interact(Item item)
    {
        if (CanInteract(item))
        {
            Debug.Log(item.itemName + " usado em " + gameObject.name);
            Destroy(gameObject); // Exemplo: remover o objeto do cen�rio
        }
        else
        {
            Debug.Log("Esse item n�o pode ser usado aqui!");
        }
    }
}

