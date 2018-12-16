using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//This script will act as the controller for the player's weapon system.
//Allowing enemy to switch guns, shoot bullets to harm enemies and create bulletholes.
public class PlayerWeaponsController : MonoBehaviour
{

    //storing different guns
    [SerializeField]
    private enum Guns
    {
        Rifle = 0,
        Smg = 1

    }
    //Setting the initial player weapon to handgun
    [SerializeField] private Guns _playerGun;
    public int _currentGun;

    private Camera _camera;

    [SerializeField] private GameObject _handGunBulletHolePrefab;
    [SerializeField] private GameObject _riffleBulletHolePrefab;
    private GameObject _BulletHole;
    private AudioSource _gunFire;


    // Use this for initialization
    void Start()
    {
        // refer to the camera parent of the weapons object
        _camera = GetComponentInParent<Camera>();
        _gunFire = GetComponent<AudioSource>();
        _playerGun = Guns.Rifle;
        _currentGun = 0;
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            _gunFire.Play();
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            /*Ray ray = new Ray(transform.position, transform.forward);*/
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                 
                    target.ReactToHit();
                }
                else
                {
                    //TODO: Figure out why bulletholes don't work on walls and floors
                    Vector3 normal = hit.normal;
                    Vector3 pos = hit.point;
                    SwitchBulletHoles(normal, pos);

                }


            }


        }

        if (Input.GetMouseButtonDown(1)) {
            // toggle between weapon 0 and 1
            if(_currentGun == 0) {
                _currentGun = 1;
            }
            else if(_currentGun == 1) {
                _currentGun = 0;
            }
            SwitchWeapons();
        }
    }

    private IEnumerator BulletholeIndicator()
    {


        yield return new WaitForSeconds(2);
        Destroy(_BulletHole);
    }


    private void SwitchWeapons()
    {
        int weaponIndex = 0;
        foreach(Transform weapon in transform) {
            if (weaponIndex == _currentGun)
            {
                Debug.Log("Weapon Index: " + weaponIndex.ToString());
                switch(weaponIndex) {
                    case(0):
                        _playerGun = Guns.Rifle;
                        Debug.Log("Rifle now selected!");
                        break;
                    case(1):
                        _playerGun = Guns.Smg;
                        Debug.Log("Smg now selected!");
                        break;
                }
                weapon.gameObject.SetActive(true);
            }
            else 
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }

    }

    private void SwitchBulletHoles(Vector3 normal, Vector3 pos)
    {
        if (_playerGun == Guns.Smg)
        {
            _BulletHole = Instantiate(_handGunBulletHolePrefab, pos, Quaternion.FromToRotation(Vector3.up, normal));
            StartCoroutine(BulletholeIndicator());

        }

        else if (_playerGun == Guns.Rifle)
        {
            _BulletHole = Instantiate(_riffleBulletHolePrefab, pos, Quaternion.FromToRotation(Vector3.up, normal));
            StartCoroutine(BulletholeIndicator());
        }

    }
}
