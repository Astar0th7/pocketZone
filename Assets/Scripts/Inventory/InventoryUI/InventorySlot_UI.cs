using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot_UI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private InventorySlot _assignedInventorySlot;

    public InventorySlot AssignedInventorySlot => _assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }
    
    private void Awake()
    {
        ClearSlot();

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot invSlot)
    {
        _assignedInventorySlot = invSlot;
        UpdateUISlot(invSlot);
    }
    
    public void UpdateUISlot(InventorySlot invSlot)
    {
        if (invSlot.ItemData != null)
        {
            _itemIcon.sprite = invSlot.ItemData.Icon;
            _itemIcon.color = Color.white;
            
            if (invSlot.StackSize > 1)
                _itemCount.text = invSlot.StackSize.ToString();
            else
                _itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (_assignedInventorySlot != null)
            UpdateUISlot(_assignedInventorySlot);
    }
    
    public void ClearSlot()
    {
        _assignedInventorySlot?.ClearSlot();
        _itemIcon.sprite = null;
        _itemIcon.color = Color.clear;
        _itemCount.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ParentDisplay?.SlotClicked(this);
    }
}
