using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateIdle : StateBase
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 enemyPosition = animator.transform.position;
        float distance = Vector3.Distance(enemyPosition, playerPosition);
        
        if (_enemy.EnemyData.AttackRange * 1.5f >= distance)
        {
            animator.SetBool(_attackHash, true);
            return;
        }

        if (distance <= _enemy.EnemyData.VisibilityRange)
        {
            animator.SetBool(_moveHash, true);
            _player.EnemyInRange(_enemy);
        }
    }
}
