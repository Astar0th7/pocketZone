using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private Rigidbody2D _rb;
    private float _damage;

    public void Init(Vector3 direction, float damage)
    {
        _direction = direction;
        _damage = damage;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rb.MovePosition(transform.position + _direction * 20 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
