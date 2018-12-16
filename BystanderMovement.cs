using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;
using System.Collections;


/*This script is in charge of bystanders movements and ascertaining the closest mutant 2 to run to*/

public class BystanderMovement : MonoBehaviour
{
    [SerializeField] private float _range = 12f;
    private Vector3 _destination;
    private NavMeshAgent _agent;
    private Transform _player;
    private Animator _anim;
    private bool _alive;
    private bool _running;
    private bool _lookedAtPlayer;
    private float _distance;
    private GameObject _closestAlly;
    private GameObject _closestMutant;
    private GameObject _closestMutant2;
    private float _closesAllyDistance;
    private float _rotationY;
    public bool IsAlert;
    private AudioSource _annoyedSound;
    private AudioSource _dyingSound;

    void Start()
    {
        _alive = true;
        // get a reference to the NavMesh Agent component
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").transform;
         _annoyedSound= gameObject.GetComponent<AudioSource>();
      
        
    }

    void Update()
    {


        _distance = Vector3.Distance(this.transform.position, _player.transform.position);
        if (_alive && _distance < _range || _running || IsAlert)
        {


            if (!_lookedAtPlayer)
            {
                _annoyedSound.Play();
                transform.LookAt(_player);
                _rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(0, _rotationY, 0);

            }

            FindClosestAlly();
            if (_closestAlly != null)
            {

                _agent.destination = _closestAlly.transform.position;
                _anim.SetBool("byStanderRunning", true);
                _lookedAtPlayer = true;
            }
            else 
            {
                GetComponent<BystanderMovement>().enabled = false;
                GetComponent<WanderingAI>().enabled = true;

            }


            if (!pathComplete())
                _running = true;
            else
            {
                _anim.SetBool("byStanderRunning", false);
                _running = false;
                transform.LookAt(_player);
                _rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            }
            
        }
        /*else if (!_alive)
            _dyingSound.Play();*/

    }





    private void FindClosestAlly()
    {


        float mDistance = GetClosestMutant();
        float m2Distance = GetClosestMutant2();

        if (_closestMutant != null && _closestMutant != null)
        {
            if (mDistance < m2Distance)
                _closestAlly = _closestMutant;
            else
                _closestAlly = _closestMutant2;
        }
        else if (_closestMutant != null)
            _closestAlly = _closestMutant;
        else
            _closestAlly = _closestMutant2;
    }

    private float GetClosestMutant()
    {
       

        GameObject[] mutants = GameObject.FindGameObjectsWithTag("mutant");
        if (mutants != null && mutants.Length != 0)
        {
            _closesAllyDistance = Vector3.Distance(gameObject.transform.position, mutants[0].transform.position);

            foreach (var m in mutants)
            {
                _distance = Vector3.Distance(gameObject.transform.position, m.transform.position);

                if (_distance <= _closesAllyDistance)
                {
                    _closesAllyDistance = _distance;
                    _closestMutant = m;

                }

            }

        }

        return _closesAllyDistance;

    }

    private float GetClosestMutant2()
    {
       

        GameObject[] mutant2s = GameObject.FindGameObjectsWithTag("mutant2");
        if (mutant2s != null && mutant2s.Length != 0)
        {

            _closesAllyDistance = Vector3.Distance(gameObject.transform.position, mutant2s[0].transform.position);


            foreach (var m2 in mutant2s)
            {

                _distance = Vector3.Distance(gameObject.transform.position, m2.transform.position);

                if (_distance <= _closesAllyDistance)
                {
                    _closesAllyDistance = _distance;
                    _closestMutant2 = m2;

                }

            }

        }

        return _closesAllyDistance;
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
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