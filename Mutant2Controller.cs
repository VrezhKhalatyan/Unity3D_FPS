using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/*This script is charge of implementing the alert system to alert other enemies within his range that the player
 has been spotted*/
public class Mutant2Controller : MonoBehaviour
{

    [SerializeField] float _alertDistance = 20f;
    private float _distance;



    public void AlertOthers(GameObject callingMutant)
    {
        //find all the mutant2 clones or otherwise and alert all others within a 20 feet radius that player has been spotted
        GameObject[] mutants2 = GameObject.FindGameObjectsWithTag("mutant2");

        if (mutants2 != null && mutants2.Length != 0)
        {
            foreach (var m2 in mutants2)
            {
                _distance = Vector3.Distance(m2.gameObject.transform.position, callingMutant.transform.position);
                if (_distance < _alertDistance)
                {
                    m2.GetComponent<StaticShootingEnemy>().IsAlert = true;
                    m2.GetComponentInChildren<Image>().enabled = true;
                }
            }


        }


        GameObject[] byStanders = GameObject.FindGameObjectsWithTag("byStander");

        if (byStanders != null && byStanders.Length != 0)
        {
            foreach (var b in byStanders)
            {
                _distance = Vector3.Distance(b.gameObject.transform.position, callingMutant.transform.position);
                if (_distance < _alertDistance)
                {
                    b.GetComponent<BystanderMovement>().IsAlert = true;
                    b.GetComponentInChildren<Image>().enabled = true;
                }
            }


        }


        StartCoroutine(TimerForAlertness(mutants2, byStanders));


    }

    IEnumerator TimerForAlertness(GameObject[] mutants2, GameObject[] byStanders)
    {
        yield return new WaitForSeconds(10);

        foreach (var m2 in mutants2)
        {
            if (m2 != null)
            {
                m2.GetComponent<StaticShootingEnemy>().IsAlert = false;
                m2.GetComponentInChildren<Image>().enabled = false;


            }
        }

        foreach (var b in byStanders)
        {
            if (b != null)
            {
                b.GetComponent<BystanderMovement>().IsAlert = false;
                b.GetComponentInChildren<Image>().enabled = false;

            }
        }
    }



    //TODO: Do something similar to mutant and bystander
}
