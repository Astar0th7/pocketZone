using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Created Enemy", order = 51)]
public class EnemyData : ScriptableObject
{   
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _visibilityRange;
    [SerializeField] private float _damage;
    [SerializeField] private Vector2 _offsetRangeAttack;
    [SerializeField] private float _attackRange;

    public float Health => _health;
    public float Speed => _speed;
    public float VisibilityRange => _visibilityRange;
    public float Damage => _damage;
    public Vector2 OffsetRangeAttack => _offsetRangeAttack;
    public float AttackRange => _attackRange;
}
