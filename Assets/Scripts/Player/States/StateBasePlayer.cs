using UnityEngine;

public class StateBasePlayer : StateMachineBehaviour
{
    protected Player _player;
    protected Rigidbody2D _playerRb;
    
    protected static readonly int _moveHash = Animator.StringToHash("Move");
    protected static readonly int _weaponHash = Animator.StringToHash("Weapon");
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        _playerRb = animator.GetComponent<Rigidbody2D>();
    }
}
