using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _upperLimit;
    [SerializeField] private float _bottomLimit;
    
    private Transform _playerTransform;

    private void Start ()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
        FindPlayer();
    }

    private void FindPlayer()
    {
        _playerTransform = GameObject.FindObjectOfType<Player>().transform;
        transform.position = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
    }

    private void Update () 
    {
        if(_playerTransform)
        {
            Vector3 target = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.aspect * halfHeight;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, _leftLimit + halfWidth, _rightLimit - halfWidth),
            Mathf.Clamp(transform.position.y, _bottomLimit + halfHeight, _upperLimit - halfHeight),
            transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_rightLimit, _upperLimit));
        Gizmos.DrawLine(new Vector2(_leftLimit, _bottomLimit), new Vector2(_rightLimit, _bottomLimit));
        Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_leftLimit, _bottomLimit));
        Gizmos.DrawLine(new Vector2(_rightLimit, _upperLimit), new Vector2(_rightLimit, _bottomLimit));
    }
}
