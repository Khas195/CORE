using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorControl : MonoBehaviour
{
    [SerializeField]
    Rigidbody hostRb;
    [SerializeField]
    Animator animator;
    [SerializeField]
    List<AnimationClip> attackingAnims;
    [SerializeField]
    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var curClip = animator.GetCurrentAnimatorClipInfo(0)[0];
        isAttacking = IsAnAttackingClip(curClip);
        animator.SetFloat("MoveSpeed", hostRb.velocity.magnitude);
        animator.SetInteger("ChangeInVelY", (int)hostRb.velocity.y);
    }

    private bool IsAnAttackingClip(AnimatorClipInfo curClip)
    {
        foreach(var clip in attackingAnims){
            if (curClip.clip == clip){
                return true;
            }
        } 
        return false;
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
