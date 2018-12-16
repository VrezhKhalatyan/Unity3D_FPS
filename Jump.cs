using UnityEngine;
using System.Collections;

//GIVES THE ABILITY TO JUMP(CREATED FOR THE PLAYER)
[RequireComponent(typeof(CharacterController))]
public class Jump : MonoBehaviour {
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;
 
	private float _vertSpeed;
	private ControllerColliderHit _contact;

	private CharacterController _charController;
	

	// Use this for initialization
	void Start() {
		_vertSpeed = minFall;

		_charController = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update() {

		// start with zero and add movement components progressively
		Vector3 movement = Vector3.zero;

		

		// raycast down to address steep slopes and dropoff edge
		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
			float check = (_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check;	// to be sure check slightly beyond bottom of capsule
		}

		// y movement: possibly jump impulse up, always accel down
		// could _charController.isGrounded instead, but then cannot workaround dropoff edge
		if (hitGround) {
			if (Input.GetButtonDown("Jump")) {
				_vertSpeed = jumpSpeed;
			} else {
				_vertSpeed = minFall;
				
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
			if (_contact != null ) {	// not right at level start
				
			}

			// workaround for standing on dropoff edge
			if (_charController.isGrounded) {
				if (Vector3.Dot(movement, _contact.normal) < 0) {
					movement = _contact.normal * moveSpeed;
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
		}
 		movement.y = _vertSpeed;
 
        movement *= Time.deltaTime;
		_charController.Move(movement);
	}

    // store collision to use in Update
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
		
}
