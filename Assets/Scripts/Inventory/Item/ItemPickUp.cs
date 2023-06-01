using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private int _amount;

    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true; 
    }

    public void Init(int amount)
    {
        if (amount <= 0)
            amount = 1;
        
        _amount = amount;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.TryGetComponent(out InventoryHolder inventory))
        {
            if (inventory.PrimaryInventorySystem.AddToInventory(_itemData, _amount, out int leftInStack))
            {
                if (leftInStack <= 0)
                    Destroy(gameObject);
                
                _amount = leftInStack;
            }
        }
    }
}
