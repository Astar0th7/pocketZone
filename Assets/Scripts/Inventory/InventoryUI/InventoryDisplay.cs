using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private SortingSlotItemData _sortingSlotItemData;  
    
    protected InventorySystem _inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> _slotDictionary;

    public InventorySystem InventorySystem => _inventorySystem;
    public IReadOnlyDictionary<InventorySlot_UI, InventorySlot> SlotDictionary => _slotDictionary;

    public abstract void AssignSlot(InventorySystem invToDysplay);

    protected virtual void Start()
    {
        
    }

    protected virtual void UpdateSlot(InventorySlot invSlot)
    {
        foreach (var slot in _slotDictionary)
        {
            if (slot.Value == invSlot)
            {
                slot.Key.UpdateUISlot(invSlot);
            }
        }
    }

    public void SlotClicked(InventorySlot_UI invUISlot)
    {
        if (invUISlot.AssignedInventorySlot.ItemData != null &&
            _sortingSlotItemData.AssignedInventorySlot.ItemData == null)
        {
            _sortingSlotItemData.UpdateSortingSlot(invUISlot.AssignedInventorySlot);
            invUISlot.ClearSlot();
            return;
        }
        
        if (invUISlot.AssignedInventorySlot.ItemData == null &&
            _sortingSlotItemData.AssignedInventorySlot.ItemData != null)
        {
            invUISlot.AssignedInventorySlot.AssignItem(_sortingSlotItemData.AssignedInventorySlot);
            invUISlot.UpdateUISlot();
            _sortingSlotItemData.ClearSlot();
            return;
        }
        
        if (invUISlot.AssignedInventorySlot.ItemData != null &&
            _sortingSlotItemData.AssignedInventorySlot.ItemData != null)
        {
            var isSameItem = invUISlot.AssignedInventorySlot.ItemData == _sortingSlotItemData.AssignedInventorySlot.ItemData;
            if (isSameItem && invUISlot.AssignedInventorySlot.RoomLeftInStack(_sortingSlotItemData.AssignedInventorySlot.StackSize))
            {
                invUISlot.AssignedInventorySlot.AssignItem(_sortingSlotItemData.AssignedInventorySlot);
                invUISlot.UpdateUISlot();
                _sortingSlotItemData.ClearSlot();
            }
            else if (isSameItem && 
                     !invUISlot.AssignedInventorySlot.RoomLeftInStack(_sortingSlotItemData.AssignedInventorySlot
                         .StackSize, out int leftInStack))
            {
                 if (leftInStack < 1)
                     SwipeSlot(invUISlot);
                 else
                 {
                     int remainingOnSorting = _sortingSlotItemData.AssignedInventorySlot.StackSize - leftInStack;
                     
                     invUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                     invUISlot.UpdateUISlot();

                     var newItem = new InventorySlot(_sortingSlotItemData.AssignedInventorySlot.ItemData,
                         remainingOnSorting);
                     _sortingSlotItemData.ClearSlot();
                     _sortingSlotItemData.UpdateSortingSlot(newItem);
                 }
                 
            }
            else if (!isSameItem)
                SwipeSlot(invUISlot);
        }
    }

    private void SwipeSlot(InventorySlot_UI invUISlot)
    {
        var cloneSlot = new InventorySlot(_sortingSlotItemData.AssignedInventorySlot.ItemData,
            _sortingSlotItemData.AssignedInventorySlot.StackSize);
        _sortingSlotItemData.ClearSlot();
        _sortingSlotItemData.UpdateSortingSlot(invUISlot.AssignedInventorySlot);
        
        invUISlot.ClearSlot();
        invUISlot.AssignedInventorySlot.AssignItem(cloneSlot);
        invUISlot.UpdateUISlot();
    }
}
