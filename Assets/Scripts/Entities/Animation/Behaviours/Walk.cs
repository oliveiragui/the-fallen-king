using UnityEngine;

namespace Entities.Animation.Behaviours
{
    [SharedBetweenAnimators]
    public class Walk : StateMachineBehaviour
    {
        [SerializeField] int currentCombo;
        Entity entity;
        
        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (entity == null) entity = animator.transform.GetComponent<Entity>();
           // entity.Movimenta();
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (entity == null) entity = animator.transform.GetComponent<Entity>();
            entity.ParaDeAndar();
        }
    }
}