using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private int _stackSize;

    public InventoryItemData ItemData => _itemData;
    public int StackSize => _stackSize;

    public InventorySlot(InventoryItemData itemData, int amount)
    {
        _itemData = itemData;
        _stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _itemData = null;
        _stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (_itemData == invSlot.ItemData)
        {
            AddToStack(invSlot.StackSize);
        }
        else
        {
            _itemData = invSlot.ItemData;
            _stackSize = 0;
            AddToStack(invSlot.StackSize);
        }
    }
    
    public void UpdateInventorySlot(InventoryItemData itemData, int amount)
    {
        _itemData = itemData;
        _stackSize = amount;
    }
    
    public bool RoomLeftInStack(int amountInAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - _stackSize;

        return RoomLeftInStack(amountInAdd);
    }

    public bool RoomLeftInStack(int amountInAdd)
    {
        if (_stackSize + amountInAdd <= _itemData.MaxStackSize)
            return true;

        return false;
    }
    
    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

    
}
