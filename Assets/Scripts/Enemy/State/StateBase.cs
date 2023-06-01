using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : StateMachineBehaviour
{
    protected Enemy _enemy;
    protected Player _player;
    
    protected static readonly int _moveHash = Animator.StringToHash("Move");
    protected static readonly int _attackHash = Animator.StringToHash("Attack");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<Enemy>();
        _player = GameObject.FindObjectOfType<Player>();
    }
}
