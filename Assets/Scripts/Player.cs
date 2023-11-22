using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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

    private Vector3 _verticalDirection;

    [SerializeField]
    private GameObject _rollTeleport;

    //Velocity cache
    private float _yVelocity;

    [SerializeField]
    private float _rollSpeed = 8f;

    private Animator _anim;

    private bool _jumping;

    private bool _onLedge;

    public LedgeTrigger activeLedge;

    private bool _isRolling;

    [SerializeField]
    private int _lives = 3;

    private bool _playerIsDead;


    [SerializeField]
    private int _collectable;

    //Ladder Variables
    public bool _isOnLadder;

    public LadderEnter activeLadder;

    [SerializeField]
    private float _ladderSpeed = 3.0f;

    
    private GameManager _gameManager;

    private bool _playerHit;

    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        if (_uiManager == null)
        {
            Debug.Log("UIManager is NULL");
        }
    }
    private void Update()
    {
        if (_playerIsDead == false)
        {
            if (_playerHit == false)
            {
                CalculateMovement();

                if (_onLedge == true)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _anim.SetBool("ClimbUp", true);
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    DodgeRoll();
                }

            }

        }
    }

    private void CalculateMovement()
    {
            if (_isOnLadder == false && _isRolling == false)
            {
                HorizontalMovement();
            }

            if (_isOnLadder == true)
            {
                VerticalMovement();
            }
    }

    private void HorizontalMovement()
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
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }
    public void VerticalMovement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        _verticalDirection = new Vector3(0, verticalInput, 0);
        Vector3 velocity = _verticalDirection * _ladderSpeed;

        if (verticalInput > 0)
        {
            _anim.SetBool("ClimbUpLadder", true);
            _anim.SetBool("ClimbDownLadder", false);
        }
        else if (verticalInput < 0)
        {
            _anim.SetBool("ClimbDownLadder", true);
            _anim.SetBool("ClimbUpLadder", false);
        }

        if (verticalInput == 0)
        {
            _anim.speed = 0;
        }
        else _anim.speed = 1;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isOnLadder = false;
            _yVelocity = _jumpHeight;
        }

        Debug.Log("Vertical Axis Input: " + verticalInput);

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

    public int PowerCellTally()
    {
        return _collectable;
    }

    public void OnPlayerFall()
    {
        _lives--;
        _uiManager.UpdateLives(_lives);
        transform.position = new Vector3(95.1f, 68.96f, 0);
        _yVelocity = 0.0f;
    }

    public void OnLadder()
    {
        _isOnLadder = true;
        Debug.Log("OnLadder() is active");
        _anim.SetBool("ClimbUpLadder", true);
        _anim.SetBool("ClimbLadder", true);
        
    }

    public void OffLadder()
    {
        _isOnLadder = false;
        _anim.speed = 1;
        Debug.Log("Player is off the Ladder");
        _anim.SetBool("ClimbLadder", false);
        _anim.SetBool("ClimbUpLadder", false);
        _anim.SetBool("ClimbDownLadder", false);
    }

    private void DodgeRoll()
    {
        _anim.SetBool("Roll", true);
        _isRolling = true;

        if (_isRolling == true)
        {
            _controller.enabled = false;
            StartCoroutine(DodgeWait());
        }
    }

    IEnumerator DodgeWait()
    {
        yield return new WaitForSeconds(1.2f);
        transform.position = _rollTeleport.transform.position;
        _anim.SetBool("Roll", false);
        _isRolling = false;
        _controller.enabled = true;
    }

    public void PlayerDamage()
    {
        _lives--;
        _uiManager.UpdateLives(_lives);
        _playerHit = true;
        _anim.SetBool("PlayerHit", true);
        StartCoroutine(PlayerHitRoutine());


        if (_lives < 1)
        {
            _anim.SetTrigger("PlayerDeath");
            _controller.enabled = false;
            _playerIsDead = true;
            _gameManager.GameOver();
            StartCoroutine(DeathRoutine());
        }

        //if (_lives < 1 && Input.GetKeyDown(KeyCode.Return))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(5);
        _uiManager.GameOver();
    }

    IEnumerator PlayerHitRoutine()
    {
        yield return new WaitForSeconds(1.2f);
        _anim.SetBool("PlayerHit", false);
        _playerHit = false;
        Debug.Log("PlayerHitROutine played through");
    }
}
