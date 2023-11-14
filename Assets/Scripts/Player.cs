using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    public float _gravity = 1f;

    private Vector3 _direction;

    [SerializeField]
    private float _jumpHeight = 10f;

    //Velocity cache
    private float _yVelocity;

    private bool _canDoubleJump;

    private CharacterController _controller;

    private Animator _anim;

    private bool _jumping;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = _direction * _speed;

        _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        //what direction to face?
        //if direction on the x is greater than 0
        //then face right
        //else
        //face 

        if (horizontalInput != 0)
        {
            Vector3 facing = transform.localEulerAngles;
            facing.y = _direction.x > 0 ? 0 : 180; ;
            transform.localEulerAngles = facing;
        }
        

        if (_controller.isGrounded == true)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;

                //_anim.SetTrigger("RunningJump");
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);

                _canDoubleJump = true;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void GrabLedge()
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
    }
}
