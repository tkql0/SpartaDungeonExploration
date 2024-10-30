using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventroyWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    int selectedItemIndex = 0;
    // ������ ������
    int curEquipIndex;
    // ������ ������
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventroy += Toggle;

        CharacterManager.Instance.Player.addItem += AddItem;

        inventroyWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];
        // slotPanel.childCount : �ڽ� ������Ʈ�� ������ ������ �� ����

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventroy = this;
        }
    }

    void ClearSelctedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            inventroyWindow.SetActive(false);
        }
        else
        {
            inventroyWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventroyWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData date = CharacterManager.Instance.Player.itemData;

        // �������� �ߺ� �������� canStack Ȯ��
        if (date.canStack)
        {
            ItemSlot slot = GetItemStack(date);

            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        // ����ִ� ���� ��������
        ItemSlot emptySlot = GetEmptyStack();
        // �ִٸ�
        if (emptySlot != null)
        {
            emptySlot.item = date;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        // ���ٸ�
        ThrowItem(date);

        CharacterManager.Instance.Player.itemData = null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            { // ���� �����Ͱ� ���ų� �ߺ� ������ ������ ���� Maxġ ���� ���� ���
                return slots[i];
            }
        }
        return null;
    }
    ItemSlot GetEmptyStack()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position,
            Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int inIndex)
    {
        if (slots[inIndex].item == null)
        {
            return;
        }

        selectedItem = slots[inIndex].item;
        selectedItemIndex = inIndex;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }
        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        // �������� ������
        RemoveSelectedItem();
        // ������ ���� ���ֱ�
    }

    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            // �������� ������ �� �κ��丮���� ����
            selectedItemIndex = -1;
            ClearSelctedItemWindow();
        }
        UpdateUI();
    }
}
