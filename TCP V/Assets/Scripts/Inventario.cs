using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Para usar o UI como RawImage, Text, etc.

public class Inventario : MonoBehaviour
{
    // Lista de itens no inventário
    public List<GameObject> items = new List<GameObject>();

    // Referência à UI do inventário
    public Transform inventoryUI; // Contêiner do painel do inventário
    public GameObject inventorySlotPrefab; // Prefab para o slot de item na HUD (Ex: uma imagem ou botão)

    public int maxItems = 2; // Limite máximo de itens no inventário

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

    // Função para adicionar um item ao inventário
    public void AddItem(GameObject item)
    {
        if (items.Count < maxItems && !items.Contains(item)) // Verifica o limite de itens
        {
            items.Add(item);
            UpdateInventoryUI(); // Atualiza a HUD após adicionar o item
        }
        else if (items.Count >= maxItems)
        {
            // Caso o inventário esteja cheio, pode-se optar por substituir o item mais antigo
            // Ou pode-se optar por ignorar a adição de um novo item, como preferir
            Debug.Log("Inventário cheio!");
        }
    }

    // Função para remover um item do inventário
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryUI(); // Atualiza a HUD após remover o item
        }
    }

    // Função para trocar o primeiro e o último item do inventário
    private void SwapItems()
    {
        // Troca os itens apenas se houver mais de um item no inventário
        if (items.Count > 1)
        {
            GameObject temp = items[0]; // Guarda o primeiro item
            items[0] = items[items.Count - 1]; // Coloca o último item no lugar do primeiro
            items[items.Count - 1] = temp; // Coloca o primeiro item no lugar do último

            UpdateInventoryUI(); // Atualiza a UI para refletir a troca
        }
    }

    // Atualiza a interface do inventário (HUD)
    private void UpdateInventoryUI()
    {
        // Limpa os slots atuais na HUD
        foreach (GameObject slot in inventorySlots)
        {
            if (slot != null)
                Destroy(slot); // Destrói o slot
        }

        // Limpa a referência do array
        inventorySlots = new GameObject[maxItems];

        // Adiciona os itens da lista à HUD
        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = items[i];

            // Verifica se o slot existe ou cria um novo
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUI);
            TMP_Text itemText = slot.GetComponentInChildren<TMP_Text>(); // Exemplo de nome do item
            RawImage itemIcon = slot.GetComponentInChildren<RawImage>(); // Usando RawImage para o ícone

            itemText.text = item.name; // Exibe o nome do item
            itemIcon.texture = item.GetComponent<Item>().icon; // Exibe o ícone do item

            // Armazena o slot no array
            inventorySlots[i] = slot;

            // Define a visibilidade do slot
            slot.SetActive(i == items.Count - 1); // Apenas o último item será visível
        }

        // Esconde o primeiro slot se houver mais de um item
        if (items.Count > 1 && inventorySlots[0] != null)
        {
            inventorySlots[0].SetActive(false); // Esconde o primeiro slot quando há 2 itens
        }
        else if (inventorySlots[0] != null)
        {
            inventorySlots[0].SetActive(true); // Exibe o primeiro slot se houver apenas 1 item
        }
    }
}
