using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*This script is in charge of putting the enemies in wandering state when game over or player has beat the game*/
public class WanderingAI : MonoBehaviour
{

    
    private Vector3 _destination;
    private NavMeshAgent _agent;
    private int _roomNumber;
    private float _randX;
    private float _randZ;

    private Animator _anim;
    private SceneController _sceneController;
   

    //Todo: Figure out why bystander moves before animation starts
    void Start()
    {
        _anim = GetComponent<Animator>();

        _sceneController = GameObject.Find("Controller").GetComponent<SceneController>();

       
        if (this.gameObject.tag == "mutant")
            _anim.SetBool("isWalking", true);

        else
        {
            _anim.SetBool("byStanderRunning", true);

           
        }
        // Cache _agent component and _destination
        _agent = GetComponent<NavMeshAgent>();
        _roomNumber = GetRoomNumber();
        _agent.destination = WalkTo();
       
    }



    // Update is called once per frame
    void Update()
    {

        if (pathComplete())
            _agent.destination = WalkTo();

    }


    private Vector3 WalkTo()
    {
        if (_roomNumber == 1)
            _destination = _sceneController.GetRoom1RandomLocations();
        else if (_roomNumber == 2)
            _destination = _sceneController.GetRoom2RandomLocations();
        else
            _destination = _sceneController.GetRoom3RandomLocations();

        return _destination;
    }

    private int GetRoomNumber()
    {
        if (gameObject.transform.position.z < -13.5693f)
            _roomNumber = 3;
        else if (gameObject.transform.position.z >= -13.5693f && gameObject.transform.position.z <= 15.23492f)
            _roomNumber = 1;
        else
            _roomNumber = 2;

        return _roomNumber;
    }


    protected bool pathComplete()
    {
        if (Vector3.Distance(_agent.destination, _agent.transform.position) <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }


   
}
