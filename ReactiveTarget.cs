using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/*This script is in charge of handling the action of an enemy dying*/
public class ReactiveTarget : MonoBehaviour
{
    private Animator _anim;
    private SceneController _sceneController;
    private UIManager _uiManager;
    
    


    void Start()
    {
        _anim = GetComponent<Animator>();
        _sceneController = GameObject.Find("Controller").GetComponent<SceneController>();
        _uiManager = GameObject.Find("Controller").GetComponent<UIManager>();

    }


    public void ReactToHit()
    {
        StaticShootingEnemy staticShootingEnemy = GetComponent<StaticShootingEnemy>();
        if ( staticShootingEnemy!= null)
            staticShootingEnemy.SetAlive(false);

        MutantShoot mv = GetComponent<MutantShoot>();
        if(mv != null)
            mv.SetAlive(false);
        

        BystanderMovement byStander = GetComponent<BystanderMovement>();
        if(byStander != null)
            byStander.SetAlive(false);


        StartCoroutine(Die());

        _sceneController.ReSpawn();
       
    }

    private IEnumerator Die()
    {
        if (this.gameObject.tag == "mutant2")
            _anim.SetBool("dieM2", true);

        else if (this.gameObject.tag == "mutant")
            _anim.SetBool("isDying", true);

        else
        {
            GetComponent<BystanderMovement>().enabled = false;
            GetComponent<WanderingAI>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            _anim.SetBool("byStanderDead", true);
           
            
        }

        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        _uiManager.UpdateKillsBar();
    }
}