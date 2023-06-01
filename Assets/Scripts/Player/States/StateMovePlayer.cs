using TMPro;
using UnityEngine;

public class StateMovePlayer : StateBasePlayer
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player.Direction == Vector2.zero)
            animator.SetBool(_moveHash, false);
        
        var transform = _player.transform;
        
        _playerRb.MovePosition(transform.position + (Vector3)_player.Direction * _player.Speed * Time.deltaTime);
        
        if (_player.Direction.x * _player.transform.localScale.x < 0 && _player.Enemy == null)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (_player.Enemy != null)
        {
            Vector2 directionToEnemy = Vector3.Normalize(_player.Enemy.transform.position - transform.position);
            if (directionToEnemy.x * transform.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        
        CheckWeapon(animator);
    }
    
    private void CheckWeapon(Animator animator)
    {
        switch (_player.WeaponData.ID)
        {
            case 0:
                animator.SetInteger(_weaponHash, 0);
                break;
            case 1:
                animator.SetInteger(_weaponHash, 1);
                break;
            case 2:
                animator.SetInteger(_weaponHash, 2);
                break;
        }
    }
}
