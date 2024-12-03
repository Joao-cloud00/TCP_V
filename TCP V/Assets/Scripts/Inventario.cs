using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Para usar o UI como RawImage, Text, etc.

public class Inventario : MonoBehaviour
{
    // Lista de itens no invent�rio
    public List<GameObject> items = new List<GameObject>();

    // Refer�ncia � UI do invent�rio
    public Transform inventoryUI; // Cont�iner do painel do invent�rio
    public GameObject inventorySlotPrefab; // Prefab para o slot de item na HUD (Ex: uma imagem ou bot�o)

    public int maxItems = 2; // Limite m�ximo de itens no invent�rio

    private GameObject[] inventorySlots; // Array para armazenar os slots

    private void Start()
    {
        // Inicializa o array de slots
        inventorySlots = new GameObject[maxItems];
        UpdateInventoryUI();
    }

    private void Update()
    {
        // Verifica se a tecla "T" foi pressionada para trocar os itens
        if (Input.GetKeyDown(KeyCode.T) && items.Count > 1)
        {
            SwapItems(); // Troca os itens se houver mais de um
        }
    }

    // Fun��o para adicionar um item ao invent�rio
    public void AddItem(GameObject item)
    {
        if (items.Count < maxItems && !items.Contains(item)) // Verifica o limite de itens
        {
            items.Add(item);
            UpdateInventoryUI(); // Atualiza a HUD ap�s adicionar o item
        }
        else if (items.Count >= maxItems)
        {
            // Caso o invent�rio esteja cheio, pode-se optar por substituir o item mais antigo
            // Ou pode-se optar por ignorar a adi��o de um novo item, como preferir
            Debug.Log("Invent�rio cheio!");
        }
    }

    // Fun��o para remover um item do invent�rio
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryUI(); // Atualiza a HUD ap�s remover o item
        }
    }

    // Fun��o para trocar o primeiro e o �ltimo item do invent�rio
    private void SwapItems()
    {
        // Troca os itens apenas se houver mais de um item no invent�rio
        if (items.Count > 1)
        {
            GameObject temp = items[0]; // Guarda o primeiro item
            items[0] = items[items.Count - 1]; // Coloca o �ltimo item no lugar do primeiro
            items[items.Count - 1] = temp; // Coloca o primeiro item no lugar do �ltimo

            UpdateInventoryUI(); // Atualiza a UI para refletir a troca
        }
    }

    // Atualiza a interface do invent�rio (HUD)
    private void UpdateInventoryUI()
    {
        // Limpa os slots atuais na HUD
        foreach (GameObject slot in inventorySlots)
        {
            if (slot != null)
                Destroy(slot); // Destr�i o slot
        }

        // Limpa a refer�ncia do array
        inventorySlots = new GameObject[maxItems];

        // Adiciona os itens da lista � HUD
        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = items[i];

            // Verifica se o slot existe ou cria um novo
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUI);
            TMP_Text itemText = slot.GetComponentInChildren<TMP_Text>(); // Exemplo de nome do item
            RawImage itemIcon = slot.GetComponentInChildren<RawImage>(); // Usando RawImage para o �cone

            itemText.text = item.name; // Exibe o nome do item
            itemIcon.texture = item.GetComponent<Item>().icon; // Exibe o �cone do item

            // Armazena o slot no array
            inventorySlots[i] = slot;

            // Define a visibilidade do slot
            slot.SetActive(i == items.Count - 1); // Apenas o �ltimo item ser� vis�vel
        }

        // Esconde o primeiro slot se houver mais de um item
        if (items.Count > 1 && inventorySlots[0] != null)
        {
            inventorySlots[0].SetActive(false); // Esconde o primeiro slot quando h� 2 itens
        }
        else if (inventorySlots[0] != null)
        {
            inventorySlots[0].SetActive(true); // Exibe o primeiro slot se houver apenas 1 item
        }
    }
}
