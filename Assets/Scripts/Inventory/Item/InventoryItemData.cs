using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory System/Inventory Item", order = 51)]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _displayName;
    [TextArea(4, 4)]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _maxStackSize;

    public int ID => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int MaxStackSize => _maxStackSize;
}
