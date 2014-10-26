using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {
	
	public enum Facing {FACING_DOWN, FACING_LEFT, FACING_UP, FACING_RIGHT};

	private float _speed;
	private Facing _facing;
	private bool _isMoving;

	private Animator _anim;

/**
 *  Initialize values
 */
	void Start () {
		_speed = 3.0f;
		_facing = Facing.FACING_DOWN;
		_isMoving = false;
		_anim = GetComponent<Animator> ();
	}
	
/**
 *  Update once per frame
 */
	void Update () {
		updateFacing ();
		updateMovement ();
		updateAnimation ();
		move ();
	}

/*  Public Methods
==========================================================================*/
/**
 *  Frame-independent speed; use this in all cases instead of _speed
 */
	public float speed(){
		return _speed * Time.deltaTime;
	}

/**
 *  Returns whether the character is presently moving by means of Input
 */
	public bool isMoving(){
		return _isMoving;
	}

/**
 *  Set the direction the character is facing
 *  @param	__facing	One of four cardinal directions specified in CharController
 */
	public void setFacing(Facing __facing){
		_facing = __facing;
	}

/*  Private Methods
==========================================================================*/
/**
 *  @private
 *  Translates the character a single frame based on _facing
 */
	private void move(){
		if (_isMoving) {
			Vector3 translationVector = new Vector3 (0, 0, 0);
			switch(_facing){
				case Facing.FACING_DOWN: translationVector.y = -speed ();
					break;
				case Facing.FACING_LEFT: translationVector.x = -speed ();
					break;
				case Facing.FACING_UP: translationVector.y = speed ();
					break;
				case Facing.FACING_RIGHT: translationVector.x = speed ();
					break;
			}
			transform.Translate(translationVector);
		}
	}

/**
 *  @private
 *  If a key is being pressed, face the character in the direction of the key
 */
	private void updateFacing(){
		if (Input.GetKey (KeyCode.S))
			setFacing (Facing.FACING_DOWN);
		else if (Input.GetKey (KeyCode.A))
			setFacing (Facing.FACING_LEFT);
		else if (Input.GetKey (KeyCode.W))
			setFacing (Facing.FACING_UP);
		else if (Input.GetKey (KeyCode.D))
			setFacing (Facing.FACING_RIGHT);
	}

/**
 *  @private
 *  If a key is being pressed, indicate with the _isMoving variable
 */
	private void updateMovement(){
		if (Input.GetKey (KeyCode.S) ||
			Input.GetKey (KeyCode.A) ||
			Input.GetKey (KeyCode.W) ||
			Input.GetKey (KeyCode.D))
			_isMoving = true;
		else _isMoving = false;

	}

/**
 *  @private
 *  Update all animation parameters here
 */
	private void updateAnimation(){
		_anim.SetInteger ("facing", (int)_facing);
		_anim.SetBool ("isMoving", _isMoving);
	}
}
