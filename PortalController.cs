using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*this script is charge of allowing player to be teleported to another area in the scene*/
public class PortalController : MonoBehaviour
{

    private AudioSource _telePortSound;


    void Start()
    {
        _telePortSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _telePortSound.Play();
        if (this.gameObject.tag == "Portal1")
            other.transform.position = new Vector3(-15.7f, 4.66f, 24.2f);
        else if (this.gameObject.tag == "Portal2")
            other.transform.position = new Vector3(19.78f, 1, 1.73f);
        else if(this.gameObject.tag == "Portal3")
            other.transform.position = new Vector3(-16.02f, 4.66f, -21.18f);
        else if(this.gameObject.tag == "Portal4")
            other.transform.position = new Vector3(-58.84f,1, 1.43f);

    }
}
