using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*This script keeps track of player health, damage, increase in health and stops enemies from shooting at him */
public class PlayerController : MonoBehaviour
{

    public int health;
    public int RoomNumber;
    private Animator _anim;
    private SceneController _sceneController;
    private PlayerWeaponsController _playerWeaponsController;
    private FPSInput _fpsInput;


    private GameObject _gameOver;
    private SimpleHealthBar _healthBar;
    private Image _crossHair;
    private AudioSource _healthAPickUpSound;


    void Start()
    {
        health = 100;
        _sceneController = GameObject.Find("Controller").GetComponent<SceneController>();
        

        _fpsInput = GetComponent<FPSInput>();
        _playerWeaponsController = GameObject.Find("Weapons").GetComponent<PlayerWeaponsController>();


        _gameOver = GameObject.Find("GameOver");
        _healthBar = GameObject.Find("Health").GetComponent<SimpleHealthBar>();

        _crossHair = GameObject.Find("CrossHair").GetComponent<Image>();

        _healthAPickUpSound = GetComponent<AudioSource>();


    }

    void Update()
    {
        _healthBar.UpdateBar(health, 100);
        //if player is killed stop enemy and player from shooting and stop enemy from moving around only rotate.
        if (health <= 0)
        {

            _gameOver.GetComponent<Image>().enabled = true;
            _crossHair.enabled = false;
            //Prevents the player from moving and shooting only moving camera
            _fpsInput.enabled = false;
            _playerWeaponsController.enabled = false;
            StopEnemyShooting();

            //Start the Losing Scene
            StartCoroutine(PlayLoseScene());

            if (Input.GetKeyDown(KeyCode.R))
                Application.LoadLevel(0);




        }



        if (Input.GetKeyDown(KeyCode.Q))
            EditorApplication.isPlaying = false;

        if (gameObject.transform.position.z < -13.5693f)
            RoomNumber = 3;
        else if (gameObject.transform.position.z >= -13.5693f && gameObject.transform.position.z <= 15.23492f)
            RoomNumber = 1;
        else
            RoomNumber = 2;



    }

    //Create Defeated Scene Coroutine
    IEnumerator PlayLoseScene(){
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
	}

    public void Hurt(int damage)
    {
        health -= damage;
        
        _healthBar.UpdateBar(health, 100);


    }
    /*The Health Pickup script calls the PlayerController to access
    the players health through health kit pickup and increases
    the players health */
    public void HealthP(int heal)
    {
        _healthAPickUpSound.Play();
        health += heal;
        Debug.Log("Health: " + health);
    }

    public void StopEnemyShooting()
    {

        //Below is the code to stop all enemy fire and the walking enemies will wander around.
        GameObject[] mutants = GameObject.FindGameObjectsWithTag("mutant");

        if (mutants != null && mutants.Length != 0)
        {
            foreach (var m in mutants)
            {
                m.GetComponent<Enemy1Movement>().enabled = false;
                /*m.GetComponentInChildren<MutantShoot>().enabled = false;*/
                m.GetComponent<WanderingAI>().enabled = true;
                m.GetComponent<ReactiveTarget>().enabled = false;
            }
        }

        GameObject[] mutants2 = GameObject.FindGameObjectsWithTag("mutant2");

        if (mutants2 != null && mutants2.Length != 0)
        {
            foreach (var m2 in mutants2)
            {
                m2.GetComponent<StaticShootingEnemy>().enabled = false;
                m2.GetComponent<ReactiveTarget>().enabled = false;
                _anim = m2.GetComponent<Animator>();
                _anim.SetBool("shootM2", false);
                _anim.SetBool("playerDied", true);

            }
        }

        GameObject[] byStanders = GameObject.FindGameObjectsWithTag("byStander");

        if (byStanders != null && byStanders.Length != 0)
        {

            foreach (var b in byStanders)
            {
                b.GetComponent<BystanderMovement>().enabled = false;
                b.GetComponent<ReactiveTarget>().enabled = false;
                b.GetComponent<WanderingAI>().enabled = true;
            }
        }



    }
}
