using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    [SerializeField] private InventorySystem _primaryInventorySystem;

    public InventorySystem PrimaryInventorySystem => _primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        _primaryInventorySystem = new InventorySystem(_inventorySize);
    }
}