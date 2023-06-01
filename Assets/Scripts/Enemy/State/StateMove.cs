using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 enemyPosition = animator.transform.position;
        float distance = Vector3.Distance(enemyPosition, playerPosition);
        Vector3 direction = Vector3.Normalize(playerPosition - enemyPosition);

        if (distance > _enemy.EnemyData.VisibilityRange)
        {
            animator.SetBool(_moveHash, false);
            return;
        }
        
        if (_enemy.PlayerInReach() != null)
        {
            animator.SetBool(_attackHash, true);
            return;
        }
        
        _enemy.transform.Translate(direction * _enemy.EnemyData.Speed * Time.deltaTime);

        Vector3 enemyLocalScale = _enemy.transform.localScale;
        if ((direction.x * enemyLocalScale.x) < 0)
            _enemy.transform.localScale = new Vector3(-enemyLocalScale.x, enemyLocalScale.y, enemyLocalScale.z);
    }
}
