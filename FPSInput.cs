using UnityEngine;
using System.Collections;

/*
This script allows the player to move up, down, left and right
*/
/*A portion of the script allows the player to run */
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float RunSpeed = 10.0f;
    public float gravity = -9.8f;
    public bool isRunning = false;

    private CharacterController _charController;
    private SceneController _sceneController;
 

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _sceneController = GameObject.Find("Controller").GetComponent<SceneController>();
    }
    void Update()
    {

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);


        if(Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0);

        
        /*When the character presses on the Left Shift button 
        speed is translated to Runspeed making the player run*/
        if(Input.GetKey(KeyCode.LeftShift)){
            isRunning = true;
            speed = RunSpeed;
            print("Running");
        }else{
            isRunning = false;
            speed = 6.0f;
        }
    }
}
