using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> inventoryItems = new List<Item>(); // Lista de itens coletados
    public Transform inventoryPanel; // Painel gráfico do inventário
    public GameObject inventorySlotPrefab; // Prefab do slot do inventário
    public Button equipButton, useButton; // Botões de equipar e usar
    private Item selectedItem = null; // Item atualmente selecionado
    private bool isInventoryOpen = false; // Estado do inventário
    private bool canUseItem = false; // Se o jogador pode usar o item no momento

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
    }

    public void AddItem(Item newItem)
    {
        inventoryItems.Add(newItem);
        if (isInventoryOpen)
        {
            UpdateInventoryUI();
        }
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
        if (isInventoryOpen)
        {
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventoryItems)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            slot.GetComponent<Image>().sprite = item.icon;

            Button slotButton = slot.GetComponentInChildren<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() => SelectItem(item));
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.gameObject.SetActive(isInventoryOpen);
            equipButton.enabled = isInventoryOpen;
            useButton.enabled = isInventoryOpen;

            if (isInventoryOpen)
            {
                UpdateInventoryUI();
            }
            else
            {
                DeselectItem();
            }
        }
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
        equipButton.gameObject.SetActive(true);
        useButton.gameObject.SetActive(canUseItem);
    }

    public void DeselectItem()
    {
        selectedItem = null;
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
    }

    public void EquipItem()
    {
        if (selectedItem != null)
        {
            Debug.Log("Item equipado: " + selectedItem.itemName);
        }
    }

    public void UseItem()
    {
        if (selectedItem != null && canUseItem)
        {
            Debug.Log("Item usado: " + selectedItem.itemName);
            RemoveItem(selectedItem);
            DeselectItem();
        }
    }

    public void SetCanUseItem(bool state)
    {
        canUseItem = state;
        if (selectedItem != null)
        {
            useButton.gameObject.SetActive(canUseItem);
        }
    }
}
