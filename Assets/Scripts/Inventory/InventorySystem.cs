using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> _inventorySlots;

    public IReadOnlyList<InventorySlot> InventorySlots => _inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemData, int amountToAdd, out int amountRemaining)
    {
        amountRemaining = 0;
        
        if (ContainsItem(itemData, out List<InventorySlot> invSlots))
        {
            foreach (var slot in invSlots)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
                
                if (!slot.RoomLeftInStack(amountToAdd, out int leftInStack))
                {
                    if (leftInStack < 1) continue;
                    
                    amountRemaining = amountToAdd - leftInStack;
                    slot.AddToStack(leftInStack);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemData, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool GetItemFormInventory(int itemID, int amountNeed, out int getAmount)
    {
        getAmount = 0;

        if (ContainsItem(itemID, out List<InventorySlot> invSlots))
        {
            foreach (var slot in invSlots)
            {
                var amountInStack = slot.StackSize;

                if (amountInStack <= amountNeed)
                {
                    getAmount += amountInStack;
                    amountNeed -= amountInStack;
                    slot.ClearSlot();
                    OnInventorySlotChanged?.Invoke(slot);
                }
                else
                {
                    getAmount += amountNeed;
                    slot.RemoveFromStack(amountNeed);
                    OnInventorySlotChanged?.Invoke(slot);
                }

                if (amountNeed == 0)
                    return true;
            }

            return true;
        }
        
        return false;
    }
    
    public bool ContainsItem(InventoryItemData itemData, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(x => x.ItemData == itemData).ToList();
        return invSlot != null;
    }
    
    public bool ContainsItem(int itemID, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(x => x.ItemData != null && x.ItemData.ID == itemID).ToList();
        return invSlot != null;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(x => x.ItemData == null);
        return freeSlot != null;
    }
}
