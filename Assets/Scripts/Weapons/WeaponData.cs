using System.Collections;
using System.Collections.Generic;
using Script.Weapons;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Created Weapon", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _prefabWeapon;
    [SerializeField] private GameObject _prefabBullet;
    [SerializeField] private float _damage;
    [SerializeField] private int _ammoInMagazine;
    [SerializeField] private float _shootingDaley;
    [SerializeField] private float _reloading;
    [SerializeField] private TypeAmmo _typeAmmo;

    public int ID => _id;
    public GameObject PrefabWeapon => _prefabWeapon;
    public GameObject PrefabBullet => _prefabBullet;
    public float Damage => _damage;
    public int AmmoInMagazine => _ammoInMagazine;
    public float ShootingDaley => _shootingDaley;
    public float Reloading => _reloading;
    public TypeAmmo TypeAmmo => _typeAmmo;
}