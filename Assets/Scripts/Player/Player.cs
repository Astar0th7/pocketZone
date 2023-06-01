using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _defaultHealth;
    [SerializeField] private InventoryHolder _inventoryHolder;

    [Header("Weapon")] 
    [SerializeField] private Transform _positionWeapon;
    [SerializeField] private WeaponData _weaponData;

    [SerializeField] private UnityEvent<float> _changeHealth;
    [SerializeField] private UnityEvent<string> _changeAmmo;

    private GameObject _weaponObject;
    private Vector2 _direction;
    private float _currentHealth;
    private float _shootingDaley;
    private int _ammoInMagazine;
    private float _reloading;
    private Queue<Enemy> _enemiesInRange;
    private Enemy _enemy;

    public float Speed => _speed;
    public float DefaultHealth => _defaultHealth;
    public WeaponData WeaponData => _weaponData;
    public Vector2 Direction => _direction;
    public Enemy Enemy => _enemy;

    private float _normalizeHealth => _currentHealth / _defaultHealth;

    private void Start()
    {
        _currentHealth = _defaultHealth;
        _changeHealth.Invoke(_normalizeHealth);
        _enemiesInRange = new Queue<Enemy>();
        
        AddWeapon();
    }

    private void AddWeapon()
    {
        if (_weaponData != null)
        {
            _weaponObject = Instantiate(_weaponData.PrefabWeapon, _positionWeapon);
            _weaponObject.transform.parent = _positionWeapon;
            _ammoInMagazine = _weaponData.AmmoInMagazine;
        }
    }

    private void Update()
    {
        if (_shootingDaley > 0)
            _shootingDaley -= Time.deltaTime;

        if (_reloading > 0)
            _reloading -= Time.deltaTime;
        
        if (_enemy == null && _enemiesInRange.Count > 0)
            _enemy = _enemiesInRange.Dequeue();
        
        _direction.x = _joystick.Horizontal;
        _direction.y = _joystick.Vertical;

        if (Input.GetKey(KeyCode.R))
            Reloading();
        
        if (Input.GetKey(KeyCode.Space) && _ammoInMagazine > 0)
            Shooting();
    }

    public void Reloading()
    {
        int amountNeed = _weaponData.AmmoInMagazine - _ammoInMagazine;
        
        if (_inventoryHolder.PrimaryInventorySystem.GetItemFormInventory(1, amountNeed, out int getAmount))
        {
            _reloading = _weaponData.Reloading;
            _ammoInMagazine += getAmount;
            _changeAmmo.Invoke($"Ammo: {_ammoInMagazine}/{_weaponData.AmmoInMagazine}");
        }
    }
    
    public void Shooting()
    {
        if (_shootingDaley > 0 || _reloading > 0)
            return;

        Vector2 direction = transform.right * transform.localScale.x;
        Vector3 spawnBulletPosition = _weaponObject.transform.GetChild(0).position;
        
        if (_enemy != null)
            direction = Vector3.Normalize(_enemy.transform.position - spawnBulletPosition);

        GameObject bullet = Instantiate(_weaponData.PrefabBullet, spawnBulletPosition,
                Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(direction, _weaponData.Damage);

        _shootingDaley = _weaponData.ShootingDaley;
        _ammoInMagazine--;
        _changeAmmo.Invoke($"Ammo: {_ammoInMagazine}/{_weaponData.AmmoInMagazine}");
    }
    
    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Получено не верное значние урона.", nameof(damage));

        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
            Destroy(gameObject);
        
        _changeHealth.Invoke(_normalizeHealth);
    }

    public void EnemyInRange(Enemy enemy)
    {
        _enemiesInRange.Enqueue(enemy);
    }

    public void SetSpeed(float speed)
    {
        if (speed <= 0)
            throw new ArgumentException("Знначение скорость не может быть отрицательным или равным нулю.", nameof(speed)) ;

        _speed = speed;
    }

    public void SetHealth(float health)
    {
        if (health <= 0)
            throw new ArgumentException("Знначение здоровья не может быть отрицательным или равным нулю.", nameof(health)) ;

        _defaultHealth = health;
    }
}
