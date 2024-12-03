using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Nome do item
    public Texture icon; // �cone do item, se tiver
    public GameObject aperteE;
    // M�todo para pegar o item
    public void PickUp()
    {
        // Desativa o item quando for pego
        gameObject.SetActive(false);
    }

    // M�todo para soltar o item
    public void Drop(Vector3 position)
    {
        // Coloca o item na posi��o onde foi solto e ativa ele
        transform.position = position;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aperteE.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            aperteE.SetActive(false);
        }

    }
}

