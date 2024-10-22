using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _playerRigidbody2D;
    public float _playerSpedd;
    public Vector2 _playerDirection;

    private Animator _playerAnimator;
    private bool _playerFaceRight = true;
    private bool _isWalk;

    private int _punchCount = 0;
    private float _timePunch = 0.75f;



    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(_isWalk  == false)
            {

                StartCoroutine(PunchController());

                if (_punchCount < 2)
                {
                    PlayerJab();
                    _punchCount++;
                }
                else if(_punchCount >= 2) 
                {
                    PlayerPunch();
                    _punchCount = 0;
                }

                StopCoroutine(PunchController());
            }
        }

    }

    private void FixedUpdate()
    {
        if (_playerDirection.x != 0 || _playerDirection.y != 0)
        {
            _isWalk = true;
        }
        else
        {
            _isWalk = false;
        }

        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection.normalized * _playerSpedd * Time.fixedDeltaTime);
    }

    void PlayerMove()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(_playerDirection.x < 0 && _playerFaceRight)
        {
            Flip();
        }
        else if(_playerDirection.x > 0 && !_playerFaceRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        _playerAnimator.SetBool("isWalk", _isWalk); //true ou false 
    }

    void Flip()
    {
        _playerFaceRight = !_playerFaceRight;
        transform.Rotate(0f, 180, 0f);
    }

    void PlayerJab()
    {
        _playerAnimator.SetTrigger("isJab");
    }

    void PlayerPunch()
    {
        _playerAnimator.SetTrigger("is_Punch");
    }
    
    IEnumerator PunchController()
    {
        yield return new WaitForSeconds(_timePunch);
        _punchCount = 0;
    }

}
