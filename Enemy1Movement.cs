using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//CONTROLS THE ENEMY1 MOVEMENT AND ANIMATION.
public class Enemy1Movement : MonoBehaviour {
	Animator m_Animator;
	public Transform goal;
	private NavMeshAgent agent;

    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
    }

    void Update()
    {
		//agent.destination = goal.position;
		Vector3 direction = goal.position - this.transform.position;
		float angle = Vector3.Angle(direction, this.transform.forward);
        //Press the up arrow button to reset the trigger and set another one
        if (Vector3.Distance(goal.position, this.transform.position) < 10 && angle < 360)
        {
			if(direction.magnitude > 5){
				agent.destination = goal.position;
				m_Animator.SetTrigger("isWalking");
				m_Animator.ResetTrigger("isRunning");
				if(direction.magnitude > 8){
					agent.speed = 3;
					agent.destination = goal.position;
					m_Animator.SetTrigger("isRunning");
					m_Animator.ResetTrigger("isWalking");
				}
			}else{
				m_Animator.SetTrigger("isAttacking");
				m_Animator.ResetTrigger("isRunning");
				m_Animator.ResetTrigger("isWalking");
			}
        }
		else{
			m_Animator.SetTrigger("isIdle");
			m_Animator.ResetTrigger("isRunning");
			m_Animator.ResetTrigger("isWalking");
		}
    }
}
