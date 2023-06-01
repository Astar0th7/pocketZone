
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private InventorySlot_UI[] _slots;
    
    protected override void Start()
    {
        base.Start();

        if (_inventoryHolder != null)
        {
            _inventorySystem = _inventoryHolder.PrimaryInventorySystem;
            _inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"Этому объекту не присвоен держатель инвенторя {gameObject}.");
        }
        
        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDysplay)
    {
        _slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (_slots.Length != _inventorySystem.InventorySize)
            Debug.Log($"У этого объекта не синхронизированно кол-во словтов в отображении и у держателя {gameObject}.");

        for (int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            _slotDictionary.Add(_slots[i], _inventorySystem.InventorySlots[i]);
            _slots[i].Init(_inventorySystem.InventorySlots[i]);
        }
    }
}