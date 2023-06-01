using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SortingSlotItemData : MonoBehaviour
{
    public InventorySlot AssignedInventorySlot;
    
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemCount;

    private void Awake()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        _itemCount.text = "";
        _itemIcon.sprite = null;
        _itemIcon.color = Color.clear;
        gameObject.SetActive(false);
    }
    
    public void UpdateSortingSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        _itemIcon.sprite = invSlot.ItemData.Icon;
        _itemIcon.color = Color.white;
        _itemCount.text = invSlot.StackSize.ToString();
        gameObject.SetActive(true);
        
    }
}
