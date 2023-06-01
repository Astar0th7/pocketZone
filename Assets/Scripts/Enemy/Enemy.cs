using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData;

    [SerializeField] private UnityEvent<float> _changeHealth;
    [SerializeField] private ItemPickUp _dropItem;

    private float _currentHealth;
    
    
    private void Start()
    {
        _currentHealth = EnemyData.Health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        _changeHealth.Invoke(_currentHealth / EnemyData.Health);
        
        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        var item = Instantiate(_dropItem, transform.position, Quaternion.identity);
        item.Init(Random.Range(1, 20));
        Destroy(gameObject);
    }

    public Player PlayerInReach()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + transform.right * EnemyData.OffsetRangeAttack.x * transform.localScale.x + transform.up * EnemyData.OffsetRangeAttack.y, new Vector2(EnemyData.AttackRange, EnemyData.AttackRange * 2), 0, Vector2.left, 0);
        
        if (hit.collider != null && hit.collider.TryGetComponent(out Player player))
            return player;

        return null;
    }

    private void DamagePlayer()
    {
        Player player = PlayerInReach();
        if (player != null)
            player.TakeDamage(EnemyData.Damage);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.right * EnemyData.OffsetRangeAttack.x * transform.localScale.x + transform.up *  EnemyData.OffsetRangeAttack.y, new Vector2(EnemyData.AttackRange, EnemyData.AttackRange * 2));
    }

    
}
