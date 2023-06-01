using UnityEngine;

public class StateIdlePlayer : StateBasePlayer
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        CheckWeapon(animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player.Direction != Vector2.zero)
            animator.SetBool(_moveHash, true);

        CheckWeapon(animator);
    }

    private void CheckWeapon(Animator animator)
    {
        if (_player.WeaponData == null)
        {
            animator.SetInteger(_weaponHash, 0);
        }

        animator.SetInteger(_weaponHash, _player.WeaponData.ID);
    }
}
