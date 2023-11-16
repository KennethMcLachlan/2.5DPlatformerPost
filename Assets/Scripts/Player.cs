using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private UIManager _uiManager;
    
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 10f;

    private Vector3 _direction;

    //Velocity cache
    private float _yVelocity;

    [SerializeField]
    private float _rollSpeed = 8f;

    private Animator _anim;

    private bool _canDoubleJump;
    private bool _jumping;

    private bool _onLedge;

    public LedgeTrigger activeLedge;

    private bool _isRolling;

    [SerializeField]
    private int _collectable;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.Log("UIManager is NULL");
        }
    }
    private void Update()
    {
        CalculateMovement();

        if (_onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetBool("ClimbUp", true);
            }
        }
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = _direction * _speed;

        _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput != 0)
        {
            Vector3 facing = transform.localEulerAngles;
            facing.y = _direction.x > 0 ? 0 : 180; ;
            transform.localEulerAngles = facing;
        }

        if (_controller.isGrounded == true)
        {
            //_yVelocity = 0;
            _anim.SetBool("IdleJump", false);

            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _jumping = true;

                _anim.SetBool("IdleJump", true);
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

    public void GrabLedge(Vector3 handPosition, LedgeTrigger currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0.0f);

        _onLedge = true;

        transform.position = handPosition;
        activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = activeLedge.GetStandPosition();

        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
        _anim.SetBool("ClimbUp", false);
    }

    public void AddCollectable()
    {
        _collectable ++;
        _uiManager.UpdateCollectableDisplay(_collectable);
    }

    public void OnPlayerDeath()
    {
        transform.position = new Vector3(95.1f, 68.96f, 0);
        _yVelocity = 0.0f;
    }
}
