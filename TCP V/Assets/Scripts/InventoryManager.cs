using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> inventoryItems = new List<Item>();
    public Transform inventoryPanel;
    public GameObject inventorySlotPrefab;
    public Button equipButton, useButton;
    private Item selectedItem = null;
    private bool isInventoryOpen = false;
    private GameObject currentInteractableObject = null; // Objeto interativo atual

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
        useButton.gameObject.SetActive(currentInteractableObject != null);
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
        if (selectedItem != null && currentInteractableObject != null)
        {
            Debug.Log("Usando " + selectedItem.itemName + " em " + currentInteractableObject.name);
            if(currentInteractableObject.tag == "Barricada" && selectedItem.name == "Madeira")
            {
                Destroy(currentInteractableObject);
                RemoveItem(selectedItem);
                DeselectItem();
            }
        }
    }

    public void SetInteractableObject(GameObject interactableObject)
    {
        currentInteractableObject = interactableObject;
        if (selectedItem != null)
        {
            useButton.gameObject.SetActive(currentInteractableObject != null);
        }
    }

    public void ClearInteractableObject(GameObject interactableObject)
    {
        if (currentInteractableObject == interactableObject)
        {
            currentInteractableObject = null;
            useButton.gameObject.SetActive(false);
        }
    }
}
