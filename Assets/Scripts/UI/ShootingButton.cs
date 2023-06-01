using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShootingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Player _player;

    private bool _isShooting;

    private void Update()
    {
        if(_isShooting)
            _player.Shooting();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isShooting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isShooting = false;
    }
}
