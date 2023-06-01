using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spwanPoints;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _countEnemy; 
    [SerializeField] private List<Enemy> _enemies;

    private float _delay = 0;

    private void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay <= 0 && _countEnemy > 0)
        {
            var randonPos = new Vector2(Random.Range(_spwanPoints[0].position.x, _spwanPoints[1].position.x),
                Random.Range(_spwanPoints[0].position.y, _spwanPoints[1].position.y));

            Instantiate(_enemies[Random.Range(0, _enemies.Count)], randonPos, quaternion.identity);

            _countEnemy--;
            _delay = _spawnDelay;
        }
    }
}
