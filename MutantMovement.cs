using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*The NPC nav_mesh agent's movement towards the character
 begins when the player is in range of the mutant.
The mutant has a angle of sight and sense when the player is close by.
The NPC walks, runs, attacks, and jumps at a certain distance between the 
player and the NPC*/

[RequireComponent(typeof(Animator))]
public class MutantMovement : MonoBehaviour
{
    [SerializeField] private float _range = 10f;

    //public Transform _goal; // destination
    private Transform _player;
    private Animator _anim;
    private bool _alive;
    private float _distance;
    private AudioSource _attackSound;

    void Start()
    {
        _alive = true;
        _player = GameObject.Find("Player").transform;
        _anim = GetComponent<Animator>();
        AudioSource[] sounds = gameObject.GetComponents<AudioSource>();
        _attackSound = sounds[0];
        
    }

    void Update()
    {
        
        Vector3 direction = _player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if(Vector3.Distance(_player.position, this.transform.position) < 10 && angle < 180){
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            _anim.SetBool("isIdle", false);
            if(direction.magnitude > 5){
                this.transform.Translate(0, 0, 0.05f);
                _anim.SetBool("isWalking", true);
                _anim.SetBool("isAttacking", false);
            }else{
                _anim.SetBool("isAttacking", true);
                _anim.SetBool("isWalking", false);
            }
        }else{
            _anim.SetBool("isIdle", true);
            _anim.SetBool("isWalking", false);
            _anim.SetBool("isAttacking", false);
        }
    }


    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

}
    


