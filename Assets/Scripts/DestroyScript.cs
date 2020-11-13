using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : StateMachineBehaviour
{
    public bool ActuallyDestroy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Buff buff;
        if (animator.TryGetComponent<Buff>(out buff) && buff.Respawn)
        {
            GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
            gc.StartCoroutine(gc.RespawnBuff(buff.gameObject, buff.RespawnTime));
            buff.GetComponent<SpriteRenderer>().sprite = buff.DefaultSprite;
            ActuallyDestroy = false;
        }
        Debug.Log("destroying animation object " + animator.gameObject.name);
        if (ActuallyDestroy) Destroy(animator.gameObject);
        else animator.gameObject.SetActive(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
