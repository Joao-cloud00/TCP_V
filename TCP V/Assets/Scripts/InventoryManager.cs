using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> inventoryItems = new List<Item>();
    public Transform inventoryPanel;
    public GameObject inventorySlotPrefab;
    public Button equipButton, useButton, combineButton; // Novo botão de combinar
    private Item selectedItem1 = null;
    private Item selectedItem2 = null;
    public Item itemData;
    public bool isInventoryOpen = false;
    private GameObject currentInteractableObject = null;
    public bool atacar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
        combineButton.gameObject.SetActive(false);
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
                DeselectItems();
            }
        }

        // Se o inventário estiver aberto e o botão direito do mouse for pressionado, deseleciona os itens
        if (isInventoryOpen && Input.GetMouseButtonDown(1)) // 1 = Botão direito do mouse
        {
            DeselectItems();
        }
    }


    public void SelectItem(Item item)
    {
        if (selectedItem1 == null)
        {
            selectedItem1 = item;
        }
        else if (selectedItem2 == null)
        {
            selectedItem2 = item;
        }

        equipButton.gameObject.SetActive(selectedItem1 != null && selectedItem2 == null);
        useButton.gameObject.SetActive(selectedItem1 != null && selectedItem2 == null && currentInteractableObject != null);
        combineButton.gameObject.SetActive(selectedItem1 != null && selectedItem2 != null);
    }

    public void DeselectItems()
    {
        selectedItem1 = null;
        selectedItem2 = null;
        equipButton.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
        combineButton.gameObject.SetActive(false);
    }

    public void EquipItem()
    {
        if (selectedItem1 != null)
        {
            Item Item = ScriptableObject.CreateInstance<Item>();
            Item = itemData;
            if (selectedItem1 == Item)
            {
                atacar = true;
            }
            Debug.Log("Item equipado: " + selectedItem1.itemName);
        }
    }

    public void UseItem()
    {
        if (selectedItem1 != null && currentInteractableObject != null)
        {
            Interactable interactable = currentInteractableObject.GetComponent<Interactable>();
            if (interactable != null && interactable.CanInteract(selectedItem1))
            {
                interactable.Interact(selectedItem1);
                RemoveItem(selectedItem1);
                DeselectItems();
            }
            else
            {
                Debug.Log("Esse item não pode ser usado aqui!");
            }
        }
    }


    public void CombineItems()
    {
        if (selectedItem1 != null && selectedItem2 != null)
        {
            Item combinedItem = GetCombinationResult(selectedItem1, selectedItem2);
            if (combinedItem != null)
            {
                Debug.Log("Itens combinados: " + selectedItem1.itemName + " + " + selectedItem2.itemName + " = " + combinedItem.itemName);
                RemoveItem(selectedItem1);
                RemoveItem(selectedItem2);
                AddItem(combinedItem);
                DeselectItems();
            }
            else
            {
                Debug.Log("Esses itens não podem ser combinados!");
                DeselectItems();
            }
        }
    }

    private Item GetCombinationResult(Item item1, Item item2)
    {
        // Exemplo de combinação: Se os itens forem "Pedra" e "Fogo", criamos "Pedra de Lava"
        if ((item1.itemName == "Madeira" && item2.itemName == "Pregos") ||
            (item1.itemName == "Pregos" && item2.itemName == "Madeira"))
        {
            Item combinedItem = ScriptableObject.CreateInstance<Item>();
            combinedItem = itemData;
            return combinedItem;

        }

        return null; // Nenhuma combinação válida
    }

    public void SetInteractableObject(GameObject interactableObject)
    {
        currentInteractableObject = interactableObject;
        if (selectedItem1 != null)
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
